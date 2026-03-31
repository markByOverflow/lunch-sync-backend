using LunchSync.Core.Common.Auth;
using LunchSync.Core.Common.Interfaces;
using LunchSync.Core.Common.ValueObjects;
using LunchSync.Core.Exceptions;
using LunchSync.Core.Modules.Auth.Interfaces;
using LunchSync.Core.Modules.Sessions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LunchSync.Api.Controllers;

//[Authorize]
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
    [ProducesResponseType(typeof(CreateSessionRes), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateSessionReq request, CancellationToken ct)
    {
        // Controller map principal -> local user id, business rule de service xu ly.
        var hostId = await GetCurrentHostIdAsync(ct);
        if (hostId is null)
        {
            return Unauthorized();
        }

        var result = await _sessionService.CreateSessionAsync(request, hostId.Value);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("{pin}/join")]
    [ProducesResponseType(typeof(JoinRes), StatusCodes.Status200OK)]
    public async Task<IActionResult> JoinAsync([FromRoute] string pin, [FromBody] JoinReq request, CancellationToken ct)
    {
        // Guest join bang PIN va nickname, chua can host JWT.
        var validPin = Pin.Create(pin);
        Guid? UserId = null; // Giả sử UserId được lấy từ Token/Identity. Ở đây tạm lấy Guid mẫu.
        var result = await _sessionService.JoinSessionAsync(UserId, validPin.Value, request, ct);
        return Ok(result);
    }

    // Chi host moi duoc bat dau voting.
    [Authorize(Policy = AuthPolicies.CognitoUser)]
    [HttpPost("{pin}/start")]
    [ProducesResponseType(typeof(SessionStartRes), StatusCodes.Status200OK)]
    public async Task<IActionResult> StartAsync([FromRoute] string pin, CancellationToken ct)
    {
        var hostId = await GetCurrentHostIdAsync(ct);
        if (hostId is null)
        {
            return Unauthorized();
        }

        var validPin = Pin.Create(pin);
        var result = await _sessionService.StartSessionAsync(validPin.Value, hostId.Value, ct);
        return Ok(result);
    }

    // Chi host moi duoc huy session.
    [Authorize(Policy = AuthPolicies.CognitoUser)]
    [HttpPost("{pin}/cancel")]
    public async Task<IActionResult> CancelAsync(
        [FromRoute] string pin,
        CancellationToken ct)
    {
        var hostId = await GetCurrentHostIdAsync(ct);
        if (hostId is null)
        {
            return Unauthorized();
        }

        await _sessionService.CancelSessionAsync(pin, hostId.Value, ct);
        return Ok();
    }

    [AllowAnonymous]
    [ProducesResponseType(typeof(SessionStatusDto), StatusCodes.Status200OK)]
    [HttpGet("{pin}/{sessionId:guid}/status")]
    public async Task<IActionResult> GetStatusAsync([FromRoute] string pin, [FromRoute] Guid sessionId, CancellationToken ct)
    {
        var validPin = Pin.Create(pin);
        var session = await _sessionService.GetSessionAsync(validPin.Value, ct) ?? throw new SessionNotFoundException(pin);

        if (session.Id != sessionId)

        {
            throw new EntityNotFoundException("session", sessionId);
        }

        return Ok(session.ToStatusDto());
    }

    [AllowAnonymous]
    [HttpGet("{pin}/{sessionId:guid}/info")]
    [ProducesResponseType(typeof(SessionInfoDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetInfoAsync([FromRoute] string pin, [FromRoute] Guid sessionId, CancellationToken ct)
    {
        var validPin = Pin.Create(pin);
        var session = await _sessionService.GetSessionAsync(validPin.Value, ct) ?? throw new SessionNotFoundException(pin);
        if (session.Id != sessionId)
        {
            throw new EntityNotFoundException("session", sessionId);
        }

        return Ok(session.ToInfoDto());
    }

    [AllowAnonymous]
    [HttpGet("history/{sessionId:guid}")]
    [ProducesResponseType(typeof(SessionInfoDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetHistoryAsync([FromRoute] Guid sessionId, CancellationToken ct)
    {
        var session = await _sessionService.GetSessionHistoryAsync(sessionId, ct) ?? throw new SessionNotFoundByIdException(sessionId);
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
