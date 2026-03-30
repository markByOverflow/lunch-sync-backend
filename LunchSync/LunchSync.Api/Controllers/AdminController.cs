using LunchSync.Core.Modules.Admin;
using Microsoft.AspNetCore.Mvc;

namespace LunchSync.Api.Controllers;

[ApiController]
[Route("admin")]
public sealed class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpPost("test")]
    public async Task<IActionResult> Test()
    {
        await _adminService.BulkCreateRestaurantsAsync(Array.Empty<BulkCreateRestaurantRequest>());
        return Ok();
    }
}
