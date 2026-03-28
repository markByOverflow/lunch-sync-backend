using LunchSync.Core.Exceptions;

namespace LunchSync.Core.Modules.RestaurantsAndDishes;

public class CollectionService : ICollectionService
{
    private readonly ICollectionRepository _collectionRepository;

    public CollectionService(ICollectionRepository collectionRepository)
    {
        _collectionRepository = collectionRepository;
    }

    public async Task<IEnumerable<CollectionSummaryRes>> GetAllActiveCollectionsAsync()
    {
        var collections = await _collectionRepository.GetAllActiveCollectionsAsync();

        return collections.Select(ResDisColMappers.ToCollectionSummaryRes);
    }

    public async Task<CollectionDetailRes?> GetCollectionDetailAsync(Guid id)
    {
        var collection = await _collectionRepository.GetCollectionByIdAsync(id) ?? throw new CollectionNotFoundException(id);

        return ResDisColMappers.ToCollectionDetailRes(collection);

    }
}
