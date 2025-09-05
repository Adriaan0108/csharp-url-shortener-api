using csharp_url_shortener_api.Models;

namespace csharp_url_shortener_api.Interfaces.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user);
}