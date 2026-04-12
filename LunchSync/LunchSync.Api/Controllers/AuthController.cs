using LunchSync.Core.Common.Auth;
using LunchSync.Core.Common.Interfaces;
using LunchSync.Core.Modules.Auth;
using LunchSync.Core.Modules.Auth.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace LunchSync.Api.Controllers;

[ApiController]
[Route("api/auth")]
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
    [EnableRateLimiting("auth-public")]
    [HttpPost("register")]
    [ProducesResponseType(typeof(RegisterResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequest? request,
        CancellationToken cancellationToken)
    {
        var response = await _authService.RegisterAsync(
            request ?? new RegisterRequest(string.Empty, string.Empty, null),
            cancellationToken);
        return StatusCode(StatusCodes.Status201Created, response);
    }

    [AllowAnonymous]
    [EnableRateLimiting("auth-public")]
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest? request,
        CancellationToken cancellationToken)
    {
        var response = await _authService.LoginAsync(
            request ?? new LoginRequest(string.Empty, string.Empty),
            cancellationToken);
        return Ok(response);
    }

}
