using LunchSync.Core.Modules.RestaurantsAndDishes;

using Microsoft.AspNetCore.Mvc;

namespace LunchSync.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController : ControllerBase
{
    private readonly IRestaurantService _restaurantService;

    public RestaurantsController(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

    // GET /restaurants/{id}
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(RestaurantDetailRes), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRestaurantById(Guid id, CancellationToken ct)
    {
        var result = await _restaurantService.GetRestaurantWithDishesAsync(id, ct);
        return Ok(result);
    }
}
