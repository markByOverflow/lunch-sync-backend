using LunchSync.Core.Common.Auth;
using LunchSync.Core.Common.Interfaces;
using LunchSync.Core.Common.ValueObjects;
using LunchSync.Core.Exceptions;
using LunchSync.Core.Modules.Auth.Interfaces;
using LunchSync.Core.Modules.Sessions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LunchSync.Api.Controllers;

[ApiController]
[Route("api/sessions")]
public class SessionsController : ControllerBase
{
    private readonly ISessionService _sessionService;
    private readonly ICurrentUserService _currentUser;
    private readonly IUserRepository _userRepository;

    public SessionsController(
        ISessionService sessionService,
        ICurrentUserService currentUser,
        IUserRepository userRepository)
    {
        _sessionService = sessionService;
        _currentUser = currentUser;
        _userRepository = userRepository;
    }

    // Endpoint nay chi danh cho host da dang nhap.
    [Authorize(Policy = AuthPolicies.CognitoUser)]
    [HttpPost]
    public async Task<IActionResult> CreateAsync(
        [FromBody] CreateSessionReq request,
        CancellationToken cancellationToken)
    {
        // Controller map principal -> local user id, business rule de service xu ly.
        var hostId = await GetCurrentHostIdAsync(cancellationToken);
        if (hostId is null)
        {
            return Unauthorized();
        }

        var result = await _sessionService.CreateSessionAsync(request, hostId.Value);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("{pin}/join")]
    public async Task<IActionResult> JoinAsync([FromRoute] string pin, [FromBody] JoinReq request)
    {
        // Guest join bang PIN va nickname, chua can host JWT.
        var validPin = Pin.Create(pin);
        var result = await _sessionService.JoinSessionAsync(validPin.Value, request);
        return Ok(result);
    }

    // Chi host moi duoc bat dau voting.
    [Authorize(Policy = AuthPolicies.CognitoUser)]
    [HttpPost("{pin}/start")]
    public async Task<IActionResult> StartAsync(
        [FromRoute] string pin,
        CancellationToken cancellationToken)
    {
        var hostId = await GetCurrentHostIdAsync(cancellationToken);
        if (hostId is null)
        {
            return Unauthorized();
        }

        var validPin = Pin.Create(pin);
        var result = await _sessionService.StartSessionAsync(validPin.Value, hostId.Value);
        return Ok(result);
    }

    // Chi host moi duoc huy session.
    [Authorize(Policy = AuthPolicies.CognitoUser)]
    [HttpPost("{pin}/cancel")]
    public async Task<IActionResult> CancelAsync(
        [FromRoute] string pin,
        CancellationToken cancellationToken)
    {
        var hostId = await GetCurrentHostIdAsync(cancellationToken);
        if (hostId is null)
        {
            return Unauthorized();
        }

        await _sessionService.CancelSessionAsync(pin, hostId.Value);
        return Ok();
    }

    [AllowAnonymous]
    [HttpGet("{pin}/{sessionId:guid}/status")]
    public async Task<IActionResult> GetStatusAsync([FromRoute] string pin, [FromRoute] Guid sessionId)
    {
        var validPin = Pin.Create(pin);
        var session = await _sessionService.GetSessionAsync(validPin.Value);

        if (session == null || session.Id != sessionId)
        {
            throw new EntityNotFoundException("session", sessionId);
        }

        return Ok(session.ToStatusDto());
    }

    [AllowAnonymous]
    [HttpGet("{pin}/{sessionId:guid}/info")]
    public async Task<IActionResult> GetInfoAsync([FromRoute] string pin, [FromRoute] Guid sessionId)
    {
        var validPin = Pin.Create(pin);
        var session = await _sessionService.GetSessionAsync(validPin.Value);
        if (session == null || session.Id != sessionId)
        {
            throw new EntityNotFoundException("session", sessionId);
        }

        return Ok(session.ToInfoDto());
    }

    [AllowAnonymous]
    [HttpGet("history/{sessionId:guid}")]
    public async Task<IActionResult> GetHistoryAsync([FromRoute] Guid sessionId)
    {
        var session = await _sessionService.GetSessionHistoryAsync(sessionId)
            ?? throw new EntityNotFoundException("session", sessionId);
        return Ok(session.ToInfoDto());
    }

    private async Task<Guid?> GetCurrentHostIdAsync(CancellationToken cancellationToken)
    {
        // Token Cognito chi mang sub, nen can map ve user local trong database.
        if (string.IsNullOrWhiteSpace(_currentUser.UserId))
        {
            return null;
        }

        var user = await _userRepository.GetByCognitoSubAsync(_currentUser.UserId, cancellationToken);
        return user?.Id;
    }
}
