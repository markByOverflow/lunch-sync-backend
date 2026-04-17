using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunchSync.Core.Modules.VotingAndScoring;
public interface IVotingService
{
    /// <summary>Return the 8 binary choice questions for the UI.</summary>
    IReadOnlyList<BinaryChoiceDto> GetChoices();

    /// <summary>
    /// Submit a participant's 8 binary answers.
    /// If all participants have voted, triggers group aggregation + scoring.
    /// </summary>
    Task<VoteResultDto> SubmitVoteAsync(
        string pin,
        Guid participantId,
        string choices,
        CancellationToken ct = default);

    /// <summary>
    /// Host manually closes voting (lazy evaluation fallback).
    /// Runs scoring on participants who have voted so far.
    /// </summary>
    Task CloseVotingAsync(string pin, Guid hostUserId, CancellationToken ct = default);
}
