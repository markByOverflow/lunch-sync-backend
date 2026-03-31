using Microsoft.EntityFrameworkCore;

using LunchSync.Core.Common.Enums;
using LunchSync.Core.Modules.Sessions.Entities;
using LunchSync.Core.Modules.Sessions;

using System.Linq.Expressions;

namespace LunchSync.Infrastructure.Persistence.Repositories;

public class SessionRepository : ISessionRepository
{
    private readonly AppDbContext _context;

    public SessionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Session?> GetSessionByIdAsync(Guid sessionId, CancellationToken ct = default)
    {
        return await _context.Sessions
            .AsNoTracking()
            .Include(s => s.Participants)
            .Include(s => s.Collection)
            .Include(s => s.Restaurant)
            .Include(s => s.Host)
            .FirstOrDefaultAsync(s => s.Id == sessionId, ct);
    }
    public async Task<Session?> GetLastSessionByHostIdAsync(Guid hostId, CancellationToken ct = default)
    {
        return await _context.Sessions
            .Where(s => s.HostId == hostId)
            .Include(s => s.Collection)
            .Include(s => s.Restaurant)
            .OrderByDescending(s => s.CreatedAt)
            .FirstOrDefaultAsync(ct);
    }
    public async Task<Guid> SaveSessionAsync(Session session)
    {
        _context.Sessions.Add(session);
        await _context.SaveChangesAsync();
        return session.Id;
    }

    public async Task SaveParticipantAsync(Participant participant)
    {
        _context.Participants.Add(participant);
        await _context.SaveChangesAsync();
    }

    public async Task<Session?> GetActiveSessionByPinAsync(string pin, CancellationToken ct = default)
    {
        return await _context.Sessions
            .Include(s => s.Participants)
            .Include(s => s.Collection)
            .Include(s => s.Restaurant)
            .Include(s => s.Host)
            .Where(s => s.Pin == pin && s.Status != SessionStatus.Done
                && s.Status != SessionStatus.Cancelled)
            .FirstOrDefaultAsync(ct);
    }

    public async Task UpdateSessionAsync<TProperty>(Session session, Expression<Func<Session, TProperty>> propertyExpression, TProperty value)
    {
        // 1. Gắn object vào Context nếu nó chưa được theo dõi
        _context.Attach(session);

        // 2. Chỉ định chính xác property nào thay đổi
        _context.Entry(session).Property(propertyExpression).CurrentValue = value;
        _context.Entry(session).Property(propertyExpression).IsModified = true;

        // 3. Lưu (SQL chỉ Update đúng cột đó)
        await _context.SaveChangesAsync();
    }
    // CÁCH DÙNG:await UpdateFieldAsync(session, s => s.Status, SessionStatus.Active);
}
