using LunchSync.Core.Modules.RestaurantsAndDishes;

using Microsoft.AspNetCore.Mvc;

namespace LunchSync.Api.Controllers;

[ApiController]
[Route("api/collections")]
public class CollectionsController : ControllerBase
{
    private readonly ICollectionService _collectionService;

    public CollectionsController(ICollectionService collectionService)
    {
        _collectionService = collectionService;
    }

    // GET /api/collections
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _collectionService.GetAllActiveCollectionsAsync();
        return Ok(result);
    }

    // GET /api/collections/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await _collectionService.GetCollectionDetailAsync(id);
        return Ok(result);
    }
}
