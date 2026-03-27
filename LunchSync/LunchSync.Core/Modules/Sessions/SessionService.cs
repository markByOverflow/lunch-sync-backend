
using LunchSync.Core.Common.Enums;
using LunchSync.Core.Exceptions;
using LunchSync.Core.Modules.Sessions.Entities;

using Microsoft.Extensions.Configuration;

namespace LunchSync.Core.Modules.Sessions;

public class SessionService : ISessionService
{
    private readonly ISessionCache _cache;
    private readonly ISessionRepository _repository;
    private readonly IConfiguration _configuration;
    private readonly IPinManager _pinManager;
    private const int MaxParticipants = 8;
    private const int MinParticipants = 3;
    private const int DefaultExpiryMinutes = 30;
    private const int VotingExpiryMinutes = 90;

    public SessionService(ISessionCache cache, ISessionRepository repository, IConfiguration configuration, IPinManager pinManager)
    {
        _cache = cache;
        _repository = repository;
        _configuration = configuration;
        _pinManager = pinManager;
    }
    //command Post => return DTO
    public async Task<CreateSessionRes> CreateSessionAsync(CreateSessionReq request, Guid HostId)
    {
        //var collection = await _collectionRepository.GetByIdAsync(request.CollectionId);
        // if (collection == null) throw new BusinessRuleViolationException("Bộ sưu tập không tồn tại.");
        var collectionName = "";

        var now = DateTime.UtcNow;
        var sessionId = Guid.NewGuid();
        var pin = await _pinManager.GetUnusedPinAsync(sessionId);
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
        await _cache.SaveActiveSessionAsync(session, DefaultExpiryMinutes);
        var hostParticipant = new Participant
        {
            Id = Guid.NewGuid(),
            Nickname = request.Nickname, //hostname
            JoinedAt = now
        };
        await _cache.TryJoinAtomicAsync(pin, hostParticipant, MaxParticipants, DefaultExpiryMinutes);

        var baseUrl = _configuration["AppSettings:ClientBaseUrl"];
        return session.ToCreateSessionRes(collectionName ?? "", baseUrl ?? "");

    }
    public async Task<JoinRes> JoinSessionAsync(string pin, JoinReq request)
    {
        var player = new Participant
        {
            Id = Guid.NewGuid(),
            Nickname = request.Nickname,
            JoinedAt = DateTime.UtcNow
        };
        var result = await _cache.TryJoinAtomicAsync(pin, player, MaxParticipants, DefaultExpiryMinutes);
        switch (result)
        {
            case 1:
                throw new BusinessRuleViolationException("Tên hiển thị này đã có người sử dụng.");
            case 2:
                throw new BusinessRuleViolationException("Phòng đã đầy.");
            case 3:
                throw new BusinessRuleViolationException("Phòng không tồn tại.");
            case 4:
                throw new BusinessRuleViolationException("Phòng đã bắt đầu hoặc kết thúc.");
            case 0:
                break;
            default:
                throw new BusinessRuleViolationException("Có lỗi xảy ra khi tham gia phòng.");
        }

        var session = await _cache.GetActiveSessionByPinAsync(pin)
                      ?? throw new BusinessRuleViolationException("Không thể lấy thông tin phòng.");

        session.Participants = await _cache.GetParticipantsAsync(pin);

        return player.ToJoinRes(session);
    }
    public async Task<SessionStartRes> StartSessionAsync(string pin, Guid hostId)
    {
        var session = await _cache.GetActiveSessionByPinAsync(pin)
                      ?? throw new BusinessRuleViolationException("Phòng không tồn tại.");

        if (session.Status != SessionStatus.Waiting)
            throw new BusinessRuleViolationException("Phòng đã bắt đầu.");
        if (session.HostId != hostId)
            throw new BusinessRuleViolationException("Chỉ chủ phòng mới có quyền bắt đầu.");

        var participants = await _cache.GetParticipantsAsync(pin);
        if (participants.Count < MinParticipants)
            throw new BusinessRuleViolationException($"Cần ít nhất {MinParticipants} người để bắt đầu.");

        // Cập nhật trạng thái sang Voting và gia hạn thời gian
        await _cache.UpdateStatusAndExpireAsync(pin, SessionStatus.Voting, VotingExpiryMinutes);

        return session.ToStartRes();
    }
    public async Task CancelSessionAsync(string pin, Guid hostId)
    {
        var session = await _cache.GetActiveSessionByPinAsync(pin);
        if (session != null && session.HostId == hostId)
        {
            await _cache.RemoveSessionAsync(pin);
            await _pinManager.ReleasePinAsync(pin);
        }
    }

    //GET status+info => Object Session
    public async Task<Session?> GetSessionAsync(string pin) => await _cache.GetActiveSessionByPinAsync(pin);

    //GET Session trong db
    public async Task<Session?> GetSessionHistoryAsync(Guid sessionId)
    {
        // Truy cập DB để lấy dữ liệu lịch sử
        var history = await _repository.GetHistoryByIdAsync(sessionId) ?? throw new BusinessRuleViolationException("Không tìm thấy thông tin lịch sử của phiên này.");
        return history;
    }

}
