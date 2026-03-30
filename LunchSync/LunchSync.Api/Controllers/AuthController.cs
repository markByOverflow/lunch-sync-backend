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

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequest? request,
        CancellationToken cancellationToken)
    {
        try
        {
            // Public register se tao user tren Cognito va tao local user cho app.
            var response = await _authService.RegisterAsync(
                request ?? new RegisterRequest(string.Empty, string.Empty, null),
                cancellationToken);
            return StatusCode(StatusCodes.Status201Created, response);
        }
        catch (InvalidOperationException ex)
        {
            return ValidationProblem(detail: ex.Message);
        }
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest? request,
        CancellationToken cancellationToken)
    {
        try
        {
            // Login se lay token tu Cognito va dong bo local user neu can.
            var response = await _authService.LoginAsync(
                request ?? new LoginRequest(string.Empty, string.Empty),
                cancellationToken);
            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return ValidationProblem(detail: ex.Message);
        }
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
