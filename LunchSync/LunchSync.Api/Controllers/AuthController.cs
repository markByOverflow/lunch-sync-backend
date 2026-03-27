using LunchSync.Core.Common.Auth;
using LunchSync.Core.Common.Interfaces;
using LunchSync.Core.Modules.Auth;
using LunchSync.Core.Modules.Auth.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LunchSync.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AuthController : ControllerBase
{
    private readonly ICurrentUserService _currentUser;
    private readonly IAuthService _authService;
    private readonly IGuestTokenService _guestTokenService;

    public AuthController(
        ICurrentUserService currentUser,
        IAuthService authService,
        IGuestTokenService guestTokenService)
    {
        _currentUser = currentUser;
        _authService = authService;
        _guestTokenService = guestTokenService;
    }

    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        if (!_currentUser.IsAuthenticated)
            return Unauthorized();

        // Tra ve principal da duoc chuan hoa sau khi auth xong.
        return Ok(new CurrentActorResponse(
            _currentUser.UserId,
            _currentUser.Email,
            _currentUser.Name,
            _currentUser.ActorType,
            _currentUser.Roles));
    }

    [Authorize(Policy = AuthPolicies.CognitoUser)]
    [HttpGet("registration-status")]
    public async Task<IActionResult> RegistrationStatus(CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_currentUser.UserId))
            return Unauthorized();

        var response = await _authService.GetRegistrationStatusAsync(_currentUser.UserId, cancellationToken);
        return Ok(response);
    }

    [Authorize(Policy = AuthPolicies.CognitoUser)]
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterCurrentUserRequest? request,
        CancellationToken cancellationToken)
    {
        try
        {
            // Hien tai register nay chi tao local user tu principal Cognito da co san.
            var response = await _authService.RegisterCurrentUserAsync(
                request ?? new RegisterCurrentUserRequest(null),
                cancellationToken);
            if (response.CreatedNewUser)
            {
                return CreatedAtAction(nameof(RegistrationStatus), response);
            }

            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return ValidationProblem(detail: ex.Message);
        }
    }

    [AllowAnonymous]
    [HttpPost("guest-token")]
    public IActionResult IssueGuestToken([FromBody] GuestAccessTokenRequest? request)
    {
        if (request is null)
        {
            return ValidationProblem(detail: "Guest token request body is required.");
        }

        try
        {
            // Guest token duoc cap rieng de client guest goi cac endpoint public/guest.
            var response = _guestTokenService.IssueToken(request);
            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return ValidationProblem(detail: ex.Message);
        }
    }
}
