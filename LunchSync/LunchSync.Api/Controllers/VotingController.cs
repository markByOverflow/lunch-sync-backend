using LunchSync.Core.Common.Auth;
using LunchSync.Core.Common.Interfaces;
using LunchSync.Core.Exceptions;
using LunchSync.Core.Modules.Auth.Interfaces;
using LunchSync.Core.Modules.VotingAndScoring;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LunchSync.Api.Controllers;

[ApiController]
[Route("api/Sessions/{pin}")]
public sealed class VotingController : ControllerBase
{
    private readonly IVotingService _votingService;
    private readonly ICurrentUserService _currentUser;
    private readonly IUserRepository _userRepository;
    public VotingController(IVotingService votingService,
        ICurrentUserService currentUser,
        IUserRepository userRepository)
    {
        _votingService = votingService;
        _currentUser = currentUser;
        _userRepository = userRepository;
    }

    /// <summary>GET the 8 binary choice questions.</summary>
    [HttpGet("choices")]
    [AllowAnonymous]   // guests can also see the questions
    [ProducesResponseType(typeof(BinaryChoiceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetChoices()
    {
        var choices = _votingService.GetChoices();
        return Ok(choices);
    }

    /// <summary>POST a participant's 8-character answer string.</summary>
    [HttpPost("vote")]
    [ProducesResponseType(typeof(VoteResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SubmitVote(
        [FromRoute] string pin,
        [FromBody] SubmitVoteRequest request,
        CancellationToken ct)
    {
        var result = await _votingService.SubmitVoteAsync(pin, request.participantId, request.Choices, ct);
        return Ok(result);
    }

    /// <summary>POST by host to close voting early (lazy evaluation).</summary>
    [HttpPost("close")]
    [Authorize(Policy = AuthPolicies.CognitoUser)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CloseVoting([FromRoute] string pin, CancellationToken ct)
    {
        var hostId = await GetCurrentHostIdAsync(ct);
        if (hostId is null)
        {
            throw new NotHostException();
        }
        await _votingService.CloseVotingAsync(pin, hostId.Value, ct);
        return NoContent();
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
