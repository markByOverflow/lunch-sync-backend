using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LunchSync.Core.Modules.VotingAndScoring;
// ── Inbound

/// <summary>POST /voting/{sessionId}/vote</summary>
public sealed record SubmitVoteRequest(
    /// <summary>8-char string, each char 'A' or 'B'.</summary>
    [property: JsonPropertyName("participant_id")] Guid participantId,
    [property: JsonPropertyName("choices")] string Choices
);

// ── Outbound

/// <summary>One binary choice as shown to the user.</summary>
public sealed record BinaryChoiceDto(
    [property: JsonPropertyName("index")] int Index,
    [property: JsonPropertyName("label")] string Label,
    [property: JsonPropertyName("option_a")] string OptionA,
    [property: JsonPropertyName("option_b")] string OptionB
);

/// <summary>Response after a participant submits their vote.</summary>
public sealed record VoteResultDto(
    [property: JsonPropertyName("status")] string Status,
    [property: JsonPropertyName("total_participants")] int TotalParticipants,
    [property: JsonPropertyName("total_voted")] int TotalVoted,
    /// <summary>True when this vote triggered scoring (all participants voted).</summary>
    [property: JsonPropertyName("scoring_triggered")] bool ScoringTriggered
);
