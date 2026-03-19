namespace LunchSync.Contracts.Modules.Sessions.Dtos;

using LunchSync.Contracts.Common.Enums;

public sealed record SessionDto
{
    // Id phien
    public Guid Id { get; init; }
    // Ma PIN phien
    public string Pin { get; init; } = null!;
    // Id chu phong
    public Guid HostId { get; init; }
    // Id bo suu tap
    public Guid CollectionId { get; init; }
    // Muc gia
    public PriceTier PriceTier { get; init; }
    // Trang thai phien
    public SessionStatus Status { get; init; }
    // Vector nhom
    public IReadOnlyList<float>? GroupVector { get; init; }
    // Danh sach Id mon an top
    public IReadOnlyList<Guid>? TopDishIds { get; init; }
    // Danh sach Id nha hang top
    public IReadOnlyList<Guid>? TopRestaurantIds { get; init; }
    // Danh sach Id bi boom loai
    public IReadOnlyList<Guid>? BoomEliminatedIds { get; init; }
    // Id nha hang cuoi cung
    public Guid? FinalRestaurantId { get; init; }
    // Thoi diem bat dau vote
    public DateTime? VotingStartedAt { get; init; }
    // Thoi diem het han
    public DateTime? ExpiresAt { get; init; }
    // Thoi diem tao
    public DateTime CreatedAt { get; init; }
    // Thoi diem cap nhat
    public DateTime? UpdatedAt { get; init; }
}

public sealed record CreateSessionRequest
{
    // Ma PIN phien
    public string Pin { get; init; } = null!;
    // Id chu phong
    public Guid HostId { get; init; }
    // Id bo suu tap
    public Guid CollectionId { get; init; }
    // Muc gia
    public PriceTier PriceTier { get; init; }
}

public sealed record UpdateSessionRequest
{
    // Ma PIN phien
    public string Pin { get; init; } = null!;
    // Id chu phong
    public Guid HostId { get; init; }
    // Id bo suu tap
    public Guid CollectionId { get; init; }
    // Muc gia
    public PriceTier PriceTier { get; init; }
}
