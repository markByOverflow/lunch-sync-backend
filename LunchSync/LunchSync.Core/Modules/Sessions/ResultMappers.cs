using LunchSync.Core.Common.Enums;
using LunchSync.Core.Modules.RestaurantsAndDishes.Entities;
using LunchSync.Core.Modules.Sessions.Entities;

namespace LunchSync.Core.Modules.Sessions;

public static class ResultMappers
{
    // ── Dish ────────────────────────────────────────────────────────────────

    public static ScoredDishDto ToScoredDishDto(Dish dish, double score, int rank)
    {
        return new ScoredDishDto
        {
            Id = dish.Id,
            Name = dish.Name,
            Category = dish.Category,
            Score = Math.Round(score, 4),
            Rank = rank,
        };
    }

    // ── Restaurant ──────────────────────────────────────────────────────────

    public static RestaurantResultDto ToRestaurantResultDto(
        Restaurant restaurant,
        double score,
        int rank,
        List<string> matchedDishNames)
    {
        return new RestaurantResultDto
        {
            Id = restaurant.Id,
            Name = restaurant.Name,
            Address = restaurant.Address,
            GoogleMapsUrl = restaurant.GoogleMapsUrl,
            PriceTier = restaurant.PriceTier.ToString(),
            PriceDisplay = ResolvePriceDisplay(restaurant.PriceTier.ToString()),
            Rating = restaurant.Rating,
            ThumbnailUrl = restaurant.ThumbnailUrl,
            MatchedDishes = matchedDishNames,
            Score = Math.Round(score, 4),
            Rank = rank,
        };
    }

    /// <summary>
    /// Slim card — chỉ dùng trong eliminated list sau Boom.
    /// </summary>
    public static EliminatedDto ToEliminatedDto(Restaurant restaurant, int rank)
    {
        return new EliminatedDto
        {
            Id = restaurant.Id,
            Name = restaurant.Name,
            Rank = rank,
        };
    }

    // ── Composite: GetResults response ──────────────────────────────────────

    /// <summary>
    /// Build GET /results payload.
    /// Caller phải truyền vào:
    ///   - session (với TopRestaurantIds, BoomEliminatedIds, FinalRestaurantId đã populated)
    ///   - topDishes: Dish entities theo đúng thứ tự rank (index 0 = rank 1)
    ///   - dishScores: map DishId → score
    ///   - topRestaurants: Restaurant entities theo đúng thứ tự rank
    ///   - restaurantScores: map RestaurantId → score
    ///   - topDishIds: HashSet để check matched dishes nhanh
    /// </summary>
    public static GetResultsDto ToGetResultsDto(
        Session session,
        List<Dish> topDishes,
        Dictionary<Guid, double> dishScores,
        List<Restaurant> topRestaurants,
        Dictionary<Guid, double> restaurantScores,
        HashSet<Guid> topDishIdSet)
    {
        var dto = new GetResultsDto();

        // 1. Tạo bản đồ tra cứu nhanh
        var restaurantMap = topRestaurants.ToDictionary(r => r.Id);
        var originalRanks = BuildRankMap(session.TopRestaurantIds);

        // 2. Top dishes
        for (int i = 0; i < topDishes.Count; i++)
        {
            var dish = topDishes[i];
            var score = dishScores.GetValueOrDefault(dish.Id, 0);
            dto.TopDishes.Add(ToScoredDishDto(dish, score, rank: i + 1));
        }

        // 3. Xử lý Eliminated Set
        var eliminatedIds = session.BoomEliminatedIds ?? new List<Guid>();
        var eliminatedSet = eliminatedIds.ToHashSet();

        // 4. TopRestaurants — Full ranked list
        for (int i = 0; i < topRestaurants.Count; i++)
        {
            var r = topRestaurants[i];
            var score = restaurantScores.GetValueOrDefault(r.Id, 0);
            var matched = ResolveMatchedDishNames(r, topDishIdSet);
            dto.TopRestaurants.Add(ToRestaurantResultDto(r, score, rank: i + 1, matched));
        }

        // 5. Nếu đã qua giai đoạn "Boom" (có danh sách bị loại)
        if (eliminatedSet.Count > 0)
        {
            // Add vào danh sách Eliminated
            foreach (var id in eliminatedIds)
            {
                if (restaurantMap.TryGetValue(id, out var r))
                {
                    var rank = originalRanks.GetValueOrDefault(id, 0);
                    dto.Eliminated.Add(ToEliminatedDto(r, rank));
                }
            }

            // Xử lý Remaining (Top 5 trừ đi Eliminated)
            var top5Ids = session.TopRestaurantIds?.Take(5) ?? Enumerable.Empty<Guid>();
            foreach (var id in top5Ids)
            {
                if (eliminatedSet.Contains(id))
                    continue;
                if (!restaurantMap.TryGetValue(id, out var r))
                    continue;

                var score = restaurantScores.GetValueOrDefault(id, 0);
                var matched = ResolveMatchedDishNames(r, topDishIdSet);
                // Quan trọng: Lấy rank gốc từ map thay vì dùng index i biến động
                var rank = originalRanks.GetValueOrDefault(id, 0);
                dto.Remaining.Add(ToRestaurantResultDto(r, score, rank, matched));
            }
        }

        // Final restaurant (status = done)
        if (session.FinalRestaurantId.HasValue
    && restaurantMap.TryGetValue(session.FinalRestaurantId.Value, out var final))
        {
            // Dùng GetValueOrDefault để code sạch hơn
            var score = restaurantScores.GetValueOrDefault(final.Id, 0);
            var matched = ResolveMatchedDishNames(final, topDishIdSet);

            // Dùng lại rankMap đã khởi tạo ở trên, không gọi lại BuildRankMap nữa
            var rank = originalRanks.GetValueOrDefault(final.Id, 0);

            dto.FinalRestaurant = ToRestaurantResultDto(final, score, rank, matched);
        }

        return dto;
    }

    // ── Boom response ───────────────────────────────────────────────────────

    public static BoomResultDto ToBoomResultDto(
        List<Guid> eliminatedIds,
        List<Guid> remainingIds,
        Dictionary<Guid, Restaurant> restaurantMap,
        Dictionary<Guid, double> restaurantScores,
        Dictionary<Guid, int> rankMap,
        HashSet<Guid> topDishIdSet)
    {
        var dto = new BoomResultDto();

        foreach (var id in eliminatedIds)
        {
            if (!restaurantMap.TryGetValue(id, out var r))
                continue;
            var rank = rankMap.TryGetValue(id, out var rk) ? rk : 0;
            dto.Eliminated.Add(ToEliminatedDto(r, rank));
        }

        for (int i = 0; i < remainingIds.Count; i++)
        {
            var id = remainingIds[i];
            if (!restaurantMap.TryGetValue(id, out var r))
                continue;
            var score = restaurantScores.TryGetValue(id, out var s) ? s : 0;
            var rank = rankMap.TryGetValue(id, out var rk) ? rk : 0;
            var matched = ResolveMatchedDishNames(r, topDishIdSet);
            dto.Remaining.Add(ToRestaurantResultDto(r, score, rank, matched));
        }

        return dto;
    }

    // ── Pick response ───────────────────────────────────────────────────────

    public static PickResultDto ToPickResultDto(
        Restaurant restaurant,
        double score,
        int rank,
        HashSet<Guid> topDishIdSet)
    {
        var matched = ResolveMatchedDishNames(restaurant, topDishIdSet);
        return new PickResultDto
        {
            FinalRestaurant = ToRestaurantResultDto(restaurant, score, rank, matched),
            Status = SessionStatus.Done,
        };
    }

    // ── Private helpers ─────────────────────────────────────────────────────

    private static List<string> ResolveMatchedDishNames(Restaurant restaurant, HashSet<Guid> topDishIdSet)
    {
        if (restaurant.RestaurantDishes is null)
            return new List<string>();

        return restaurant.RestaurantDishes
            .Where(rd => topDishIdSet.Contains(rd.DishId) && rd.Dish is not null)
            .Select(rd => rd.Dish!.Name)
            .ToList();
    }

    private static Dictionary<Guid, int> BuildRankMap(IEnumerable<Guid>? orderedIds)
    {
        if (orderedIds is null)
            return new Dictionary<Guid, int>();

        var map = new Dictionary<Guid, int>();
        int rank = 1;

        foreach (var id in orderedIds)
        {
            map[id] = rank++;
        }

        return map;
    }

    private static string ResolvePriceDisplay(string priceTier)
    {
        return priceTier switch
        {
            "under_40k" => "Dưới 40k/phần",
            "40_70k" => "40–70k/phần",
            "70_120k" => "70–120k/phần",
            "over_120k" => "Trên 120k/phần",
            _ => priceTier
        };
    }
}
