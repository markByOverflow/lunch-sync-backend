
using LunchSync.Core.Common.Enums;
using LunchSync.Core.Common.ValueObjects;
using LunchSync.Core.Exceptions;
using LunchSync.Core.Modules.RestaurantsAndDishes;
using LunchSync.Core.Modules.Sessions.Entities;

using Microsoft.Extensions.Configuration;

namespace LunchSync.Core.Modules.Sessions;

public class SessionService : ISessionService
{
    private readonly ISessionCache _cache;
    private readonly ISessionRepository _repository;
    private readonly IConfiguration _configuration;
    private readonly ICollectionRepository _collectionRepository;
    private const int MaxParticipants = 8;
    private const int MinParticipants = 3;
    private const int DefaultExpiryMinutes = 15;
    private const int VotingExpiryMinutes = 10;

    public SessionService(ISessionCache cache, ISessionRepository repository, IConfiguration configuration, ICollectionRepository collectionRepository)
    {
        _cache = cache;
        _repository = repository;
        _configuration = configuration;
        _collectionRepository = collectionRepository;
    }
    //command Post => return DTO
    public async Task<CreateSessionRes> CreateSessionAsync(CreateSessionReq request, Guid HostId)
    {
        var collection = await _collectionRepository.GetCollectionByIdAsync(request.CollectionId) ?? throw new CollectionNotFoundException(request.CollectionId);
        var now = DateTime.UtcNow;
        var sessionId = Guid.NewGuid();

        var pin = "";
        for (int i = 0; i < 5; i++)
        {
            var candidate = Pin.Generate().Value;

            var exists = await _cache.GetActiveSessionByPinAsync(candidate);
            if (exists == null)
            {
                pin = candidate;
                break;
            }
        }

        if (string.IsNullOrEmpty(pin))
            throw new BusinessRuleViolationException("Không thể tạo mã PIN. Vui lòng thử lại.");

        var session = new Session
        {
            Id = sessionId,
            Pin = pin,
            HostId = HostId,
            Status = SessionStatus.Waiting,
            CreatedAt = now,
            ExpiresAt = now.AddMinutes(DefaultExpiryMinutes),
            CollectionId = request.CollectionId,
            PriceTier = request.PriceTier
        };
        await _repository.SaveSessionAsync(session);
        await _cache.SaveActiveSessionAsync(session, DefaultExpiryMinutes);

        var host = new Participant
        {
            Id = Guid.NewGuid(),
            SessionId = session.Id,
            Nickname = request.Nickname,
            JoinedAt = DateTime.UtcNow,
            UserId = HostId
        };

        await _cache.TryJoinAtomicAsync(pin, host, MaxParticipants, DefaultExpiryMinutes);
        await _repository.SaveParticipantAsync(host);

        var sessionUpdate = await _cache.GetActiveSessionByPinAsync(pin) ?? throw new SessionNotFoundException(pin);

        var baseUrl = _configuration["AppSettings:ClientBaseUrl"];
        return sessionUpdate.ToCreateSessionRes(collection.Name ?? "", baseUrl ?? "");

    }
    public async Task<JoinRes> JoinSessionAsync(Guid? userId, string pin, JoinReq request, CancellationToken ct = default)
    {
        var session = await _cache.GetActiveSessionByPinAsync(pin, ct) ?? throw new SessionNotFoundException(pin);
        var participant = new Participant
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            SessionId = session.Id,
            Nickname = request.Nickname,
            JoinedAt = DateTime.UtcNow
        };

        var result = await _cache.TryJoinAtomicAsync(pin, participant, MaxParticipants, DefaultExpiryMinutes);
        switch (result)
        {
            case 1:
                throw new NicknameTakenException(request.Nickname);
            case 2:
                throw new SessionFullException();
            case 3:
                throw new SessionAlreadyStartedException();
            case 0:
                break;
            default:
                throw new BusinessRuleViolationException("Có lỗi xảy ra khi tham gia phòng.");
        }

        var sessionUpdate = await _cache.GetActiveSessionByPinAsync(pin, ct)
                      ?? throw new SessionNotFoundException(pin);

        await _repository.SaveParticipantAsync(participant);
        sessionUpdate.Participants = await _cache.GetParticipantsAsync(pin, ct);

        return participant.ToJoinRes(sessionUpdate);
    }
    public async Task<SessionStartRes> StartSessionAsync(string pin, Guid hostId, CancellationToken ct = default)
    {
        var session = await _cache.GetActiveSessionByPinAsync(pin, ct) ?? throw new SessionNotFoundException(pin);

        if (session.Status != SessionStatus.Waiting)
            throw new SessionAlreadyStartedException();
        if (session.HostId != hostId)
            throw new NotHostException();
        var participants = await _cache.GetParticipantsAsync(pin, ct);
        if (participants.Count < MinParticipants)
            throw new InsufficientParticipantsException(MinParticipants, participants.Count);

        // Cập nhật trạng thái sang Voting
        await _cache.UpdateStatusAndExpireAsync(pin, SessionStatus.Voting, VotingExpiryMinutes);
        session.Status = SessionStatus.Voting;
        await _repository.UpdateSessionAsync(session, s => s.Status, session.Status);

        var sessionUpdate = await _cache.GetActiveSessionByPinAsync(pin, ct) ?? throw new SessionNotFoundException(pin);
        return sessionUpdate.ToStartRes();
    }
    public async Task<SessionCancelRes> CancelSessionAsync(string pin, Guid hostId, CancellationToken ct = default)
    {
        var session = await _cache.GetActiveSessionByPinAsync(pin, ct) ?? throw new SessionNotFoundException(pin);
        if (session.HostId != hostId)
        {
            throw new NotHostException();
        }
        await _repository.UpdateSessionAsync(session, s => s.Status, SessionStatus.Cancelled);
        await _cache.RemoveSessionAsync(pin);

        var sessionUpdate = await _repository.GetSessionByIdAsync(session.Id, ct) ?? throw new SessionNotFoundByIdException(session.Id);
        return sessionUpdate.ToCancelRes();
    }

    //GET status+info => Object Session
    public async Task<Session?> GetSessionAsync(string pin, CancellationToken ct = default)
    {
        var session = await _cache.GetActiveSessionByPinAsync(pin, ct) ?? throw new SessionExpiredException();
        session.Participants = await _cache.GetParticipantsAsync(pin, ct);
        session.Collection = await _collectionRepository.GetCollectionByIdAsync(session.CollectionId, ct) ?? throw new CollectionNotFoundException(session.CollectionId);
        return session;
    }

    //GET Session trong db
    public async Task<Session?> GetSessionHistoryAsync(Guid sessionId, CancellationToken ct = default)
    => await _repository.GetSessionByIdAsync(sessionId, ct);
}
