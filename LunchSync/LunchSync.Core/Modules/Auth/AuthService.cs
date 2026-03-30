using LunchSync.Core.Common.Enums;
using LunchSync.Core.Modules.Auth.Entities;
using LunchSync.Core.Modules.Auth.Interfaces;

namespace LunchSync.Core.Modules.Auth;

public sealed class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<AuthResponse> HandleLoginAsync(string cognitoSub, string email, string? name)
    {
        var user = await _userRepository.GetByCognitoSubAsync(cognitoSub);
        if (user == null)
        {
            user = new User
            {
                CognitoSub = cognitoSub,
                Email = email,
                FullName = string.IsNullOrWhiteSpace(name) ? null : name,
                Role = UserRole.User
            };

            await _userRepository.AddAsync(user);
        }

        return new AuthResponse(user.Id, user.Email);
    }
}
