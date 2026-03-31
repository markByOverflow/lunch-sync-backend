using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
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
        LogCognitoConfigSnapshot("register", request.Email);

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
        EnsureSuccess(response, document);

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
        LogCognitoConfigSnapshot("login", request.Email);

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
        EnsureSuccess(response, document);

        if (!document.RootElement.TryGetProperty("AuthenticationResult", out var authResult))
        {
            throw new InvalidOperationException("Cognito login did not return authentication result.");
        }

        var idToken = authResult.GetProperty("IdToken").GetString();
        var expiresIn = authResult.GetProperty("ExpiresIn").GetInt32();
        if (string.IsNullOrWhiteSpace(idToken))
        {
            throw new InvalidOperationException("Cognito login did not return id token.");
        }

        // App tam thoi dung id token lam bearer token vi backend can profile claim.
        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(idToken);
        var cognitoSub = jwt.Claims.FirstOrDefault(claim => claim.Type == "sub")?.Value;
        var email = jwt.Claims.FirstOrDefault(claim => claim.Type == "email")?.Value;
        var fullName = jwt.Claims.FirstOrDefault(claim => claim.Type == "name")?.Value;

        if (string.IsNullOrWhiteSpace(cognitoSub) || string.IsNullOrWhiteSpace(email))
        {
            throw new InvalidOperationException("Cognito id token is missing required claims.");
        }

        return new CognitoLoginResult(
            idToken,
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

    private static void EnsureSuccess(HttpResponseMessage response, JsonDocument document)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        var root = document.RootElement;
        var message = root.TryGetProperty("message", out var lowerMessage)
            ? lowerMessage.GetString()
            : root.TryGetProperty("Message", out var upperMessage)
                ? upperMessage.GetString()
                : "Cognito request failed.";

        throw new InvalidOperationException(message ?? "Cognito request failed.");
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

    private void LogCognitoConfigSnapshot(string operation, string? email)
    {
        // Log tam de check app dang doc config nao khi goi Cognito.
        _logger.LogInformation(
            "Cognito config snapshot. Operation={Operation}, Region={Region}, ClientId={ClientId}, AuthFlow={AuthFlow}, Email={Email}, HasClientSecret={HasClientSecret}",
            operation,
            _configuration["AWS:Region"],
            _configuration["Cognito:ClientId"],
            _configuration["Cognito:AuthFlow"],
            email,
            !string.IsNullOrWhiteSpace(_configuration["Cognito:ClientSecret"]));
    }
}
