using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LunchSync.Core.Common.Auth;
using LunchSync.Core.Modules.Auth;
using LunchSync.Core.Modules.Auth.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LunchSync.Infrastructure.Auth;

public sealed class GuestTokenService : IGuestTokenService
{
    private readonly GuestTokenOptions _options;

    public GuestTokenService(IOptions<GuestTokenOptions> options)
    {
        _options = options.Value;
    }

    public GuestAccessTokenResponse IssueToken(GuestAccessTokenRequest request)
    {
        // Moi guest duoc cap mot subject rieng de client co the giu identity tam thoi.
        return IssueTokenForSubject(request, $"guest:{Guid.NewGuid():N}");
    }

    public GuestAccessTokenResponse IssueTokenForSubject(GuestAccessTokenRequest request, string guestSubject)
    {
        var nickname = request.Nickname?.Trim();
        if (string.IsNullOrWhiteSpace(nickname))
        {
            throw new InvalidOperationException("Guest nickname is required.");
        }

        if (nickname.Length > 20)
        {
            throw new InvalidOperationException("Guest nickname must be 20 characters or fewer.");
        }

        var now = DateTime.UtcNow;
        var expiresAt = now.AddMinutes(_options.ExpiryMinutes);
        var normalizedSubject = (guestSubject ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(normalizedSubject))
        {
            throw new InvalidOperationException("Guest subject is required.");
        }

        if (string.IsNullOrWhiteSpace(_options.SigningKey) || _options.SigningKey.Length < 32)
        {
            throw new InvalidOperationException("Guest token signing key must be at least 32 characters.");
        }

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SigningKey));

        // Guest token hien chi mang identity co ban, chua bind voi session.
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, normalizedSubject),
            new(AuthClaimTypes.ActorType, AuthActorTypes.Guest),
            new(AuthClaimTypes.Nickname, nickname)
        };

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = _options.Issuer,
            Audience = _options.Audience,
            NotBefore = now,
            Expires = expiresAt,
            SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
        };

        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateToken(descriptor);

        return new GuestAccessTokenResponse(
            handler.WriteToken(token),
            expiresAt,
            AuthHeaderNames.GuestToken
        );
    }
}
