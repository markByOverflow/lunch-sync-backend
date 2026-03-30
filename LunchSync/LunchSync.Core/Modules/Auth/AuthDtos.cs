namespace LunchSync.Core.Modules.Auth;

public sealed record AuthResponse(
    Guid UserId,
    string Email
);
