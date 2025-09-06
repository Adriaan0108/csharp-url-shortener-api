using csharp_url_shortener_api.Interfaces.Services;
using System.IdentityModel.Tokens.Jwt;

namespace csharp_url_shortener_api.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int GetCurrentUserId()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null || !user.Identity.IsAuthenticated)
            throw new UnauthorizedAccessException("User is not authenticated");

        // Only look for the standard claim
        var userIdClaim = user.FindFirst(JwtRegisteredClaimNames.Sub);
        
        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
            throw new UnauthorizedAccessException("Invalid user ID in token");

        return userId;
    }

    public string GetCurrentUserUsername()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null || !user.Identity.IsAuthenticated)
            throw new UnauthorizedAccessException("User is not authenticated");

        return user.FindFirst(JwtRegisteredClaimNames.PreferredUsername)?.Value
               ?? throw new UnauthorizedAccessException("Username not found in token");
    }

    public bool IsAuthenticated()
    {
        return _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    }
}