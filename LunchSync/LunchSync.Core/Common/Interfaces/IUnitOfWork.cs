using LunchSync.Core.Modules.Auth.Interfaces;
using LunchSync.Core.Modules.RestaurantsAndDishes;
using LunchSync.Core.Modules.RestaurantsAndDishes.Repositories;
using LunchSync.Core.Modules.Sessions;

namespace LunchSync.Core.Common.Interfaces;
public interface IUnitOfWork : IAsyncDisposable
{
    ISessionRepository Sessions { get; }
    IDishRepository Dishes { get; }
    IRestaurantRepository Restaurants { get; }
    ICollectionRepository Collections { get; }

    /// <summary>Persist mọi thay đổi đang tracked trong transaction hiện tại.</summary>
    Task<int> SaveChangesAsync(CancellationToken ct = default);

    /// <summary>
    /// Bắt đầu explicit DB transaction.
    /// Dùng khi cần rollback thủ công (e.g. saga-style).
    /// </summary>
    Task BeginTransactionAsync(CancellationToken ct = default);

    Task CommitAsync(CancellationToken ct = default);
    Task RollbackAsync(CancellationToken ct = default);
}
