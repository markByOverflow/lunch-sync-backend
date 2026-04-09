using LunchSync.Core.Modules.RestaurantsAndDishes;

using Microsoft.AspNetCore.Mvc;

namespace LunchSync.Api.Controllers;

[ApiController]
[Route("api/dishes")]
public class DishesController : ControllerBase
{
    private readonly IDishService _dishService;

    public DishesController(IDishService dishService)
    {
        _dishService = dishService;
    }

    // GET /dishes?category={category}&search={search}
    [HttpGet]
    [ProducesResponseType(typeof(DishListRes), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDishes(
        [FromQuery] string? category,
        [FromQuery] string? search,
        CancellationToken ct)
    {
        var result = await _dishService.GetDishesAsync(search, category, ct);
        return Ok(result);
    }

    // GET /dishes/{id}
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(DishDetailRes), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDishById(Guid id, CancellationToken ct)
    {
        var result = await _dishService.GetDishByIdAsync(id, ct);
        return Ok(result);
    }
}
