namespace LunchSync.Contracts.Modules.Sessions.Dtos;

public sealed record ParticipantDto
{
    // Id nguoi tham gia
    public Guid Id { get; init; }
    // Id phien
    public Guid SessionId { get; init; }
    // Id nguoi dung (co the null neu an danh)
    public Guid? UserId { get; init; }
    // Biet danh hien thi
    public string Nickname { get; init; } = null!;
    // Vector so thich
    public IReadOnlyList<float>? PrefVector { get; init; }
    // Thoi diem tham gia
    public DateTime JoinedAt { get; init; }
    // Thoi diem da vote
    public DateTime? VotedAt { get; init; }
    // Thoi diem tao
    public DateTime CreatedAt { get; init; }
    // Thoi diem cap nhat
    public DateTime? UpdatedAt { get; init; }
}

public sealed record CreateParticipantRequest
{
    // Id phien
    public Guid SessionId { get; init; }
    // Id nguoi dung (co the null neu an danh)
    public Guid? UserId { get; init; }
    // Biet danh hien thi
    public string Nickname { get; init; } = null!;
    // Vector so thich
    public IReadOnlyList<float>? PrefVector { get; init; }
}

public sealed record UpdateParticipantRequest
{
    // Id phien
    public Guid SessionId { get; init; }
    // Id nguoi dung (co the null neu an danh)
    public Guid? UserId { get; init; }
    // Biet danh hien thi
    public string Nickname { get; init; } = null!;
    // Vector so thich
    public IReadOnlyList<float>? PrefVector { get; init; }
}
