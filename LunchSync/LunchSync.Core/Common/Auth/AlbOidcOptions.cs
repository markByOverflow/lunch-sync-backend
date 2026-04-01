namespace LunchSync.Core.Common.Auth;

public sealed class AlbOidcOptions
{
    public const string SectionName = "AlbOidc";

    public bool Enabled { get; set; }
    public string? Region { get; set; }
    public string? LoadBalancerArn { get; set; }
    public string? Issuer { get; set; }
    public string? ClientId { get; set; }
    public int PublicKeyCacheMinutes { get; set; } = 60;
    public int ClockSkewSeconds { get; set; } = 60;
}
