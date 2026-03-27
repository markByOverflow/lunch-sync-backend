using LunchSync.Core.Common.Enums;
using LunchSync.Core.Modules.Sessions.Entities;

namespace LunchSync.Core.Modules.Sessions;

public class CreateSessionReq
{
    public string Nickname { get; set; } = string.Empty;
    public Guid CollectionId { get; set; }
    public PriceTier PriceTier { get; set; }
}
public class CreateSessionRes
{
    public Guid SessionId { get; set; }
    public string Pin { get; set; } = string.Empty;
    public string ShareLink { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string CollectionName { get; set; } = string.Empty;
    public Guid participantId { get; set; }
}

public class JoinReq
{
    public string Nickname { get; set; } = string.Empty;
}
public class JoinRes
{
    public Guid ParticipantId { get; set; }
    public Guid SessionId { get; set; }
    public string Nickname { get; set; } = string.Empty;
    public List<ParticipantDto> Participants { get; set; } = new List<ParticipantDto>();
}
public class SessionStatusDto
{
    public string Status { get; set; } = string.Empty;
    public int ParticipantsJoined { get; set; }
    public int ParticipantsVoted { get; set; }
    public DateTime? VotingStartedAt { get; set; }
}
public class ParticipantDto
{
    public string Nickname { get; set; } = string.Empty;
    public DateTime JoinedAt { get; set; }
    public bool? IsHost { get; set; }
}

public class SessionInfoDto
{
    public Guid SessionId { get; set; }
    public string Pin { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string HostName { get; set; } = string.Empty;
    public string CollectionName { get; set; } = string.Empty;
    public string PriceTier { get; set; } = string.Empty;
    public string PriceDisplay { get; set; } = string.Empty;
    public List<ParticipantDto> Participants { get; set; } = new List<ParticipantDto>();
    //nickname, joinedat, ishost
    public int ParticipantCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ExpiredAt { get; set; }
}

public class SessionStartRes
{
    public string Status { get; set; } = string.Empty;
    public int ParticipantCount { get; set; }
    public DateTime? VotingStartedAt { get; set; }
    public string Message { get; set; } = string.Empty;
}

public class SessionCloseVotingRes
{
    public string Status { get; set; } = string.Empty;
    public int TotalVoted { get; set; }
    public int TotalParticipants { get; set; }
    public string Message { get; set; } = string.Empty;
}
