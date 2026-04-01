using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;
using LunchSync.Core.Common.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LunchSync.Api.Authentication;

public sealed class AlbOidcAuthenticationHandler
    : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly AlbOidcOptions _albOptions;
    private readonly AlbPublicKeyProvider _publicKeyProvider;

    public AlbOidcAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IOptions<AlbOidcOptions> albOptions,
        AlbPublicKeyProvider publicKeyProvider)
        : base(options, logger, encoder)
    {
        _albOptions = albOptions.Value;
        _publicKeyProvider = publicKeyProvider;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var rawToken = Request.Headers[AuthHeaderNames.AlbOidcData].FirstOrDefault();
        if (string.IsNullOrWhiteSpace(rawToken))
        {
            return AuthenticateResult.NoResult();
        }

        JwtSecurityToken jwt;
        try
        {
            jwt = new JwtSecurityTokenHandler().ReadJwtToken(rawToken);
        }
        catch (Exception ex)
        {
            return AuthenticateResult.Fail($"Invalid ALB OIDC token format: {ex.Message}");
        }

        try
        {
            var keyId = ReadRequiredHeader(jwt, JwtHeaderParameterNames.Kid);
            var signer = ReadRequiredHeader(jwt, "signer");
            var issuer = ReadRequiredHeader(jwt, JwtRegisteredClaimNames.Iss);
            var clientId = ReadRequiredHeader(jwt, "client");
            var expiresAtUnix = ReadRequiredUnixHeader(jwt, JwtRegisteredClaimNames.Exp);
            var algorithm = ReadRequiredHeader(jwt, JwtHeaderParameterNames.Alg);

            if (!string.Equals(algorithm, SecurityAlgorithms.EcdsaSha256, StringComparison.Ordinal))
            {
                return AuthenticateResult.Fail("Invalid ALB OIDC signing algorithm.");
            }

            if (!string.IsNullOrWhiteSpace(_albOptions.LoadBalancerArn)
                && !string.Equals(signer, _albOptions.LoadBalancerArn, StringComparison.Ordinal))
            {
                return AuthenticateResult.Fail("Unexpected ALB signer.");
            }

            if (!string.IsNullOrWhiteSpace(_albOptions.Issuer)
                && !string.Equals(issuer, _albOptions.Issuer, StringComparison.Ordinal))
            {
                return AuthenticateResult.Fail("Invalid ALB OIDC issuer.");
            }

            if (!string.IsNullOrWhiteSpace(_albOptions.ClientId)
                && !string.Equals(clientId, _albOptions.ClientId, StringComparison.Ordinal))
            {
                return AuthenticateResult.Fail("Invalid ALB OIDC client id.");
            }

            var clockSkew = TimeSpan.FromSeconds(Math.Max(0, _albOptions.ClockSkewSeconds));
            var expiresAtUtc = DateTimeOffset.FromUnixTimeSeconds(expiresAtUnix);
            if (expiresAtUtc < DateTimeOffset.UtcNow.Subtract(clockSkew))
            {
                return AuthenticateResult.Fail("Expired ALB OIDC token.");
            }

            var region = ResolveRegion(signer);
            if (string.IsNullOrWhiteSpace(region))
            {
                return AuthenticateResult.Fail("Cannot resolve ALB region.");
            }

            var signingKey = await _publicKeyProvider.GetSigningKeyAsync(
                region,
                keyId,
                Context.RequestAborted);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                RequireSignedTokens = true,
                ValidAlgorithms = [SecurityAlgorithms.EcdsaSha256]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(
                rawToken,
                validationParameters,
                out _);

            var identity = new ClaimsIdentity(
                principal.Claims,
                Scheme.Name,
                "name",
                ClaimTypes.Role);

            if (string.IsNullOrWhiteSpace(identity.FindFirst("sub")?.Value))
            {
                return AuthenticateResult.Fail("ALB OIDC token is missing sub.");
            }

            if (!identity.HasClaim(claim => claim.Type == AuthClaimTypes.ActorType))
            {
                identity.AddClaim(new Claim(AuthClaimTypes.ActorType, AuthActorTypes.User));
            }

            var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
        catch (SecurityTokenException ex)
        {
            return AuthenticateResult.Fail($"Invalid ALB OIDC token: {ex.Message}");
        }
        catch (Exception ex)
        {
            return AuthenticateResult.Fail($"ALB OIDC authentication failed: {ex.Message}");
        }
    }

    private string ResolveRegion(string signer)
    {
        if (!string.IsNullOrWhiteSpace(_albOptions.Region))
        {
            return _albOptions.Region;
        }

        var parts = signer.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (parts.Length > 3)
        {
            return parts[3];
        }

        return string.Empty;
    }

    private static string ReadRequiredHeader(JwtSecurityToken jwt, string key)
    {
        if (!jwt.Header.TryGetValue(key, out var value) || value is null)
        {
            throw new SecurityTokenException($"Missing header '{key}'.");
        }

        var stringValue = Convert.ToString(value, CultureInfo.InvariantCulture);
        if (string.IsNullOrWhiteSpace(stringValue))
        {
            throw new SecurityTokenException($"Missing header '{key}'.");
        }

        return stringValue;
    }

    private static long ReadRequiredUnixHeader(JwtSecurityToken jwt, string key)
    {
        var value = ReadRequiredHeader(jwt, key);
        if (!long.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var unixTime))
        {
            throw new SecurityTokenException($"Invalid numeric header '{key}'.");
        }

        return unixTime;
    }
}
