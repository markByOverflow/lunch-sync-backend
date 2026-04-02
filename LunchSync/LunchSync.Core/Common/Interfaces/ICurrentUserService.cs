namespace LunchSync.Core.Common.Interfaces;

public interface ICurrentUserService
{
    // Backward-compatible alias: this remains Cognito sub for existing flows.
    string? UserId { get; }
    string? CognitoSub { get; }
    Guid? LocalUserId { get; }
    string? ActorType { get; }
    string? Email { get; }
    string? Name { get; }
    string? Role { get; }
    bool IsActive { get; }
    bool IsAuthenticated { get; }
}
