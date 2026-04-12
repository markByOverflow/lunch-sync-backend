using LunchSync.Core.Common.Abstractions;
using LunchSync.Core.Common.Enums;
using LunchSync.Core.Modules.RestaurantsAndDishes.Entities;
namespace LunchSync.Core.Modules.Auth.Entities;

public class User : BaseEntity
{
    public string CognitoSub { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? FullName { get; set; }
    public UserRole Role { get; set; } = UserRole.User;
    public Boolean IsActive { get; set; } = true;

}
