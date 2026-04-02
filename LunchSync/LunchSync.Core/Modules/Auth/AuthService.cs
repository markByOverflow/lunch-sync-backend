using EmailAddressAttribute = System.ComponentModel.DataAnnotations.EmailAddressAttribute;
using LunchSync.Core.Exceptions;
using LunchSync.Core.Modules.Auth.Entities;
using LunchSync.Core.Modules.Auth.Interfaces;

namespace LunchSync.Core.Modules.Auth;

public sealed class AuthService : IAuthService
{
    private static readonly EmailAddressAttribute EmailValidator = new();
    private readonly IUserRepository _userRepository;
    private readonly ICognitoAuthProvider _cognitoAuthProvider;

    public AuthService(IUserRepository userRepository, ICognitoAuthProvider cognitoAuthProvider)
    {
        _userRepository = userRepository;
        _cognitoAuthProvider = cognitoAuthProvider;
    }

    public async Task<RegisterResponse> RegisterAsync(
        RegisterRequest request,
        CancellationToken cancellationToken = default)
    {
        var normalizedEmail = NormalizeAndValidateRegisterRequest(request);

        var cognitoResult = await _cognitoAuthProvider.RegisterAsync(
            request with { Email = normalizedEmail },
            cancellationToken);

        var existingBySub = await _userRepository.GetByCognitoSubAsync(cognitoResult.CognitoSub, cancellationToken);
        if (existingBySub is not null)
        {
            if (!string.Equals(existingBySub.Email, cognitoResult.Email, StringComparison.OrdinalIgnoreCase))
            {
                throw new AuthIdentityConflictException(cognitoResult.Email);
            }

            return existingBySub.ToRegisterResponse("Đăng ký thành công. Vui lòng xác nhận email nếu cần.");
        }

        var existingByEmail = await _userRepository.GetByEmailAsync(cognitoResult.Email, cancellationToken);
        if (existingByEmail is not null)
        {
            throw new AuthIdentityConflictException(cognitoResult.Email);
        }

        var createdUser = await _userRepository.AddAsync(new User
        {
            CognitoSub = cognitoResult.CognitoSub,
            Email = cognitoResult.Email,
            FullName = cognitoResult.FullName?.Trim()
        }, cancellationToken);

        return createdUser.ToRegisterResponse("Đăng ký thành công. Vui lòng xác nhận email nếu cần.");
    }

    public async Task<LoginResponse> LoginAsync(
        LoginRequest request,
        CancellationToken cancellationToken = default)
    {
        var normalizedEmail = NormalizeAndValidateLoginRequest(request);

        var cognitoResult = await _cognitoAuthProvider.LoginAsync(
            request with { Email = normalizedEmail },
            cancellationToken);

        var user = await ResolveLocalUserForLoginAsync(cognitoResult, cancellationToken);

        if (!user.IsActive)
        {
            throw new InactiveUserException();
        }

        return user.ToLoginResponse(cognitoResult.AccessToken, cognitoResult.ExpiresIn);
    }

    private static string NormalizeAndValidateRegisterRequest(RegisterRequest request)
    {
        var normalizedEmail = request.Email?.Trim().ToLowerInvariant();
        var details = new Dictionary<string, string>();

        if (string.IsNullOrWhiteSpace(normalizedEmail))
        {
            details["email"] = "Email là bắt buộc.";
        }
        else if (!EmailValidator.IsValid(normalizedEmail))
        {
            details["email"] = "Email không đúng định dạng.";
        }

        if (string.IsNullOrWhiteSpace(request.Password))
        {
            details["password"] = "Mật khẩu là bắt buộc.";
        }
        else if (!IsValidPassword(request.Password))
        {
            details["password"] = "Mật khẩu phải có ít nhất 8 ký tự, gồm 1 chữ hoa, 1 số và 1 ký tự đặc biệt.";
        }

        if (details.Count > 0)
        {
            throw new ValidationException("Dữ liệu đăng ký không hợp lệ.", details);
        }

        return normalizedEmail!;
    }

    private static string NormalizeAndValidateLoginRequest(LoginRequest request)
    {
        var normalizedEmail = request.Email?.Trim().ToLowerInvariant();
        var details = new Dictionary<string, string>();

        if (string.IsNullOrWhiteSpace(normalizedEmail))
        {
            details["email"] = "Email là bắt buộc.";
        }
        else if (!EmailValidator.IsValid(normalizedEmail))
        {
            details["email"] = "Email không đúng định dạng.";
        }

        if (string.IsNullOrWhiteSpace(request.Password))
        {
            details["password"] = "Mật khẩu là bắt buộc.";
        }

        if (details.Count > 0)
        {
            throw new ValidationException("Dữ liệu đăng nhập không hợp lệ.", details);
        }

        return normalizedEmail!;
    }

    private async Task<User> ResolveLocalUserForLoginAsync(
        CognitoLoginResult cognitoResult,
        CancellationToken cancellationToken)
    {
        var existingBySub = await _userRepository.GetByCognitoSubAsync(cognitoResult.CognitoSub, cancellationToken);
        if (existingBySub is not null)
        {
            return await SyncUserProfileAsync(existingBySub, cognitoResult, cancellationToken);
        }

        var existingByEmail = await _userRepository.GetByEmailAsync(cognitoResult.Email, cancellationToken);
        if (existingByEmail is not null)
        {
            if (!string.Equals(existingByEmail.CognitoSub, cognitoResult.CognitoSub, StringComparison.Ordinal))
            {
                throw new AuthIdentityConflictException(cognitoResult.Email);
            }

            return await SyncUserProfileAsync(existingByEmail, cognitoResult, cancellationToken);
        }

        return await _userRepository.AddAsync(new User
        {
            CognitoSub = cognitoResult.CognitoSub,
            Email = cognitoResult.Email,
            FullName = cognitoResult.FullName?.Trim()
        }, cancellationToken);
    }

    private async Task<User> SyncUserProfileAsync(
        User user,
        CognitoLoginResult cognitoResult,
        CancellationToken cancellationToken)
    {
        var normalizedFullName = cognitoResult.FullName?.Trim();
        var hasChanges =
            !string.Equals(user.Email, cognitoResult.Email, StringComparison.OrdinalIgnoreCase)
            || !string.Equals(user.FullName, normalizedFullName, StringComparison.Ordinal);

        if (!hasChanges)
        {
            return user;
        }

        user.Email = cognitoResult.Email;
        user.FullName = normalizedFullName;
        return await _userRepository.UpdateAsync(user, cancellationToken);
    }

    private static bool IsValidPassword(string password)
    {
        if (password.Length < 8)
        {
            return false;
        }

        var hasUpper = password.Any(char.IsUpper);
        var hasDigit = password.Any(char.IsDigit);
        var hasSpecial = password.Any(ch => !char.IsLetterOrDigit(ch));

        return hasUpper && hasDigit && hasSpecial;
    }
}
