using LunchSync.Contracts.Common.Enums;
using LunchSync.Contracts.Modules.Auth.Dtos;
using LunchSync.Core.Modules.Auth.Entities;

namespace LunchSync.Core.Mappings;

public static class AuthMappingExtensions
{
    public static UserDto ToDto(this User entity)
    {
        // Null safety: bao ve khi entity null
        ArgumentNullException.ThrowIfNull(entity);

        // Map tung truong tu entity sang DTO
        return new UserDto
        {
            Id = entity.Id,
            CognitoSub = entity.CognitoSub,
            Email = entity.Email,
            FullName = entity.FullName,
            // Chuyen doi enum bang Enum.Parse de tranh ep kieu truc tiep
            Role = Enum.Parse<UserRole>(entity.Role.ToString()),
            IsActive = entity.IsActive,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }

    public static User ToEntity(this CreateUserRequest request)
    {
        // Null safety: bao ve khi request null
        ArgumentNullException.ThrowIfNull(request);

        // Map tung truong tu request sang entity (khong map truong he thong)
        return new User
        {
            CognitoSub = request.CognitoSub,
            Email = request.Email,
            FullName = request.FullName
        };
    }

    public static void ApplyTo(this UpdateUserRequest request, User entity)
    {
        // Null safety: bao ve khi request hoac entity null
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(entity);

        // Map tung truong tu request sang entity hien tai (khong map truong he thong)
        entity.CognitoSub = request.CognitoSub;
        entity.Email = request.Email;
        entity.FullName = request.FullName;
    }
}
