using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LunchSync.Core.Common.Enums;
using LunchSync.Core.Exceptions;
using LunchSync.Core.Modules.RestaurantsAndDishes;
using LunchSync.Core.Modules.RestaurantsAndDishes.Entities;
using LunchSync.Core.Modules.RestaurantsAndDishes.Repositories;
using LunchSync.Core.Modules.Sessions.Entities;

namespace LunchSync.Core.Modules.Sessions;

public sealed class ResultsService : IResultsService
{
    private readonly ISessionRepository _sessionRepo;
    private readonly IRestaurantRepository _restaurantRepo;
    private readonly IDishRepository _dishRepo;

    public ResultsService(
        ISessionRepository sessionRepo,
        IRestaurantRepository restaurantRepo,
        IDishRepository dishRepo)
    {
        _sessionRepo = sessionRepo;
        _restaurantRepo = restaurantRepo;
        _dishRepo = dishRepo;
    }

    // ── GET /results ─────────────────────────────────────────────────────────

    public async Task<GetResultsDto> GetResultsAsync(string pin, CancellationToken ct = default)
    {
        var session = await _sessionRepo.GetActiveSessionByPinAsync(pin, ct)
            ?? throw new SessionNotFoundException(pin);

        var validStatuses = new[]
        {
            SessionStatus.Results,
            SessionStatus.Picking,
            SessionStatus.Done
        };

        if (!validStatuses.Contains(session.Status))
            throw new ResultsNotReadyException("Kết quả chưa sẵn sàng");

        // Load dishes theo top_dish_ids (đã ordered từ scoring)
        var topDishIds = session.TopDishIds ?? [];
        var dishEntities = await LoadOrderedDishesAsync(topDishIds, ct);

        // Load restaurants theo top_restaurant_ids (đã ordered từ scoring)
        var topRestaurantIds = session.TopRestaurantIds ?? [];
        var restaurantEntities = await _restaurantRepo
            .GetRestaurantsByIdsAsync(topRestaurantIds, ct);

        // Reorder về đúng thứ tự rank (DB trả về không đảm bảo order)
        var orderedRestaurants = ReorderById(restaurantEntities, topRestaurantIds);

        // Scores được lưu trong session.GroupVector — rebuild từ stored results
        // Vì scoring đã done, dùng rank-based score proxy để render thanh/% trên UI
        var dishScores = BuildRankBasedScores(topDishIds);
        var restaurantScores = BuildRankBasedScores(topRestaurantIds);

        var topDishIdSet = new HashSet<Guid>(topDishIds.Take(7)); // top 7 for matched dishes

        return ResultMappers.ToGetResultsDto(
            session,
            dishEntities,
            dishScores,
            orderedRestaurants,
            restaurantScores,
            topDishIdSet);
    }

    // ── POST /boom ────────────────────────────────────────────────────────────

    public async Task<BoomResultDto> BoomAsync(string pin, Guid hostId, CancellationToken ct = default)
    {
        var session = await _sessionRepo.GetActiveSessionByPinAsync(pin, ct)
            ?? throw new SessionNotFoundException(pin);

        ValidateIsHost(session, hostId);

        if (session.Status != SessionStatus.Results)
            throw new BoomNotReadyException("Chỉ Boom khi session đang ở trạng thái Results");

        var top5 = session.TopRestaurantIds?
            .Take(5)
            .ToList()
            ?? throw new BoomNotReadyException("Chưa có kết quả để Boom");

        if (top5.Count < 2)
            throw new BoomNotReadyException("Không đủ quán để Boom");

        // Random eliminate 2 — Fisher-Yates partial
        var eliminatedIds = PickTwoRandom(top5);
        var remainingIds = top5.Except(eliminatedIds).ToList();

        // Persist — 2 separate updates để dùng lại UpdateSessionAsync generic
        await _sessionRepo.UpdateSessionAsync(
            session, s => s.BoomEliminatedIds, eliminatedIds);
        await _sessionRepo.UpdateSessionAsync(
            session, s => s.Status, SessionStatus.Picking);

        // Load restaurant data cho response
        var restaurants = await _restaurantRepo.GetRestaurantsByIdsAsync(top5, ct);
        var restaurantMap = restaurants.ToDictionary(r => r.Id);
        var rankMap = BuildRankMap(top5);
        var restaurantScores = BuildRankBasedScores(top5);
        var topDishIdSet = new HashSet<Guid>(
            session.TopDishIds?.Take(7) ?? Enumerable.Empty<Guid>());

        return ResultMappers.ToBoomResultDto(
            eliminatedIds,
            remainingIds,
            restaurantMap,
            restaurantScores,
            rankMap,
            topDishIdSet);
    }

    // ── POST /pick ────────────────────────────────────────────────────────────

    public async Task<PickResultDto> PickAsync(
        string pin, Guid hostId, Guid restaurantId, CancellationToken ct = default)
    {
        var session = await _sessionRepo.GetActiveSessionByPinAsync(pin, ct)
            ?? throw new SessionNotFoundException(pin);

        ValidateIsHost(session, hostId);

        if (session.Status != SessionStatus.Picking)
            throw new PickNotReadyException("Chỉ Pick khi session đang ở trạng thái Picking");

        // Validate restaurantId thuộc remaining (top5 - eliminated)
        var top5 = session.TopRestaurantIds?.Take(5).ToHashSet()
            ?? new HashSet<Guid>();
        var eliminated = session.BoomEliminatedIds?.ToHashSet()
            ?? new HashSet<Guid>();
        var remaining = top5.Except(eliminated).ToHashSet();

        if (!remaining.Contains(restaurantId))
            throw new InvalidPickException(restaurantId);

        await _sessionRepo.UpdateSessionAsync(
            session, s => s.FinalRestaurantId, (Guid?)restaurantId);
        await _sessionRepo.UpdateSessionAsync(
            session, s => s.Status, SessionStatus.Done);

        var restaurants = await _restaurantRepo
            .GetRestaurantsByIdsAsync(new[] { restaurantId }, ct);
        var final = restaurants.FirstOrDefault()
            ?? throw new RestaurantNotFoundException(restaurantId);

        var rankMap = BuildRankMap(session.TopRestaurantIds?.Take(5).ToList() ?? new List<Guid>());
        var rank = rankMap.TryGetValue(restaurantId, out var rk) ? rk : 0;
        var score = BuildRankBasedScores(session.TopRestaurantIds ?? new List<Guid>())
                            .TryGetValue(restaurantId, out var s) ? s : 0;
        var topDishIdSet = new HashSet<Guid>(
            session.TopDishIds?.Take(7) ?? Enumerable.Empty<Guid>());

        return ResultMappers.ToPickResultDto(final, score, rank, topDishIdSet);
    }

    // ── Private helpers ───────────────────────────────────────────────────────

    private async Task<List<Dish>> LoadOrderedDishesAsync(List<Guid> ids, CancellationToken ct)
    {
        if (ids.Count == 0)
            return new List<Dish>();

        var result = new List<Dish>(ids.Count);
        foreach (var id in ids)
        {
            var dish = await _dishRepo.GetDishByIdAsync(id, ct);
            if (dish is not null)
                result.Add(dish);
        }
        return result;
    }

    private static List<Restaurant> ReorderById(List<Restaurant> items, List<Guid> orderedIds)
    {
        var map = items.ToDictionary(r => r.Id);
        return orderedIds
            .Where(id => map.ContainsKey(id))
            .Select(id => map[id])
            .ToList();
    }

    private static Dictionary<Guid, double> BuildRankBasedScores(List<Guid> orderedIds)
    {
        var map = new Dictionary<Guid, double>();
        int index = 0;

        foreach (var id in orderedIds)
        {
            map[id] = Math.Round(1.0 - index * 0.07, 4);
            index++;
        }
        return map;
    }

    private static Dictionary<Guid, int> BuildRankMap(List<Guid> orderedIds)
    {
        var map = new Dictionary<Guid, int>(orderedIds.Count);
        for (int i = 0; i < orderedIds.Count; i++)
        {
            map[orderedIds[i]] = i + 1;
        }
        return map;
    }

    private static List<Guid> PickTwoRandom(List<Guid> source)
    {
        var copy = source.ToList();
        var rng = Random.Shared;
        for (int i = copy.Count - 1; i > copy.Count - 3; i--)
        {
            int j = rng.Next(0, i + 1);
            (copy[i], copy[j]) = (copy[j], copy[i]);
        }
        return copy.TakeLast(2).ToList();
    }

    private static void ValidateIsHost(Session session, Guid callerId)
    {
        if (session.HostId != callerId)
            throw new NotHostException();
    }
}
