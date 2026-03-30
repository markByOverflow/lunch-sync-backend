using Microsoft.EntityFrameworkCore;

using LunchSync.Core.Common.Enums;
using LunchSync.Core.Modules.Sessions.Entities;
using LunchSync.Core.Modules.Sessions;

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
            .Include(s => s.Participants) // Load danh sách người chơi
            .FirstOrDefaultAsync(s => s.Id == sessionId, ct);
    }
    public async Task<Session?> GetLastSessionByHostIdAsync(Guid hostId, CancellationToken ct = default)
    {
        return await _context.Sessions
            .AsNoTracking()
            .Where(s => s.HostId == hostId)
            .OrderByDescending(s => s.CreatedAt)
            .FirstOrDefaultAsync(ct);
    }
    public async Task<Guid> SaveSessionAsync(Session session)
    {
        _context.Sessions.Add(session);
        await _context.SaveChangesAsync();
        return session.Id;
    }
}
