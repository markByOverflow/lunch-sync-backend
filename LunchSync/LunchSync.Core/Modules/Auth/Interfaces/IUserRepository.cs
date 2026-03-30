using LunchSync.Core.Modules.Auth.Entities;

namespace LunchSync.Core.Modules.Auth.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByCognitoSubAsync(string sub, CancellationToken cancellationToken = default);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> ExistsByCognitoSubAsync(string sub, CancellationToken cancellationToken = default);
    Task<User> AddAsync(User user, CancellationToken cancellationToken = default);
    Task<User> UpdateAsync(User user, CancellationToken cancellationToken = default);
}
