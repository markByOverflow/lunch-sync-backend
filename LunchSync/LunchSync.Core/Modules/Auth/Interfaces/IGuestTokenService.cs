namespace LunchSync.Core.Modules.Auth.Interfaces;

public interface IGuestTokenService
{
    GuestAccessTokenResponse IssueToken(GuestAccessTokenRequest request);
    GuestAccessTokenResponse IssueTokenForSubject(GuestAccessTokenRequest request, string guestSubject);
}
