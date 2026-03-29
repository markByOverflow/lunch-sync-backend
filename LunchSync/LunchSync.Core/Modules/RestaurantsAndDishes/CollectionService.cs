using LunchSync.Core.Exceptions;

namespace LunchSync.Core.Modules.RestaurantsAndDishes;

public class CollectionService : ICollectionService
{
    private readonly ICollectionRepository _collectionRepository;

    public CollectionService(ICollectionRepository collectionRepository)
    {
        _collectionRepository = collectionRepository;
    }

    public async Task<List<CollectionSummaryRes>> GetAllActiveCollectionsAsync(CancellationToken ct = default)
    {
        var collections = await _collectionRepository.GetAllActiveCollectionsAsync(ct);

        return collections.Select(c => c.ToCollectionSummaryRes()).ToList();
    }

    public async Task<CollectionDetailRes?> GetCollectionDetailAsync(Guid id, CancellationToken ct = default)
    {
        var collection = await _collectionRepository.GetCollectionByIdAsync(id, ct) ?? throw new CollectionNotFoundException(id);

        return collection.ToCollectionDetailRes();

    }
}
