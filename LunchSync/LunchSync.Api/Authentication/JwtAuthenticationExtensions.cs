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
            // Validate token Cognito, con actor type duoc them o event ben duoi.
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = cognitoIssuer,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(2),
                NameClaimType = "name",
                RoleClaimType = "cognito:groups"
            };
            options.Events = new JwtBearerEvents
            {
                OnTokenValidated = context =>
                {
                    // Them actor type de policy auth trong app dung chung.
                    var identity = context.Principal?.Identity as ClaimsIdentity;
                    if (identity is null)
                    {
                        context.Fail("Missing identity.");
                        return Task.CompletedTask;
                    }

                    var tokenUse = context.Principal?.FindFirst("token_use")?.Value;
                    var subject = context.Principal?.FindFirst("sub")?.Value;
                    var audience = context.Principal?.FindFirst("aud")?.Value;
                    var clientId = context.Principal?.FindFirst("client_id")?.Value;

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

                    if (!identity.HasClaim(claim => claim.Type == AuthClaimTypes.ActorType))
                    {
                        identity.AddClaim(new Claim(AuthClaimTypes.ActorType, AuthActorTypes.User));
                    }

                    return Task.CompletedTask;
                }
            };
        });

        return services;
    }
}
