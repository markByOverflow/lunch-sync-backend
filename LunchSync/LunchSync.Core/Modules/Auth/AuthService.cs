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
    private readonly IPendingRegistrationStore _pendingRegistrationStore;

    public AuthService(
        IUserRepository userRepository,
        ICognitoAuthProvider cognitoAuthProvider,
        IPendingRegistrationStore pendingRegistrationStore)
    {
        _userRepository = userRepository;
        _cognitoAuthProvider = cognitoAuthProvider;
        _pendingRegistrationStore = pendingRegistrationStore;
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

            return new RegisterResponse(
                existingBySub.Email,
                existingBySub.FullName,
                "Tai khoan da ton tai. Ban co the dang nhap.");
        }

        var existingByEmail = await _userRepository.GetByEmailAsync(cognitoResult.Email, cancellationToken);
        if (existingByEmail is not null)
        {
            if (!string.Equals(existingByEmail.CognitoSub, cognitoResult.CognitoSub, StringComparison.Ordinal))
            {
                throw new AuthIdentityConflictException(cognitoResult.Email);
            }

            return new RegisterResponse(
                existingByEmail.Email,
                existingByEmail.FullName,
                "Tai khoan da ton tai. Ban co the dang nhap.");
        }

        await _pendingRegistrationStore.SaveAsync(cognitoResult, cancellationToken);

        return new RegisterResponse(
            cognitoResult.Email,
            cognitoResult.FullName?.Trim(),
            "Dang ky thanh cong. Vui long xac thuc OTP de hoan tat.");
    }

    public async Task<VerifyOtpResponse> VerifyOtpAsync(
        VerifyOtpRequest request,
        CancellationToken cancellationToken = default)
    {
        var normalizedEmail = NormalizeAndValidateVerifyOtpRequest(request);
        var pendingRegistration = await _pendingRegistrationStore.GetAsync(normalizedEmail, cancellationToken);

        await _cognitoAuthProvider.ConfirmSignUpAsync(
            request with { Email = normalizedEmail, Otp = request.Otp.Trim() },
            cancellationToken);

        if (pendingRegistration is not null)
        {
            await SyncLocalUserForConfirmedRegistrationAsync(pendingRegistration, cancellationToken);
        }

        await _pendingRegistrationStore.RemoveAsync(normalizedEmail, cancellationToken);

        return new VerifyOtpResponse(
            normalizedEmail,
            "Xac thuc OTP thanh cong. Ban co the dang nhap.");
    }

    public async Task<ResendOtpResponse> ResendOtpAsync(
        ResendOtpRequest request,
        CancellationToken cancellationToken = default)
    {
        var normalizedEmail = NormalizeAndValidateResendOtpRequest(request);
        var pendingRegistration = await _pendingRegistrationStore.GetAsync(normalizedEmail, cancellationToken);

        await _cognitoAuthProvider.ResendConfirmationCodeAsync(
            request with { Email = normalizedEmail },
            cancellationToken);

        if (pendingRegistration is not null)
        {
            await _pendingRegistrationStore.SaveAsync(pendingRegistration, cancellationToken);
        }

        return new ResendOtpResponse(
            normalizedEmail,
            "Da gui lai ma OTP. Vui long kiem tra email.");
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
            details["email"] = "Email la bat buoc.";
        }
        else if (!EmailValidator.IsValid(normalizedEmail))
        {
            details["email"] = "Email khong dung dinh dang.";
        }

        if (string.IsNullOrWhiteSpace(request.Password))
        {
            details["password"] = "Mat khau la bat buoc.";
        }
        else if (!IsValidPassword(request.Password))
        {
            details["password"] = "Mat khau phai co it nhat 8 ky tu, gom 1 chu hoa, 1 so va 1 ky tu dac biet.";
        }

        if (details.Count > 0)
        {
            throw new ValidationException("Du lieu dang ky khong hop le.", details);
        }

        return normalizedEmail!;
    }

    private static string NormalizeAndValidateVerifyOtpRequest(VerifyOtpRequest request)
    {
        var normalizedEmail = request.Email?.Trim().ToLowerInvariant();
        var otp = request.Otp?.Trim();
        var details = new Dictionary<string, string>();

        if (string.IsNullOrWhiteSpace(normalizedEmail))
        {
            details["email"] = "Email la bat buoc.";
        }
        else if (!EmailValidator.IsValid(normalizedEmail))
        {
            details["email"] = "Email khong dung dinh dang.";
        }

        if (string.IsNullOrWhiteSpace(otp))
        {
            details["otp"] = "Ma OTP la bat buoc.";
        }
        else if (otp.Length < 6)
        {
            details["otp"] = "Ma OTP phai co it nhat 6 ky tu.";
        }

        if (details.Count > 0)
        {
            throw new ValidationException("Du lieu xac thuc OTP khong hop le.", details);
        }

        return normalizedEmail!;
    }

    private static string NormalizeAndValidateResendOtpRequest(ResendOtpRequest request)
    {
        var normalizedEmail = request.Email?.Trim().ToLowerInvariant();
        var details = new Dictionary<string, string>();

        if (string.IsNullOrWhiteSpace(normalizedEmail))
        {
            details["email"] = "Email la bat buoc.";
        }
        else if (!EmailValidator.IsValid(normalizedEmail))
        {
            details["email"] = "Email khong dung dinh dang.";
        }

        if (details.Count > 0)
        {
            throw new ValidationException("Du lieu gui lai OTP khong hop le.", details);
        }

        return normalizedEmail!;
    }

    private static string NormalizeAndValidateLoginRequest(LoginRequest request)
    {
        var normalizedEmail = request.Email?.Trim().ToLowerInvariant();
        var details = new Dictionary<string, string>();

        if (string.IsNullOrWhiteSpace(normalizedEmail))
        {
            details["email"] = "Email la bat buoc.";
        }
        else if (!EmailValidator.IsValid(normalizedEmail))
        {
            details["email"] = "Email khong dung dinh dang.";
        }

        if (string.IsNullOrWhiteSpace(request.Password))
        {
            details["password"] = "Mat khau la bat buoc.";
        }

        if (details.Count > 0)
        {
            throw new ValidationException("Du lieu dang nhap khong hop le.", details);
        }

        return normalizedEmail!;
    }

    private async Task<User> SyncLocalUserForConfirmedRegistrationAsync(
        CognitoRegisterResult registration,
        CancellationToken cancellationToken)
    {
        var existingBySub = await _userRepository.GetByCognitoSubAsync(registration.CognitoSub, cancellationToken);
        if (existingBySub is not null)
        {
            if (!string.Equals(existingBySub.Email, registration.Email, StringComparison.OrdinalIgnoreCase))
            {
                throw new AuthIdentityConflictException(registration.Email);
            }

            return await SyncRegisteredUserProfileAsync(existingBySub, registration, cancellationToken);
        }

        var existingByEmail = await _userRepository.GetByEmailAsync(registration.Email, cancellationToken);
        if (existingByEmail is not null)
        {
            if (!string.Equals(existingByEmail.CognitoSub, registration.CognitoSub, StringComparison.Ordinal))
            {
                throw new AuthIdentityConflictException(registration.Email);
            }

            return await SyncRegisteredUserProfileAsync(existingByEmail, registration, cancellationToken);
        }

        return await _userRepository.AddAsync(new User
        {
            CognitoSub = registration.CognitoSub,
            Email = registration.Email,
            FullName = registration.FullName?.Trim()
        }, cancellationToken);
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

    private async Task<User> SyncRegisteredUserProfileAsync(
        User user,
        CognitoRegisterResult registration,
        CancellationToken cancellationToken)
    {
        var normalizedFullName = registration.FullName?.Trim();
        var hasChanges =
            !string.Equals(user.Email, registration.Email, StringComparison.OrdinalIgnoreCase)
            || !string.Equals(user.FullName, normalizedFullName, StringComparison.Ordinal);

        if (!hasChanges)
        {
            return user;
        }

        user.Email = registration.Email;
        user.FullName = normalizedFullName;
        return await _userRepository.UpdateAsync(user, cancellationToken);
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
