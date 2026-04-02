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

    public AuthController(
        ICurrentUserService currentUser,
        IAuthService authService)
    {
        _currentUser = currentUser;
        _authService = authService;
    }

    [Authorize(Policy = AuthPolicies.CognitoUser)]
    [HttpGet("me")]
    public IActionResult Me()
    {
        if (!_currentUser.IsAuthenticated)
            return Unauthorized();

        return Ok(new CurrentUserResponse(
            _currentUser.LocalUserId,
            _currentUser.CognitoSub,
            _currentUser.Email,
            _currentUser.Name,
            _currentUser.Role,
            _currentUser.IsActive));
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

}
