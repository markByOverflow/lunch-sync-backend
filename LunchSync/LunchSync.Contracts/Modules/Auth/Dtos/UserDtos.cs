namespace LunchSync.Contracts.Modules.Auth.Dtos;

using LunchSync.Contracts.Common.Enums;

public sealed record UserDto
{
    // Id cua nguoi dung
    public Guid Id { get; init; }
    // Ma dinh danh tu Cognito
    public string CognitoSub { get; init; } = null!;
    // Email dang nhap
    public string Email { get; init; } = null!;
    // Ho ten hien thi
    public string? FullName { get; init; }
    // Vai tro nguoi dung
    public UserRole Role { get; init; }
    // Trang thai hoat dong
    public bool IsActive { get; init; }
    // Thoi diem tao
    public DateTime CreatedAt { get; init; }
    // Thoi diem cap nhat
    public DateTime? UpdatedAt { get; init; }
}

public sealed record CreateUserRequest
{
    // Ma dinh danh tu Cognito
    public string CognitoSub { get; init; } = null!;
    // Email dang nhap
    public string Email { get; init; } = null!;
    // Ho ten hien thi
    public string? FullName { get; init; }
}

public sealed record UpdateUserRequest
{
    // Ma dinh danh tu Cognito
    public string CognitoSub { get; init; } = null!;
    // Email dang nhap
    public string Email { get; init; } = null!;
    // Ho ten hien thi
    public string? FullName { get; init; }
}
