using System.Net.Http.Headers;
using System.Text.Json;
using LunchSync.Core.Exceptions;
using LunchSync.Core.Modules.Auth;
using LunchSync.Core.Modules.Auth.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LunchSync.Infrastructure.Auth;

public sealed class CognitoOAuthProvider : ICognitoOAuthProvider
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<CognitoOAuthProvider> _logger;

    public CognitoOAuthProvider(
        HttpClient httpClient,
        IConfiguration configuration,
        ILogger<CognitoOAuthProvider> logger)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<CognitoTokenResponse> ExchangeCodeAsync(
        AuthCallbackRequest request,
        CancellationToken cancellationToken = default)
    {
        var formValues = CreateBaseClientForm();
        formValues["grant_type"] = "authorization_code";
        formValues["code"] = request.Code.Trim();
        formValues["redirect_uri"] = GetRequiredConfig("Cognito:RedirectUri");

        return await SendTokenRequestAsync(formValues, "code", cancellationToken);
    }

    public async Task<CognitoTokenResponse> RefreshTokenAsync(
        RefreshTokenRequest request,
        CancellationToken cancellationToken = default)
    {
        var formValues = CreateBaseClientForm();
        formValues["grant_type"] = "refresh_token";
        formValues["refresh_token"] = request.RefreshToken.Trim();

        return await SendTokenRequestAsync(formValues, "refresh_token", cancellationToken);
    }

    public async Task RevokeTokenAsync(
        RevokeTokenRequest request,
        CancellationToken cancellationToken = default)
    {
        var formValues = new Dictionary<string, string>
        {
            ["token"] = request.RefreshToken.Trim()
        };

        using var httpRequest = CreateClientAuthenticatedRequest(
            HttpMethod.Post,
            BuildEndpoint("/oauth2/revoke"),
            formValues);

        using var response = await SendAsync(httpRequest, cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        var body = await ReadBodyAsync(response, cancellationToken);
        throw MapOAuthError(body, "refresh_token");
    }

    public async Task<CognitoUserProfileResult> GetUserProfileAsync(
        string accessToken,
        CancellationToken cancellationToken = default)
    {
        using var httpRequest = new HttpRequestMessage(HttpMethod.Get, BuildEndpoint("/oauth2/userInfo"));
        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        using var response = await SendAsync(httpRequest, cancellationToken);
        var body = await ReadBodyAsync(response, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            throw MapOAuthError(body, "access_token");
        }

        var profile = JsonSerializer.Deserialize<CognitoUserProfileResult>(body, JsonOptions);
        if (profile is null
            || string.IsNullOrWhiteSpace(profile.CognitoSub)
            || string.IsNullOrWhiteSpace(profile.Email))
        {
            throw new InvalidOperationException("Cognito user info response is missing required fields.");
        }

        return profile with
        {
            CognitoSub = profile.CognitoSub.Trim(),
            Email = profile.Email.Trim().ToLowerInvariant(),
            FullName = profile.FullName?.Trim()
        };
    }

    private async Task<CognitoTokenResponse> SendTokenRequestAsync(
        IReadOnlyDictionary<string, string> formValues,
        string errorField,
        CancellationToken cancellationToken)
    {
        using var httpRequest = CreateClientAuthenticatedRequest(
            HttpMethod.Post,
            BuildEndpoint("/oauth2/token"),
            formValues);

        using var response = await SendAsync(httpRequest, cancellationToken);
        var body = await ReadBodyAsync(response, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            throw MapOAuthError(body, errorField);
        }

        var tokenResponse = JsonSerializer.Deserialize<CognitoTokenResponse>(body, JsonOptions);
        if (tokenResponse is null || string.IsNullOrWhiteSpace(tokenResponse.AccessToken))
        {
            throw new InvalidOperationException("Cognito token response is missing the access token.");
        }

        return tokenResponse with
        {
            AccessToken = tokenResponse.AccessToken.Trim(),
            IdToken = tokenResponse.IdToken?.Trim(),
            RefreshToken = tokenResponse.RefreshToken?.Trim(),
            TokenType = string.IsNullOrWhiteSpace(tokenResponse.TokenType)
                ? "Bearer"
                : tokenResponse.TokenType.Trim()
        };
    }

    private Dictionary<string, string> CreateBaseClientForm()
    {
        var formValues = new Dictionary<string, string>
        {
            ["client_id"] = GetRequiredConfig("Cognito:ClientId")
        };

        return formValues;
    }

    private HttpRequestMessage CreateClientAuthenticatedRequest(
        HttpMethod method,
        string endpoint,
        IReadOnlyDictionary<string, string> formValues)
    {
        var clientId = GetRequiredConfig("Cognito:ClientId");
        var clientSecret = _configuration["Cognito:ClientSecret"]?.Trim();
        var contentValues = new Dictionary<string, string>(formValues);

        var request = new HttpRequestMessage(method, endpoint);
        if (string.IsNullOrWhiteSpace(clientSecret))
        {
            contentValues["client_id"] = clientId;
        }
        else
        {
            var credentials = Convert.ToBase64String(
                System.Text.Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", credentials);
        }

        request.Content = new FormUrlEncodedContent(contentValues);
        return request;
    }

    private string BuildEndpoint(string path)
    {
        var domain = GetRequiredConfig("Cognito:Domain").Trim().TrimEnd('/');
        if (!Uri.TryCreate($"{domain}{path}", UriKind.Absolute, out var endpoint))
        {
            throw new InvalidOperationException($"Invalid Cognito domain configuration: {domain}");
        }

        return endpoint.ToString();
    }

    private string GetRequiredConfig(string key)
    {
        var value = _configuration[key];
        if (!string.IsNullOrWhiteSpace(value))
        {
            return value;
        }

        throw new InvalidOperationException($"Missing configuration: {key}");
    }

    private static async Task<string> ReadBodyAsync(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        return await response.Content.ReadAsStringAsync(cancellationToken);
    }

    private async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        try
        {
            return await _httpClient.SendAsync(request, cancellationToken);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Failed to reach Cognito OAuth endpoint {Endpoint}", request.RequestUri);
            throw new AuthProviderUnavailableException("Cognito", ex);
        }
    }

    private Exception MapOAuthError(string body, string field)
    {
        string? error = null;
        string? description = null;

        if (!string.IsNullOrWhiteSpace(body))
        {
            try
            {
                using var document = JsonDocument.Parse(body);
                var root = document.RootElement;
                error = root.TryGetProperty("error", out var errorValue)
                    ? errorValue.GetString()
                    : null;
                description = root.TryGetProperty("error_description", out var descriptionValue)
                    ? descriptionValue.GetString()
                    : null;
            }
            catch (JsonException ex)
            {
                _logger.LogWarning(ex, "Failed to parse Cognito OAuth error payload: {Body}", body);
            }
        }

        return error switch
        {
            "invalid_grant" => new ValidationException(
                "Token Cognito khong hop le hoac da het han.",
                new Dictionary<string, string> { [field] = description ?? "Gia tri token/code khong hop le." }),
            "invalid_request" => new ValidationException(
                "Yeu cau Cognito OAuth khong hop le.",
                new Dictionary<string, string> { [field] = description ?? "Thong tin gui len khong hop le." }),
            "invalid_client" => new InvalidOperationException(description ?? "Cognito client configuration is invalid."),
            _ => new InvalidOperationException(description ?? "Cognito OAuth request failed.")
        };
    }
}
