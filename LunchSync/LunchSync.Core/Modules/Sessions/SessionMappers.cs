using LunchSync.Core.Modules.Sessions.Entities;
using LunchSync.Core.Modules.Sessions;

namespace LunchSync.Core.Modules.Sessions;

public static class SessionsMappers
{
    public static CreateSessionRes ToCreateSessionRes(this Session session, string collectionName, string baseUrl)
    {
        return new CreateSessionRes
        {
            SessionId = session.Id,
            Pin = session.Pin,
            ShareLink = $"{baseUrl}/{session.Pin}",
            Status = session.Status.ToString().ToLower(),
            CollectionName = collectionName,
            participantId = session.HostId,
        };
    }
    public static JoinRes ToJoinRes(this Participant participant, Session session)
    {
        return new JoinRes
        {
            ParticipantId = participant.Id,
            SessionId = session.Id,
            Nickname = participant.Nickname,
            Participants = session.Participants.Select(p => new ParticipantRes
            {
                Nickname = p.Nickname,
                JoinedAt = p.JoinedAt,
                IsHost = p.UserId == session.HostId,
            }).ToList()
        };
    }
    public static SessionStatusRes ToStatusRes(this Session session)
    {
        return new SessionStatusRes
        {
            Status = session.Status.ToString().ToLower(),
            ParticipantsJoined = session.Participants.Count,
            ParticipantsVoted = session.GroupVector?.Count ?? 0,
            VotingStartedAt = session.VotingStartedAt
        };
    }
    public static SessionInfoRes ToInfoRes(this Session session)
    {
        return new SessionInfoRes
        {
            SessionId = session.Id,
            Pin = session.Pin,
            Status = session.Status.ToString().ToLower(),
            HostName = session.Participants
                        .Where(p => p.UserId != Guid.Empty && p.UserId == session.HostId)
                        .Select(p => p.Nickname)
                        .FirstOrDefault() ?? "Unknown Host",
            CollectionName = session.Collection?.Name ?? "Unknown Collection",
            PriceTier = session.PriceTier.ToString(),
            PriceDisplay = session.PriceTier.ToString() + "/phần",
            Participants = session.Participants.Select(p => new ParticipantRes
            {
                Nickname = p.Nickname,
                JoinedAt = p.JoinedAt,
                IsHost = p.UserId == session.HostId,
            }).ToList(),
            ParticipantCount = session.Participants.Count,
            CreatedAt = session.CreatedAt,
            ExpiredAt = session.ExpiresAt
        };
    }
    public static SessionStartRes ToStartRes(this Session session)
    {
        return new SessionStartRes
        {
            Status = session.Status.ToString().ToLower(),
            ParticipantCount = session.Participants.Count,
            VotingStartedAt = session.VotingStartedAt,
            Message = $"Bắt đầu bỏ phiếu vs {session.Participants.Count} người"
        };
    }
    public static SessionCloseVotingRes ToCloseVotingRes(this Session session)
    {
        return new SessionCloseVotingRes
        {
            Status = session.Status.ToString().ToLower(),
            TotalParticipants = session.Participants.Count,
            TotalVoted = session.GroupVector?.Count ?? 0,
            Message = $"Scoring với {session.GroupVector?.Count ?? 0} người đã vote"
        };
    }

    public static SessionCancelRes ToCancelRes(this Session session)
    {
        return new SessionCancelRes
        {
            SessionId = session.Id,
            Pin = session.Pin,
            Status = session.Status.ToString().ToLower(),
        };
    }
}
