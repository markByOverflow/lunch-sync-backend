namespace LunchSync.Core.Modules.Auth.Interfaces;

public interface IAuthService
{
    Task<RegistrationStatusResponse> GetRegistrationStatusAsync(string cognitoSub, CancellationToken cancellationToken = default);
    Task<RegisterCurrentUserResponse> RegisterCurrentUserAsync(RegisterCurrentUserRequest request, CancellationToken cancellationToken = default);
}
