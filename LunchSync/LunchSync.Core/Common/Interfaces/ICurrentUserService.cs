namespace LunchSync.Core.Common.Interfaces;

using LunchSync.Core.Modules.Auth.Entities;

public interface ICurrentUserService
{
    Guid? UserId { get; }
    string? CognitoSub { get; }
    string? Email { get; }
    Task<User?> GetUserAsync();
}
