using LunchSync.Core.Common.Auth;
using LunchSync.Core.Common.Interfaces;
using LunchSync.Core.Exceptions;
using LunchSync.Core.Modules.Auth.Interfaces;
using LunchSync.Core.Modules.Sessions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LunchSync.Api.Controllers;

[Route("api/Sessions")]
[ApiController]
public class ResultsController : ControllerBase
{

    private readonly IResultsService _resultsService;
    private readonly ICurrentUserService _currentUser;
    private readonly IUserRepository _userRepository;

    public ResultsController(IResultsService resultsService,
        ICurrentUserService currentUser,
        IUserRepository userRepository)
    {
        _resultsService = resultsService;
        _currentUser = currentUser;
        _userRepository = userRepository;
    }

    // GET /api/sessions/{pin}/results — public, polling by everyone
    [AllowAnonymous]
    [HttpGet("{pin}/results")]
    [ProducesResponseType(typeof(GetResultsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetResults([FromRoute]string pin, [FromRoute] Guid sessionId, CancellationToken ct)
    {
        var result = await _resultsService.GetResultsAsync(pin, sessionId, ct);
        return Ok(result);
    }

    // POST /api/sessions/{pin}/boom — host only
    [HttpPost("{pin}/boom")]
    [Authorize(Policy = AuthPolicies.CognitoUser)]
    [ProducesResponseType(typeof(BoomResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Boom([FromRoute] string pin, CancellationToken ct)
    {
        var hostId = await GetCurrentHostIdAsync(ct) ?? throw new NotHostException();
        var result = await _resultsService.BoomAsync(pin, hostId, ct);
        return Ok(result);
    }

    // POST /api/sessions/{pin}/pick — host only
    [HttpPost("{pin}/pick")]
    [Authorize(Policy = AuthPolicies.CognitoUser)]
    [ProducesResponseType(typeof(PickResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Pick([FromRoute] string pin, [FromBody] Guid restaurantId, CancellationToken ct)
    {
        var hostId = await GetCurrentHostIdAsync(ct) ?? throw new NotHostException();
        var result = await _resultsService.PickAsync(pin, hostId, restaurantId, ct);
        return Ok(result);
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
