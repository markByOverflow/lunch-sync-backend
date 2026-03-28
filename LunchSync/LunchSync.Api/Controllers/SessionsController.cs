using Microsoft.AspNetCore.Mvc;
using LunchSync.Core.Common.ValueObjects;
using LunchSync.Core.Modules.Sessions;
using LunchSync.Core.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace LunchSync.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/sessions")]
public class SessionsController : ControllerBase
{
    private readonly ISessionService _sessionService;

    public SessionsController(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }

    // POST: /sessions - Tạo session [Auth: Host]
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateSessionReq request)
    {
        // Giả sử HostId được lấy từ Token/Identity. Ở đây tạm lấy từ Header hoặc Guid mẫu.
        var hostId = Guid.NewGuid();
        var result = await _sessionService.CreateSessionAsync(request, hostId);
        return Ok(result);
    }


    // POST: /sessions/{pin}/join - Join session [Public]
    [AllowAnonymous]
    [HttpPost("{pin}/join")]
    public async Task<IActionResult> JoinAsync([FromRoute] string pin, [FromBody] JoinReq request)
    {
        var validPin = Pin.Create(pin);
        var result = await _sessionService.JoinSessionAsync(validPin.Value, request);
        return Ok(result);
    }

    // POST: /sessions/{pin}/start - Bắt đầu voting [Auth: Host]
    [HttpPost("{pin}/start")]
    public async Task<IActionResult> StartAsync([FromRoute] string pin)
    {
        // Giả sử HostId được lấy từ Token/Identity. Ở đây tạm lấy từ Header hoặc Guid mẫu.
        var hostId = Guid.NewGuid();
        var validPin = Pin.Create(pin);
        var result = await _sessionService.StartSessionAsync(validPin.Value, hostId);
        return Ok(result);
    }

    // POST: /sessions/{pin}/cancel  - Hủy session [Auth: Host]
    [HttpPost("{pin}/cancel")]
    public async Task<IActionResult> CancelAsync([FromRoute] string pin)
    {
        // Giả sử HostId được lấy từ Token/Identity. Ở đây tạm lấy từ Header hoặc Guid mẫu.
        var hostId = Guid.NewGuid();
        await _sessionService.CancelSessionAsync(pin, hostId);
        return Ok();
    }

    // GET: /sessions/{pin} - Lấy session status [Public]
    [AllowAnonymous]
    [HttpGet("{pin}/{sessionId:guid}/status")]
    public async Task<IActionResult> GetStatusAsync([FromRoute] string pin, [FromRoute] Guid sessionId)
    {
        var validPin = Pin.Create(pin);
        var session = await _sessionService.GetSessionAsync(validPin.Value) ?? throw new SessionNotFoundException(pin);
        if (session.Id != sessionId)
        {
            throw new SessionNotFoundByIdException(sessionId);
        }

        return Ok(session.ToStatusDto());
    }

    // GET: /sessions/{pin} - Lấy session info [Public]
    [AllowAnonymous]
    [HttpGet("{pin}/{sessionId:guid}/info")]
    public async Task<IActionResult> GetInfoAsync([FromRoute] string pin, [FromRoute] Guid sessionId)
    {
        var validPin = Pin.Create(pin);
        var session = await _sessionService.GetSessionAsync(validPin.Value) ?? throw new SessionNotFoundException(pin);
        if (session.Id != sessionId)
        {
            throw new SessionNotFoundByIdException(sessionId);
        }
        return Ok(session.ToInfoDto());
    }

    // GET: /sessions/history/{sessionId} - Lấy từ DB khi cache/local mất
    [AllowAnonymous]
    [HttpGet("history/{sessionId:guid}")]
    public async Task<IActionResult> GetHistoryAsync([FromRoute] Guid sessionId)
    {
        var session = await _sessionService.GetSessionHistoryAsync(sessionId) ?? throw new SessionNotFoundByIdException(sessionId);
        return Ok(session.ToInfoDto());
    }
}
