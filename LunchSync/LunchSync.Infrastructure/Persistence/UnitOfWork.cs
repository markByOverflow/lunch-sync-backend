using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LunchSync.Core.Common.Interfaces;
using LunchSync.Core.Modules.RestaurantsAndDishes;
using LunchSync.Core.Modules.RestaurantsAndDishes.Repositories;
using LunchSync.Core.Modules.Sessions;
using LunchSync.Infrastructure.Persistence.Repositories;

using Microsoft.EntityFrameworkCore.Storage;

namespace LunchSync.Infrastructure.Persistence;
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _db;
    private IDbContextTransaction? _tx;

    // Lazy-init repositories — chỉ tạo khi cần
    private ISessionRepository? _sessions;
    private IDishRepository? _dishes;
    private IRestaurantRepository? _restaurants;
    private ICollectionRepository? _collections;

    public UnitOfWork(AppDbContext db) => _db = db;

    public ISessionRepository Sessions => _sessions ??= new SessionRepository(_db);
    public IDishRepository Dishes => _dishes ??= new DishRepository(_db);
    public IRestaurantRepository Restaurants => _restaurants ??= new RestaurantRepository(_db);
    public ICollectionRepository Collections => _collections ??= new CollectionRepository(_db);

    public Task<int> SaveChangesAsync(CancellationToken ct = default)
        => _db.SaveChangesAsync(ct);

    // Transaction
    public async Task BeginTransactionAsync(CancellationToken ct = default)
    {
        if (_tx is not null)
            throw new InvalidOperationException("Transaction đã được mở.");
        _tx = await _db.Database.BeginTransactionAsync(ct);
    }

    public async Task CommitAsync(CancellationToken ct = default)
    {
        await SaveChangesAsync(ct);
        if (_tx is not null)
        {
            await _tx.CommitAsync(ct);
            await _tx.DisposeAsync();
            _tx = null;
        }
    }

    public async Task RollbackAsync(CancellationToken ct = default)
    {
        if (_tx is not null)
        {
            await _tx.RollbackAsync(ct);
            await _tx.DisposeAsync();
            _tx = null;
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_tx is not null)
            await _tx.DisposeAsync();
        await _db.DisposeAsync();
    }
}

