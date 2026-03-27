namespace LunchSync.Core.Common.Auth;

public sealed class GuestTokenOptions
{
    public const string SectionName = "GuestTokens";

    public string Issuer { get; set; } = "LunchSync.Api";
    public string Audience { get; set; } = "LunchSync.Guests";
    public string SigningKey { get; set; } = "change-me-to-a-long-random-secret-with-at-least-32-chars";
    public int ExpiryMinutes { get; set; } = 240;
}
