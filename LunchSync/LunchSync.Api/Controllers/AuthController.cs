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
    private readonly IUserRepository _userRepository;

    public AuthController(
        ICurrentUserService currentUser,
        IAuthService authService,
        IUserRepository userRepository)
    {
        _currentUser = currentUser;
        _authService = authService;
        _userRepository = userRepository;
    }

    [AllowAnonymous]
    [HttpPost("callback")]
    [ProducesResponseType(typeof(AuthTokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ExchangeCode(
        [FromQuery] string code,
        CancellationToken cancellationToken)
    {
        var request = new AuthCallbackRequest(code);
        var response = await _authService.ExchangeCodeAsync(request, cancellationToken);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("refresh")]
    [ProducesResponseType(typeof(AuthTokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RefreshToken(
        [FromBody] RefreshTokenRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _authService.RefreshTokenAsync(request, cancellationToken);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("revoke")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RevokeToken(
        [FromBody] RevokeTokenRequest request,
        CancellationToken cancellationToken)
    {
        await _authService.RevokeTokenAsync(request, cancellationToken);
        return NoContent();
    }

    [Authorize(Policy = AuthPolicies.CognitoUser)]
    [HttpGet("me")]
    public async Task<IActionResult> Me(CancellationToken cancellationToken)
    {
        if (!_currentUser.IsAuthenticated)
        {
            return Unauthorized();
        }

        var localUser = string.IsNullOrWhiteSpace(_currentUser.CognitoSub)
            ? null
            : await _userRepository.GetByCognitoSubAsync(_currentUser.CognitoSub, cancellationToken);

        return Ok(new CurrentUserResponse(
            localUser?.Id,
            _currentUser.CognitoSub,
            localUser?.Email ?? _currentUser.Email,
            localUser?.FullName ?? _currentUser.Name,
            localUser?.Role.ToString().ToLowerInvariant() ?? _currentUser.Role,
            localUser?.IsActive ?? _currentUser.IsActive));
    }
}
