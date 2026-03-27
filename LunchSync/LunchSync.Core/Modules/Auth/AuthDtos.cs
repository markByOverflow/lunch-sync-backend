namespace LunchSync.Core.Modules.Auth;

public sealed record CurrentActorResponse(
    string? UserId,
    string? Email,
    string? Name,
    string? ActorType,
    IReadOnlyList<string> Roles
);

public sealed record RegistrationStatusResponse(
    string CognitoSub,
    bool IsRegistered
);

public sealed record RegisterCurrentUserRequest(
    string? DisplayName
);

public sealed record RegisterCurrentUserResponse(
    Guid RegistrationId,
    string CognitoSub,
    string Email,
    string? FullName,
    bool CreatedNewUser
);

public sealed record GuestAccessTokenRequest(
    string Nickname
);

public sealed record GuestAccessTokenResponse(
    string Token,
    DateTime ExpiresAtUtc,
    string HeaderName
);
