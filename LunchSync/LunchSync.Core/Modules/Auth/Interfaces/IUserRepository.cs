using LunchSync.Core.Modules.Auth.Entities;

namespace LunchSync.Core.Modules.Auth.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByCognitoSubAsync(string sub);
    Task AddAsync(User user);
}
