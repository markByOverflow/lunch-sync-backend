using System.Security.Claims;
using LunchSync.Core.Common.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace LunchSync.Api.Authentication;

public static class JwtAuthenticationExtensions
{
    public const string CognitoScheme = "CognitoJwt";

    public static IServiceCollection AddLunchSyncAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var cognitoIssuer = configuration["Cognito:Issuer"];
        var cognitoClientId = configuration["Cognito:ClientId"];
        var expectedTokenUse = configuration["Cognito:TokenUse"] ?? "id";

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = CognitoScheme;
            options.DefaultChallengeScheme = CognitoScheme;
        })
        .AddJwtBearer(CognitoScheme, options =>
        {
            options.MapInboundClaims = false;
            options.Authority = cognitoIssuer;
            options.RequireHttpsMetadata = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = cognitoIssuer,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(2)
            };
            options.Events = new JwtBearerEvents
            {
                OnTokenValidated = context =>
                {
                    var principal = context.Principal;
                    var identity = context.Principal?.Identity as ClaimsIdentity;
                    if (principal is null || identity is null)
                    {
                        context.Fail("Missing principal.");
                        return Task.CompletedTask;
                    }

                    var tokenUse = principal.FindFirst("token_use")?.Value;
                    var subject = principal.FindFirst("sub")?.Value;
                    var audience = principal.FindFirst("aud")?.Value;
                    var clientId = principal.FindFirst("client_id")?.Value;

                    if (tokenUse != expectedTokenUse || string.IsNullOrWhiteSpace(subject))
                    {
                        context.Fail("Invalid Cognito token.");
                        return Task.CompletedTask;
                    }

                    if (!string.IsNullOrWhiteSpace(cognitoClientId)
                        && audience != cognitoClientId
                        && clientId != cognitoClientId)
                    {
                        context.Fail("Invalid Cognito audience.");
                        return Task.CompletedTask;
                    }

                    AddOrReplaceClaim(identity, AuthClaimTypes.ActorType, AuthActorTypes.User);
                    AddOrReplaceClaim(identity, AuthClaimTypes.IsActive, bool.TrueString);
                    CopyClaim(principal, identity, "email", AuthClaimTypes.Email);
                    CopyClaim(principal, identity, "name", AuthClaimTypes.FullName);

                    return Task.CompletedTask;
                }
            };
        });

        return services;
    }

    private static void AddOrReplaceClaim(ClaimsIdentity identity, string claimType, string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        foreach (var claim in identity.FindAll(claimType).ToList())
        {
            identity.RemoveClaim(claim);
        }

        identity.AddClaim(new Claim(claimType, value));
    }

    private static void CopyClaim(
        ClaimsPrincipal principal,
        ClaimsIdentity identity,
        string sourceClaimType,
        string targetClaimType)
    {
        var value = principal.FindFirst(sourceClaimType)?.Value;
        AddOrReplaceClaim(identity, targetClaimType, value);
    }
}
