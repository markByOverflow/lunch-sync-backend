namespace LunchSync.Core.Common.Interfaces;

public interface ICurrentUserService
{
    string? UserId { get; }
    string? ActorType { get; }
    string? Email { get; }
    string? Name { get; }
    IReadOnlyList<string> Roles { get; }
    bool IsAuthenticated { get; }
    bool IsGuest { get; }
}
