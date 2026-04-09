using LunchSync.Core.Common.Enums;
using LunchSync.Core.Modules.Sessions.Entities;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LunchSync.Core.Modules.Sessions;

public class CreateSessionReq
{
    [JsonPropertyName("nickname")]
    [Required(ErrorMessage = "Nickname is required")]
    [StringLength(20, MinimumLength = 1, ErrorMessage = "Nickname must be between 1 and 20 characters")]
    public string Nickname { get; set; } = string.Empty;

    [JsonPropertyName("collection_id")]
    [Required(ErrorMessage = "Collection ID is required")]
    public Guid CollectionId { get; set; }

    [JsonPropertyName("price_tier")]
    [Required(ErrorMessage = "PriceTier is required")]
    public PriceTier PriceTier { get; set; }
}
public class CreateSessionRes
{
    [JsonPropertyName("session_id")]
    public Guid SessionId { get; set; }

    [JsonPropertyName("pin")]
    public string Pin { get; set; } = string.Empty;

    [JsonPropertyName("share_link")]
    public string ShareLink { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("collection_name")]
    public string CollectionName { get; set; } = string.Empty;

    [JsonPropertyName("participant_id")]
    public Guid participantId { get; set; }
}

public class JoinReq
{
    [JsonPropertyName("nickname")]
    [Required(ErrorMessage = "Nickname is required")]
    [StringLength(20, MinimumLength = 1, ErrorMessage = "Nickname must be between 1 and 20 characters")]
    public string Nickname { get; set; } = string.Empty;
}
public class JoinRes
{
    [JsonPropertyName("participant_id")]
    public Guid ParticipantId { get; set; }

    [JsonPropertyName("session_id")]
    public Guid SessionId { get; set; }

    [JsonPropertyName("nickname")]
    public string Nickname { get; set; } = string.Empty;

    [JsonPropertyName("participants")]
    public List<ParticipantRes> Participants { get; set; } = new List<ParticipantRes>();
}
public class SessionStatusRes
{
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("participants_joined")]
    public int ParticipantsJoined { get; set; }

    [JsonPropertyName("participants_voted")]
    public int ParticipantsVoted { get; set; }

    [JsonPropertyName("voting_started_at")]
    public DateTime? VotingStartedAt { get; set; }
}
public class ParticipantRes
{

    [JsonPropertyName("nickname")]
    public string Nickname { get; set; } = string.Empty;

    [JsonPropertyName("joined_at")]
    public DateTime JoinedAt { get; set; }

    [JsonPropertyName("is_host")]
    public bool? IsHost { get; set; }
}

public class SessionInfoRes
{
    [JsonPropertyName("session_id")]
    public Guid SessionId { get; set; }

    [JsonPropertyName("pin")]
    public string Pin { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("host_name")]
    public string HostName { get; set; } = string.Empty;

    [JsonPropertyName("collection_name")]
    public string CollectionName { get; set; } = string.Empty;

    [JsonPropertyName("price_tier")]
    public string PriceTier { get; set; } = string.Empty;

    [JsonPropertyName("price_display")]
    public string PriceDisplay { get; set; } = string.Empty;

    [JsonPropertyName("participants")]
    public List<ParticipantRes> Participants { get; set; } = new List<ParticipantRes>();
    //nickname, joinedat, ishost

    [JsonPropertyName("participant_count")]
    public int ParticipantCount { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("expired_at")]
    public DateTime? ExpiredAt { get; set; }
}

public class SessionStartRes
{
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("participant_count")]
    public int ParticipantCount { get; set; }

    [JsonPropertyName("voting_started_at")]
    public DateTime? VotingStartedAt { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}

public class SessionCloseVotingRes
{
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("total_voted")]
    public int TotalVoted { get; set; }

    [JsonPropertyName("total_participants")]
    public int TotalParticipants { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}

public class SessionCancelRes
{
    [JsonPropertyName("session_id")]
    public Guid SessionId { get; set; }

    [JsonPropertyName("pin")]
    public string Pin { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;
}
