using LunchSync.Core.Modules.Auth.Entities;
using LunchSync.Core.Modules.Auth.Interfaces;

namespace LunchSync.Core.Modules.Auth;

public sealed class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ICognitoAuthProvider _cognitoAuthProvider;

    public AuthService(IUserRepository userRepository, ICognitoAuthProvider cognitoAuthProvider)
    {
        _userRepository = userRepository;
        _cognitoAuthProvider = cognitoAuthProvider;
    }

    public async Task<RegistrationStatusResponse> GetRegistrationStatusAsync(
        string cognitoSub,
        CancellationToken cancellationToken = default)
    {
        // Kiem tra xem user Cognito nay da co ban ghi local chua.
        var isRegistered = await _userRepository.ExistsByCognitoSubAsync(cognitoSub, cancellationToken);

        return new RegistrationStatusResponse(cognitoSub, isRegistered);
    }

    public async Task<RegisterResponse> RegisterAsync(
        RegisterRequest request,
        CancellationToken cancellationToken = default)
    {
        // Dang ky qua Cognito truoc, sau do tao ban ghi local cho app.
        var normalizedEmail = request.Email?.Trim().ToLowerInvariant();
        if (string.IsNullOrWhiteSpace(normalizedEmail))
        {
            throw new InvalidOperationException("Email is required.");
        }

        if (string.IsNullOrWhiteSpace(request.Password))
        {
            throw new InvalidOperationException("Password is required.");
        }

        var cognitoResult = await _cognitoAuthProvider.RegisterAsync(
            request with { Email = normalizedEmail },
            cancellationToken);

        var existingUser = await _userRepository.GetByCognitoSubAsync(cognitoResult.CognitoSub, cancellationToken)
            ?? await _userRepository.GetByEmailAsync(cognitoResult.Email, cancellationToken);

        if (existingUser is not null)
        {
            return existingUser.ToRegisterResponse("Dang ky thanh cong.");
        }

        var createdUser = await _userRepository.AddAsync(new User
        {
            CognitoSub = cognitoResult.CognitoSub,
            Email = cognitoResult.Email,
            FullName = cognitoResult.FullName?.Trim()
        }, cancellationToken);

        return createdUser.ToRegisterResponse("Dang ky thanh cong. Vui long xac nhan email neu can.");
    }

    public async Task<LoginResponse> LoginAsync(
        LoginRequest request,
        CancellationToken cancellationToken = default)
    {
        // Dang nhap qua Cognito, sau do dong bo local user de session dung duoc.
        var normalizedEmail = request.Email?.Trim().ToLowerInvariant();
        if (string.IsNullOrWhiteSpace(normalizedEmail))
        {
            throw new InvalidOperationException("Email is required.");
        }

        if (string.IsNullOrWhiteSpace(request.Password))
        {
            throw new InvalidOperationException("Password is required.");
        }

        var cognitoResult = await _cognitoAuthProvider.LoginAsync(
            request with { Email = normalizedEmail },
            cancellationToken);

        var user = await _userRepository.GetByCognitoSubAsync(cognitoResult.CognitoSub, cancellationToken)
            ?? await _userRepository.GetByEmailAsync(cognitoResult.Email, cancellationToken);

        if (user is null)
        {
            user = await _userRepository.AddAsync(new User
            {
                CognitoSub = cognitoResult.CognitoSub,
                Email = cognitoResult.Email,
                FullName = cognitoResult.FullName?.Trim()
            }, cancellationToken);
        }
        else if (!string.Equals(user.CognitoSub, cognitoResult.CognitoSub, StringComparison.Ordinal)
            || !string.Equals(user.Email, cognitoResult.Email, StringComparison.OrdinalIgnoreCase)
            || !string.Equals(user.FullName, cognitoResult.FullName?.Trim(), StringComparison.Ordinal))
        {
            user.CognitoSub = cognitoResult.CognitoSub;
            user.Email = cognitoResult.Email;
            user.FullName = cognitoResult.FullName?.Trim();
            user = await _userRepository.UpdateAsync(user, cancellationToken);
        }

        // App hien dang xac thuc bang token co day du claim profile cho backend.
        return user.ToLoginResponse(cognitoResult.AppBearerToken, cognitoResult.ExpiresIn);
    }
}
