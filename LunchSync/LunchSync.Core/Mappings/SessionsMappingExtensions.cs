using LunchSync.Contracts.Common.Enums;
using LunchSync.Contracts.Modules.Sessions.Dtos;
using LunchSync.Core.Modules.Sessions.Entities;

namespace LunchSync.Core.Mappings;

public static class SessionsMappingExtensions
{
    public static ParticipantDto ToDto(this Participant entity)
    {
        // Null safety: bao ve khi entity null
        ArgumentNullException.ThrowIfNull(entity);

        // Map tung truong tu entity sang DTO
        return new ParticipantDto
        {
            Id = entity.Id,
            SessionId = entity.SessionId,
            UserId = entity.UserId,
            Nickname = entity.Nickname,
            // Null safety: neu list null thi giu null, tranh loi khi Select
            PrefVector = entity.PrefVector?.ToList(),
            JoinedAt = entity.JoinedAt,
            VotedAt = entity.VotedAt,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }

    public static Participant ToEntity(this CreateParticipantRequest request)
    {
        // Null safety: bao ve khi request null
        ArgumentNullException.ThrowIfNull(request);

        // Map tung truong tu request sang entity (khong map truong he thong)
        return new Participant
        {
            SessionId = request.SessionId,
            UserId = request.UserId,
            Nickname = request.Nickname,
            // Null safety: neu list null thi giu null
            PrefVector = request.PrefVector?.ToList()
        };
    }

    public static void ApplyTo(this UpdateParticipantRequest request, Participant entity)
    {
        // Null safety: bao ve khi request hoac entity null
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(entity);

        // Map tung truong tu request sang entity hien tai (khong map truong he thong)
        entity.SessionId = request.SessionId;
        entity.UserId = request.UserId;
        entity.Nickname = request.Nickname;
        // Null safety: neu list null thi giu null
        entity.PrefVector = request.PrefVector?.ToList();
    }

    public static SessionDto ToDto(this Session entity)
    {
        // Null safety: bao ve khi entity null
        ArgumentNullException.ThrowIfNull(entity);

        // Map tung truong tu entity sang DTO
        return new SessionDto
        {
            Id = entity.Id,
            Pin = entity.Pin,
            HostId = entity.HostId,
            CollectionId = entity.CollectionId,
            // Chuyen doi enum bang Enum.Parse de tranh ep kieu truc tiep
            PriceTier = Enum.Parse<PriceTier>(entity.PriceTier.ToString()),
            // Chuyen doi enum bang Enum.Parse de tranh ep kieu truc tiep
            Status = Enum.Parse<SessionStatus>(entity.Status.ToString()),
            // Null safety: neu list null thi giu null
            GroupVector = entity.GroupVector?.ToList(),
            // Null safety: neu list null thi giu null
            TopDishIds = entity.TopDishIds?.ToList(),
            // Null safety: neu list null thi giu null
            TopRestaurantIds = entity.TopRestaurantIds?.ToList(),
            // Null safety: neu list null thi giu null
            BoomEliminatedIds = entity.BoomEliminatedIds?.ToList(),
            FinalRestaurantId = entity.FinalRestaurantId,
            VotingStartedAt = entity.VotingStartedAt,
            ExpiresAt = entity.ExpiresAt,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }

    public static Session ToEntity(this CreateSessionRequest request)
    {
        // Null safety: bao ve khi request null
        ArgumentNullException.ThrowIfNull(request);

        // Map tung truong tu request sang entity (khong map truong he thong)
        return new Session
        {
            Pin = request.Pin,
            HostId = request.HostId,
            CollectionId = request.CollectionId,
            // Chuyen doi enum bang Enum.Parse de tranh ep kieu truc tiep
            PriceTier = Enum.Parse<Core.Common.Enums.PriceTier>(request.PriceTier.ToString())
        };
    }

    public static void ApplyTo(this UpdateSessionRequest request, Session entity)
    {
        // Null safety: bao ve khi request hoac entity null
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(entity);

        // Map tung truong tu request sang entity hien tai (khong map truong he thong)
        entity.Pin = request.Pin;
        entity.HostId = request.HostId;
        entity.CollectionId = request.CollectionId;
        // Chuyen doi enum bang Enum.Parse de tranh ep kieu truc tiep
        entity.PriceTier = Enum.Parse<Core.Common.Enums.PriceTier>(request.PriceTier.ToString());
    }
}
