using System.Security.Cryptography;
using LunchSync.Core.Common.Auth;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LunchSync.Api.Authentication;

public sealed class AlbPublicKeyProvider
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _cache;
    private readonly AlbOidcOptions _options;

    public AlbPublicKeyProvider(
        HttpClient httpClient,
        IMemoryCache cache,
        IOptions<AlbOidcOptions> options)
    {
        _httpClient = httpClient;
        _cache = cache;
        _options = options.Value;
    }

    public async Task<SecurityKey> GetSigningKeyAsync(
        string region,
        string keyId,
        CancellationToken cancellationToken)
    {
        var cacheKey = $"alb-oidc-key:{region}:{keyId}";
        if (_cache.TryGetValue(cacheKey, out SecurityKey? cachedKey) && cachedKey is not null)
        {
            return cachedKey;
        }

        var pem = await _httpClient.GetStringAsync(
            BuildPublicKeyUrl(region, keyId),
            cancellationToken);

        var ecdsa = ECDsa.Create();
        ecdsa.ImportFromPem(pem);

        var securityKey = new ECDsaSecurityKey(ecdsa)
        {
            KeyId = keyId
        };

        _cache.Set(
            cacheKey,
            securityKey,
            TimeSpan.FromMinutes(Math.Max(1, _options.PublicKeyCacheMinutes)));

        return securityKey;
    }

    private static string BuildPublicKeyUrl(string region, string keyId) =>
        $"https://public-keys.auth.elb.{region}.amazonaws.com/{keyId}";
}
