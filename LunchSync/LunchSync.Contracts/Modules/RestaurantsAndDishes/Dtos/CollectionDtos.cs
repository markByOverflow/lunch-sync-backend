namespace LunchSync.Contracts.Modules.RestaurantsAndDishes.Dtos;

using LunchSync.Contracts.Common.Enums;

public sealed record CollectionDto
{
    // Id cua bo suu tap
    public Guid Id { get; init; }
    // Ten bo suu tap
    public string Name { get; init; } = null!;
    // Mo ta bo suu tap
    public string? Description { get; init; }
    // Vi do moc dinh vi
    public double? LandmarkLat { get; init; }
    // Kinh do moc dinh vi
    public double? LandmarkLon { get; init; }
    // Ban kinh bao phu (met)
    public int CoverageRadiusMeters { get; init; }
    // Trang thai bo suu tap
    public CollectionStatus Status { get; init; }
    // Thoi diem tao
    public DateTime CreatedAt { get; init; }
    // Thoi diem cap nhat
    public DateTime? UpdatedAt { get; init; }
}

public sealed record CreateCollectionRequest
{
    // Ten bo suu tap
    public string Name { get; init; } = null!;
    // Mo ta bo suu tap
    public string? Description { get; init; }
    // Vi do moc dinh vi
    public double? LandmarkLat { get; init; }
    // Kinh do moc dinh vi
    public double? LandmarkLon { get; init; }
    // Ban kinh bao phu (met)
    public int CoverageRadiusMeters { get; init; }
}

public sealed record UpdateCollectionRequest
{
    // Ten bo suu tap
    public string Name { get; init; } = null!;
    // Mo ta bo suu tap
    public string? Description { get; init; }
    // Vi do moc dinh vi
    public double? LandmarkLat { get; init; }
    // Kinh do moc dinh vi
    public double? LandmarkLon { get; init; }
    // Ban kinh bao phu (met)
    public int CoverageRadiusMeters { get; init; }
}
