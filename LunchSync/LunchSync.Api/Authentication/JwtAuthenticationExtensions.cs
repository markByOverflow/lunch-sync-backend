using System.Security.Claims;
using System.Text;
using LunchSync.Core.Common.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace LunchSync.Api.Authentication;

public static class JwtAuthenticationExtensions
{
    // Combined scheme giup app tu chon host JWT hay guest JWT theo header.
    public const string CombinedScheme = "CombinedJwt";
    public const string CognitoScheme = "CognitoJwt";
    public const string GuestScheme = "GuestJwt";

    public static IServiceCollection AddLunchSyncAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var cognitoIssuer = configuration["Cognito:Issuer"];
        var cognitoClientId = configuration["Cognito:ClientId"];
        var expectedTokenUse = configuration["Cognito:TokenUse"] ?? "id";

        var guestIssuer = configuration["GuestTokens:Issuer"] ?? "LunchSync.Api";
        var guestAudience = configuration["GuestTokens:Audience"] ?? "LunchSync.Guests";
        var guestSigningKey = configuration["GuestTokens:SigningKey"]
            ?? "change-me-to-a-long-random-secret-with-at-least-32-chars";

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = CombinedScheme;
            options.DefaultChallengeScheme = CombinedScheme;
        })
        .AddPolicyScheme(CombinedScheme, CombinedScheme, options =>
        {
            options.ForwardDefaultSelector = context =>
            {
                // Guest gui token qua header rieng, con lai mac dinh la bearer token.
                if (context.Request.Headers.ContainsKey(AuthHeaderNames.GuestToken))
                {
                    return GuestScheme;
                }

                return CognitoScheme;
            };
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
                    // Them actor type de policy dung chung cho host va guest.
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
        })
        .AddJwtBearer(GuestScheme, options =>
        {
            options.MapInboundClaims = false;
            // Guest token la JWT noi bo ky bang symmetric key.
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = guestIssuer,
                ValidateAudience = true,
                ValidAudience = guestAudience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(1),
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(guestSigningKey)),
                NameClaimType = AuthClaimTypes.Nickname
            };
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    // Guest token di bang header rieng de khong trung voi bearer token.
                    context.Token = context.Request.Headers[AuthHeaderNames.GuestToken].FirstOrDefault();
                    return Task.CompletedTask;
                },
                OnTokenValidated = context =>
                {
                    // Guest principal cung duoc chuan hoa ve claim actor_type.
                    var identity = context.Principal?.Identity as ClaimsIdentity;
                    if (identity is null)
                    {
                        context.Fail("Missing identity.");
                        return Task.CompletedTask;
                    }

                    var subject = context.Principal?.FindFirst("sub")?.Value;
                    if (string.IsNullOrWhiteSpace(subject))
                    {
                        context.Fail("Invalid guest token.");
                        return Task.CompletedTask;
                    }

                    if (!identity.HasClaim(claim => claim.Type == AuthClaimTypes.ActorType))
                    {
                        identity.AddClaim(new Claim(AuthClaimTypes.ActorType, AuthActorTypes.Guest));
                    }

                    return Task.CompletedTask;
                }
            };
        });

        return services;
    }
}
