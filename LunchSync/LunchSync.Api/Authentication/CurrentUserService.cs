using System.Security.Claims;
using LunchSync.Core.Common.Auth;
using LunchSync.Core.Common.Interfaces;

namespace LunchSync.Api.Authentication;

public sealed class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

    // Gom claim vao mot shape de controller va service doc nhat quan.
    public bool IsAuthenticated =>
        User?.Identity?.IsAuthenticated ?? false;

    // Host flow hien tai van dung Cognito sub lam identity xuyen suot.
    public string? UserId =>
        CognitoSub
        ?? User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    public string? CognitoSub =>
        User?.FindFirst("sub")?.Value
        ?? User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    public Guid? LocalUserId
    {
        get
        {
            var value = User?.FindFirst(AuthClaimTypes.LocalUserId)?.Value;
            return Guid.TryParse(value, out var userId) ? userId : null;
        }
    }

    public string? ActorType =>
        User?.FindFirst(AuthClaimTypes.ActorType)?.Value;

    public string? Email =>
        User?.FindFirst(AuthClaimTypes.Email)?.Value
        ?? User?.FindFirst("email")?.Value
        ?? User?.FindFirst(ClaimTypes.Email)?.Value;

    public string? Name =>
        User?.FindFirst(AuthClaimTypes.FullName)?.Value
        ?? User?.FindFirst("name")?.Value
        ?? User?.FindFirst(ClaimTypes.Name)?.Value;

    public string? Role =>
        User?.FindFirst(AuthClaimTypes.Role)?.Value
        ?? User?.FindFirst("cognito:groups")?.Value;

    public bool IsActive
    {
        get
        {
            var value = User?.FindFirst(AuthClaimTypes.IsActive)?.Value;
            return bool.TryParse(value, out var isActive)
                ? isActive
                : IsAuthenticated;
        }
    }
}
