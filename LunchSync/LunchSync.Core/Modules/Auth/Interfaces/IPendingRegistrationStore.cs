namespace LunchSync.Core.Modules.Auth.Interfaces;

public interface IPendingRegistrationStore
{
    Task SaveAsync(CognitoRegisterResult registration, CancellationToken cancellationToken = default);
    Task<CognitoRegisterResult?> GetAsync(string email, CancellationToken cancellationToken = default);
    Task RemoveAsync(string email, CancellationToken cancellationToken = default);
}
