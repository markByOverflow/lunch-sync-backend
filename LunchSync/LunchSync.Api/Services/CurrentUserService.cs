using System.Security.Claims;
using LunchSync.Core.Common.Auth;
using LunchSync.Core.Common.Interfaces;

namespace LunchSync.Api.Services;

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

    // Host/user va guest deu dung sub lam dinh danh chinh.
    public string? UserId =>
        User?.FindFirst("sub")?.Value
        ?? User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    public string? ActorType =>
        User?.FindFirst(AuthClaimTypes.ActorType)?.Value;

    public string? Email =>
        User?.FindFirst("email")?.Value
        ?? User?.FindFirst(ClaimTypes.Email)?.Value;

    public string? Name =>
        User?.FindFirst("name")?.Value
        ?? User?.FindFirst(ClaimTypes.Name)?.Value
        ?? User?.FindFirst(AuthClaimTypes.Nickname)?.Value;

    // Cognito groups tam thoi la nguon role cho principal.
    public IReadOnlyList<string> Roles =>
        User?.FindAll("cognito:groups")
             .Select(x => x.Value)
             .ToList()
        ?? new List<string>();

    public bool IsGuest =>
        string.Equals(ActorType, AuthActorTypes.Guest, StringComparison.Ordinal);
}
