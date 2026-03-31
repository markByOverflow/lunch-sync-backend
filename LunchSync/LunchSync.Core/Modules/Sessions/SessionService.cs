
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
    private const int DefaultExpiryMinutes = 30;

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

            // Check cả cache lẫn DB — pin có thể đang dùng nhưng chưa cache
            var existsInCache = await _cache.GetActiveSessionByPinAsync(candidate) != null;
            var existsInDb = !existsInCache && await _repository.GetActiveSessionByPinAsync(candidate) != null;

            if (!existsInCache && !existsInDb)
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

        var host = new Participant
        {
            Id = Guid.NewGuid(),
            SessionId = session.Id,
            Nickname = request.Nickname,
            JoinedAt = DateTime.UtcNow,
            UserId = HostId
        };

        session.Participants.Add(host);
        session.Collection = collection;

        await _repository.SaveSessionAsync(session);
        await _cache.SetSessionAsync(session);

        var baseUrl = _configuration["AppSettings:ClientBaseUrl"];
        return session.ToCreateSessionRes(collection.Name ?? "", baseUrl ?? "");

    }
    public async Task<JoinRes> JoinSessionAsync(Guid? userId, string pin, JoinReq request, CancellationToken ct = default)
    {
        var session = await _cache.GetActiveSessionByPinAsync(pin, ct) ?? await _repository.GetActiveSessionByPinAsync(pin, ct)
                      ?? throw new SessionNotFoundException(pin);

        if (session.Status != SessionStatus.Waiting)
            throw new SessionAlreadyStartedException();
        if (session.Participants.Count >= MaxParticipants)
            throw new SessionFullException();
        if (session.Participants.Any(p => p.Nickname == request.Nickname))
            throw new NicknameTakenException(request.Nickname);

        var participant = new Participant
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            SessionId = session.Id,
            Nickname = request.Nickname,
            JoinedAt = DateTime.UtcNow
        };

        session.Participants.Add(participant);
        await _repository.SaveParticipantAsync(participant);
        await _cache.SetSessionAsync(session, ct);

        return participant.ToJoinRes(session);
    }
    public async Task<SessionStartRes> StartSessionAsync(string pin, Guid hostId, CancellationToken ct = default)
    {
        var session = await _cache.GetActiveSessionByPinAsync(pin, ct) ?? await _repository.GetActiveSessionByPinAsync(pin, ct)
                      ?? throw new SessionNotFoundException(pin);

        if (session.Status != SessionStatus.Waiting)
            throw new SessionAlreadyStartedException();
        if (session.HostId != hostId)
            throw new NotHostException();
        if (session.Participants.Count < MinParticipants)
            throw new InsufficientParticipantsException(MinParticipants, session.Participants.Count);

        // Cập nhật trạng thái sang Voting
        session.Status = SessionStatus.Voting;
        await _repository.UpdateSessionAsync(session, s => s.Status, session.Status);
        await _cache.SetSessionAsync(session, ct);

        return session.ToStartRes();
    }
    public async Task CancelSessionAsync(string pin, Guid hostId, CancellationToken ct = default)
    {
        var session = await _cache.GetActiveSessionByPinAsync(pin) ?? throw new SessionNotFoundException(pin);
        if (session != null && session.HostId == hostId)
        {
            await _cache.RemoveSessionAsync(pin);
        }
        await _repository.UpdateSessionAsync(session, s => s.Status, SessionStatus.Cancelled);
        await _cache.RemoveSessionAsync(pin, ct);

    }

    //GET status+info => Object Session
    public async Task<Session?> GetSessionAsync(string pin, CancellationToken ct = default)
    {
        var session = await _cache.GetActiveSessionByPinAsync(pin, ct)
            ?? await _repository.GetActiveSessionByPinAsync(pin, ct) ?? throw new SessionNotFoundException(pin);
        if (session.Collection == null)
        {
            session.Collection = await _collectionRepository.GetCollectionByIdAsync(session.CollectionId) ?? throw new CollectionNotFoundException(session.CollectionId);
        }
        return session;
    }
    public async Task<Session?> GetSessionHistoryAsync(Guid sessionId, CancellationToken ct = default)
    => await _repository.GetSessionByIdAsync(sessionId, ct);


}
