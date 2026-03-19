namespace LunchSync.Contracts.Modules.RestaurantsAndDishes.Dtos;

public sealed record SubmissionPhotoDto
{
    // Id anh de xuat
    public Guid Id { get; init; }
    // Id de xuat
    public Guid SubmissionId { get; init; }
    // Duong dan anh
    public string PhotoUrl { get; init; } = null!;
    // Thu tu hien thi
    public int DisplayOrder { get; init; }
    // Thoi diem tao
    public DateTime CreatedAt { get; init; }
    // Thoi diem cap nhat
    public DateTime? UpdatedAt { get; init; }
}

public sealed record CreateSubmissionPhotoRequest
{
    // Id de xuat
    public Guid SubmissionId { get; init; }
    // Duong dan anh
    public string PhotoUrl { get; init; } = null!;
    // Thu tu hien thi
    public int DisplayOrder { get; init; }
}

public sealed record UpdateSubmissionPhotoRequest
{
    // Id de xuat
    public Guid SubmissionId { get; init; }
    // Duong dan anh
    public string PhotoUrl { get; init; } = null!;
    // Thu tu hien thi
    public int DisplayOrder { get; init; }
}
