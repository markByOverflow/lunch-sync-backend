using LunchSync.Core.Common.Enums;
using LunchSync.Core.Common.Abstractions;

namespace LunchSync.Core.Modules.RestaurantsAndDishes.Entities;
public class Restaurant : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string? GoogleMapsUrl { get; set; }
    public PriceTier PriceTier { get; set; } 
    public double? Rating { get; set; }
    public string? ThumbnailUrl { get; set; }
    public RestaurantStatus Status { get; set; } = RestaurantStatus.Active;
    public RestaurantSource Source { get; set; } = RestaurantSource.Admin;
    
    //Navigation Properties
    public ICollection<RestaurantCollection> RestaurantCollections { get; set; } = new List<RestaurantCollection>();
    public ICollection<RestaurantDish> RestaurantDishes { get; set; } = new List<RestaurantDish>();
}