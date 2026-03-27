using LunchSync.Core.Common.Interfaces;
using LunchSync.Core.Modules.Auth.Entities;
using LunchSync.Core.Modules.Auth.Interfaces;

namespace LunchSync.Core.Modules.Auth;

public sealed class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUser;

    public AuthService(IUserRepository userRepository, ICurrentUserService currentUser)
    {
        _userRepository = userRepository;
        _currentUser = currentUser;
    }

    public async Task<RegistrationStatusResponse> GetRegistrationStatusAsync(
        string cognitoSub,
        CancellationToken cancellationToken = default)
    {
        // Kiem tra xem user Cognito nay da co ban ghi local chua.
        var isRegistered = await _userRepository.ExistsByCognitoSubAsync(cognitoSub, cancellationToken);

        return new RegistrationStatusResponse(cognitoSub, isRegistered);
    }

    public async Task<RegisterCurrentUserResponse> RegisterCurrentUserAsync(
        RegisterCurrentUserRequest request,
        CancellationToken cancellationToken = default)
    {
        // Flow hien tai chua tu dang ky Cognito, chi tao user local tu principal da dang nhap.
        var cognitoSub = _currentUser.UserId;
        if (string.IsNullOrWhiteSpace(cognitoSub))
        {
            throw new InvalidOperationException("Authenticated Cognito subject is required.");
        }

        var email = _currentUser.Email;
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new InvalidOperationException("Authenticated email claim is required.");
        }

        var existingUser = await _userRepository.GetByCognitoSubAsync(cognitoSub, cancellationToken);
        if (existingUser is not null)
        {
            return existingUser.ToRegisterResponse(createdNewUser: false);
        }

        var newUser = new User
        {
            CognitoSub = cognitoSub,
            Email = email,
            FullName = request.DisplayName?.Trim()
                ?? _currentUser.Name?.Trim()
        };

        var createdUser = await _userRepository.AddAsync(newUser, cancellationToken);
        return createdUser.ToRegisterResponse(createdNewUser: true);
    }
}
