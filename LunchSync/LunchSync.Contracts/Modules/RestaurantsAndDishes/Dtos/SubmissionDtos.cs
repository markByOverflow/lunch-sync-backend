namespace LunchSync.Contracts.Modules.RestaurantsAndDishes.Dtos;

using LunchSync.Contracts.Common.Enums;

public sealed record SubmissionDto
{
    // Id de xuat
    public Guid Id { get; init; }
    // Id nguoi tao de xuat
    public Guid UserId { get; init; }
    // Ten nha hang de xuat
    public string RestaurantName { get; init; } = null!;
    // Dia chi nha hang
    public string Address { get; init; } = null!;
    // Duong dan Google Maps
    public string? GoogleMapsUrl { get; init; }
    // Muc gia
    public PriceTier? PriceTier { get; init; }
    // Ghi chu
    public string? Note { get; init; }
    // Trang thai duyet
    public SubmissionStatus Status { get; init; }
    // Id nguoi duyet
    public Guid? ReviewedBy { get; init; }
    // Thoi diem duyet
    public DateTime? ReviewedAt { get; init; }
    // Thoi diem tao
    public DateTime CreatedAt { get; init; }
    // Thoi diem cap nhat
    public DateTime? UpdatedAt { get; init; }
}

public sealed record CreateSubmissionRequest
{
    // Id nguoi tao de xuat
    public Guid UserId { get; init; }
    // Ten nha hang de xuat
    public string RestaurantName { get; init; } = null!;
    // Dia chi nha hang
    public string Address { get; init; } = null!;
    // Duong dan Google Maps
    public string? GoogleMapsUrl { get; init; }
    // Muc gia
    public PriceTier? PriceTier { get; init; }
    // Ghi chu
    public string? Note { get; init; }
}

public sealed record UpdateSubmissionRequest
{
    // Id nguoi tao de xuat
    public Guid UserId { get; init; }
    // Ten nha hang de xuat
    public string RestaurantName { get; init; } = null!;
    // Dia chi nha hang
    public string Address { get; init; } = null!;
    // Duong dan Google Maps
    public string? GoogleMapsUrl { get; init; }
    // Muc gia
    public PriceTier? PriceTier { get; init; }
    // Ghi chu
    public string? Note { get; init; }
}
