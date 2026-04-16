using LunchSync.Core.Modules.Auth.Entities;

namespace LunchSync.Core.Modules.Auth;

public static class AuthMappers
{
    public static RegisterResponse ToRegisterResponse(this User user, string message)
    {
        return new RegisterResponse(
            user.Email,
            user.FullName,
            message
        );
    }

    public static LoginResponse ToLoginResponse(this User user, string accessToken, int expiresIn)
    {
        return new LoginResponse(
            accessToken,
            expiresIn,
            user.Id,
            user.Email,
            user.FullName,
            user.Role.ToString().ToLowerInvariant()
        );
    }
}
