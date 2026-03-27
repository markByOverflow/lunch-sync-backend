namespace LunchSync.Infrastructure.Persistence.Caching;

public static class RedisKeyBuilder
{
    private const string Root = "session";
    private const string PinPool = "pin_pool";
    public static string PinAvailablePool() => $"{PinPool}:available";
    public static string Data(string pin) => $"{Root}:{pin}:data";
    public static string Participants(string pin) => $"{Root}:{pin}:participants";
    public static string Names(string pin) => $"{Root}:{pin}:names";
}
