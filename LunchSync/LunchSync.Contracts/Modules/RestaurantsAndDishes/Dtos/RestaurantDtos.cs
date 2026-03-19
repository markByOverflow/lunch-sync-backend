namespace LunchSync.Contracts.Modules.RestaurantsAndDishes.Dtos;

using LunchSync.Contracts.Common.Enums;

public sealed record RestaurantDto
{
    // Id cua nha hang
    public Guid Id { get; init; }
    // Ten nha hang
    public string Name { get; init; } = null!;
    // Dia chi nha hang
    public string Address { get; init; } = null!;
    // Duong dan Google Maps
    public string? GoogleMapsUrl { get; init; }
    // Muc gia
    public PriceTier PriceTier { get; init; }
    // Diem danh gia
    public double? Rating { get; init; }
    // Anh dai dien
    public string? ThumbnailUrl { get; init; }
    // Trang thai nha hang
    public RestaurantStatus Status { get; init; }
    // Nguon du lieu nha hang
    public RestaurantSource Source { get; init; }
    // Thoi diem tao
    public DateTime CreatedAt { get; init; }
    // Thoi diem cap nhat
    public DateTime? UpdatedAt { get; init; }
}

public sealed record CreateRestaurantRequest
{
    // Ten nha hang
    public string Name { get; init; } = null!;
    // Dia chi nha hang
    public string Address { get; init; } = null!;
    // Duong dan Google Maps
    public string? GoogleMapsUrl { get; init; }
    // Muc gia
    public PriceTier PriceTier { get; init; }
    // Diem danh gia
    public double? Rating { get; init; }
    // Anh dai dien
    public string? ThumbnailUrl { get; init; }
}

public sealed record UpdateRestaurantRequest
{
    // Ten nha hang
    public string Name { get; init; } = null!;
    // Dia chi nha hang
    public string Address { get; init; } = null!;
    // Duong dan Google Maps
    public string? GoogleMapsUrl { get; init; }
    // Muc gia
    public PriceTier PriceTier { get; init; }
    // Diem danh gia
    public double? Rating { get; init; }
    // Anh dai dien
    public string? ThumbnailUrl { get; init; }
}
