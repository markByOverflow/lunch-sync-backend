using System.Security.Claims;
using LunchSync.Core.Common.Auth;
using LunchSync.Core.Modules.Auth.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
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
                NameClaimType = AuthClaimTypes.FullName,
                RoleClaimType = AuthClaimTypes.Role
            };
            options.Events = new JwtBearerEvents
            {
                OnTokenValidated = async context =>
                {
                    var identity = context.Principal?.Identity as ClaimsIdentity;
                    if (identity is null)
                    {
                        context.Fail("Missing identity.");
                        return;
                    }

                    var tokenUse = context.Principal?.FindFirst("token_use")?.Value;
                    var subject = context.Principal?.FindFirst("sub")?.Value;
                    var audience = context.Principal?.FindFirst("aud")?.Value;
                    var clientId = context.Principal?.FindFirst("client_id")?.Value;

                    if (tokenUse != expectedTokenUse || string.IsNullOrWhiteSpace(subject))
                    {
                        context.Fail("Invalid Cognito token.");
                        return;
                    }

                    if (!string.IsNullOrWhiteSpace(cognitoClientId)
                        && audience != cognitoClientId
                        && clientId != cognitoClientId)
                    {
                        context.Fail("Invalid Cognito audience.");
                        return;
                    }

                    var userRepository = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
                    var localUser = await userRepository.GetByCognitoSubAsync(subject, context.HttpContext.RequestAborted);
                    if (localUser is null || !localUser.IsActive)
                    {
                        context.Fail("Local user is missing or inactive.");
                        return;
                    }

                    if (!identity.HasClaim(claim => claim.Type == AuthClaimTypes.ActorType))
                    {
                        identity.AddClaim(new Claim(AuthClaimTypes.ActorType, AuthActorTypes.User));
                    }

                    AddOrReplaceClaim(identity, AuthClaimTypes.LocalUserId, localUser.Id.ToString());
                    AddOrReplaceClaim(identity, AuthClaimTypes.Role, localUser.Role.ToString().ToLowerInvariant());
                    AddOrReplaceClaim(identity, AuthClaimTypes.IsActive, bool.TrueString);
                    AddOrReplaceClaim(identity, AuthClaimTypes.Email, localUser.Email);
                    AddOrReplaceClaim(identity, AuthClaimTypes.FullName, localUser.FullName);
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
}
