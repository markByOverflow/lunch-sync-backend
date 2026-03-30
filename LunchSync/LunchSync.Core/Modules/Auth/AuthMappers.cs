using LunchSync.Core.Modules.Auth.Entities;

namespace LunchSync.Core.Modules.Auth;

public static class AuthMappers
{
    public static AuthResponse ToAuthResponse(this User user)
    {
        return new AuthResponse(user.Id, user.Email);
    }
}
