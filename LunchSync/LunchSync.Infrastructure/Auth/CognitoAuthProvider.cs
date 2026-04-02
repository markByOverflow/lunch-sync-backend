using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using LunchSync.Core.Exceptions;
using LunchSync.Core.Modules.Auth;
using LunchSync.Core.Modules.Auth.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LunchSync.Infrastructure.Auth;

public sealed class CognitoAuthProvider : ICognitoAuthProvider
{
    private static readonly JsonSerializerOptions CognitoJsonOptions = new()
    {
        PropertyNamingPolicy = null
    };

    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<CognitoAuthProvider> _logger;

    public CognitoAuthProvider(
        HttpClient httpClient,
        IConfiguration configuration,
        ILogger<CognitoAuthProvider> logger)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<CognitoRegisterResult> RegisterAsync(
        RegisterRequest request,
        CancellationToken cancellationToken = default)
    {
        LogCognitoConfigSnapshot("register");

        var payload = new
        {
            ClientId = GetRequiredConfig("Cognito:ClientId"),
            SecretHash = BuildSecretHash(request.Email),
            Username = request.Email,
            Password = request.Password,
            UserAttributes = BuildUserAttributes(request)
        };

        using var response = await SendCognitoRequestAsync(
            "AWSCognitoIdentityProviderService.SignUp",
            payload,
            cancellationToken);

        using var document = await ReadResponseDocumentAsync(response, cancellationToken);
        EnsureSuccess(response, document, request.Email);

        var root = document.RootElement;
        var userSub = root.GetProperty("UserSub").GetString();
        if (string.IsNullOrWhiteSpace(userSub))
        {
            throw new InvalidOperationException("Cognito register did not return user sub.");
        }

        return new CognitoRegisterResult(
            userSub,
            request.Email.Trim().ToLowerInvariant(),
            request.FullName?.Trim());
    }

    public async Task<CognitoLoginResult> LoginAsync(
        LoginRequest request,
        CancellationToken cancellationToken = default)
    {
        LogCognitoConfigSnapshot("login");

        var payload = new
        {
            AuthFlow = _configuration["Cognito:AuthFlow"] ?? "USER_PASSWORD_AUTH",
            ClientId = GetRequiredConfig("Cognito:ClientId"),
            AuthParameters = BuildAuthParameters(request)
        };

        using var response = await SendCognitoRequestAsync(
            "AWSCognitoIdentityProviderService.InitiateAuth",
            payload,
            cancellationToken);

        using var document = await ReadResponseDocumentAsync(response, cancellationToken);
        EnsureSuccess(response, document, request.Email);

        if (!document.RootElement.TryGetProperty("AuthenticationResult", out var authResult))
        {
            throw new InvalidOperationException("Cognito login did not return authentication result.");
        }

        var accessToken = authResult.GetProperty("AccessToken").GetString();
        var idToken = authResult.TryGetProperty("IdToken", out var idTokenElement)
            ? idTokenElement.GetString()
            : null;
        var expiresIn = authResult.GetProperty("ExpiresIn").GetInt32();
        if (string.IsNullOrWhiteSpace(accessToken))
        {
            throw new InvalidOperationException("Cognito login did not return access token.");
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var accessJwt = tokenHandler.ReadJwtToken(accessToken);
        var cognitoSub = accessJwt.Claims.FirstOrDefault(claim => claim.Type == "sub")?.Value;

        string? email = null;
        string? fullName = null;
        if (!string.IsNullOrWhiteSpace(idToken))
        {
            var idJwt = tokenHandler.ReadJwtToken(idToken);
            email = idJwt.Claims.FirstOrDefault(claim => claim.Type == "email")?.Value;
            fullName = idJwt.Claims.FirstOrDefault(claim => claim.Type == "name")?.Value;
        }

        email ??= request.Email.Trim().ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(cognitoSub) || string.IsNullOrWhiteSpace(email))
        {
            throw new InvalidOperationException("Cognito access token is missing required claims.");
        }

        return new CognitoLoginResult(
            accessToken,
            expiresIn,
            cognitoSub,
            email.Trim().ToLowerInvariant(),
            fullName?.Trim());
    }

    private async Task<HttpResponseMessage> SendCognitoRequestAsync(
        string target,
        object payload,
        CancellationToken cancellationToken)
    {
        var region = GetRequiredConfig("AWS:Region");
        var endpoint = $"https://cognito-idp.{region}.amazonaws.com/";

        // Log tam de xac nhan runtime dang goi dung endpoint Cognito nao.
        _logger.LogInformation(
            "Sending Cognito request. Target={Target}, Region={Region}, Endpoint={Endpoint}",
            target,
            region,
            endpoint);

        using var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
        request.Headers.TryAddWithoutValidation("X-Amz-Target", target);
        request.Headers.TryAddWithoutValidation("Accept", "application/json");
        request.Content = new StringContent(
            // Cognito JSON API yeu cau dung ten field PascalCase nhu ClientId, Username...
            JsonSerializer.Serialize(payload, CognitoJsonOptions),
            Encoding.UTF8,
            "application/x-amz-json-1.1");

        return await _httpClient.SendAsync(request, cancellationToken);
    }

    private static async Task<JsonDocument> ReadResponseDocumentAsync(
        HttpResponseMessage response,
        CancellationToken cancellationToken)
    {
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        if (string.IsNullOrWhiteSpace(content))
        {
            content = "{}";
        }

        return JsonDocument.Parse(content);
    }

    private static void EnsureSuccess(HttpResponseMessage response, JsonDocument document, string? email = null)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        var root = document.RootElement;
        var errorType = root.TryGetProperty("__type", out var type)
            ? type.GetString()
            : root.TryGetProperty("code", out var code)
                ? code.GetString()
                : null;

        if (!string.IsNullOrWhiteSpace(errorType) && errorType.Contains('#'))
        {
            errorType = errorType.Split('#').Last();
        }

        var message = root.TryGetProperty("message", out var lowerMessage)
            ? lowerMessage.GetString()
            : root.TryGetProperty("Message", out var upperMessage)
                ? upperMessage.GetString()
                : "Cognito request failed.";

        throw MapCognitoException(errorType, message, email);
    }

    private static Exception MapCognitoException(string? errorType, string? message, string? email)
    {
        return errorType switch
        {
            "UsernameExistsException" => new DuplicateEntityException("User", "email", email ?? "unknown"),
            "NotAuthorizedException" => new InvalidCredentialsException(),
            "UserNotFoundException" => new InvalidCredentialsException(),
            "UserNotConfirmedException" => new ValidationException(
                "Tài khoản chưa được xác nhận.",
                new Dictionary<string, string> { ["email"] = "Vui lòng xác nhận email trước khi đăng nhập." }),
            "InvalidPasswordException" => new ValidationException(
                "Mật khẩu không hợp lệ.",
                new Dictionary<string, string> { ["password"] = message ?? "Mật khẩu không đáp ứng policy của Cognito." }),
            "InvalidParameterException" => new ValidationException(
                "Dữ liệu gửi lên không hợp lệ.",
                new Dictionary<string, string> { ["auth"] = message ?? "Yêu cầu không hợp lệ." }),
            _ => new InvalidOperationException(message ?? "Cognito request failed.")
        };
    }

    private Dictionary<string, string> BuildAuthParameters(LoginRequest request)
    {
        var authParameters = new Dictionary<string, string>
        {
            ["USERNAME"] = request.Email.Trim().ToLowerInvariant(),
            ["PASSWORD"] = request.Password
        };

        var secretHash = BuildSecretHash(request.Email);
        if (!string.IsNullOrWhiteSpace(secretHash))
        {
            authParameters["SECRET_HASH"] = secretHash;
        }

        return authParameters;
    }

    private object[] BuildUserAttributes(RegisterRequest request)
    {
        var attributes = new List<object>
        {
            new
            {
                Name = "email",
                Value = request.Email.Trim().ToLowerInvariant()
            }
        };

        if (!string.IsNullOrWhiteSpace(request.FullName))
        {
            attributes.Add(new
            {
                Name = "name",
                Value = request.FullName.Trim()
            });
        }

        return attributes.ToArray();
    }

    private string? BuildSecretHash(string username)
    {
        var clientSecret = _configuration["Cognito:ClientSecret"];
        var clientId = _configuration["Cognito:ClientId"];
        if (string.IsNullOrWhiteSpace(clientSecret) || string.IsNullOrWhiteSpace(clientId))
        {
            return null;
        }

        // Secret hash can cho app client co secret tren Cognito.
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(clientSecret));
        var data = Encoding.UTF8.GetBytes($"{username.Trim().ToLowerInvariant()}{clientId}");
        return Convert.ToBase64String(hmac.ComputeHash(data));
    }

    private string GetRequiredConfig(string key)
    {
        var value = _configuration[key];
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidOperationException($"Missing configuration: {key}");
        }

        return value;
    }

    private void LogCognitoConfigSnapshot(string operation)
    {
        _logger.LogInformation(
            "Cognito config snapshot. Operation={Operation}, Region={Region}, ClientId={ClientId}, AuthFlow={AuthFlow}, HasClientSecret={HasClientSecret}",
            operation,
            _configuration["AWS:Region"],
            _configuration["Cognito:ClientId"],
            _configuration["Cognito:AuthFlow"],
            !string.IsNullOrWhiteSpace(_configuration["Cognito:ClientSecret"]));
    }
}
