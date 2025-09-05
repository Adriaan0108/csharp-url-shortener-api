using csharp_url_shortener_api.Dtos.AuthDtos;

namespace csharp_url_shortener_api.Interfaces.Services;

public interface IAuthService
{
    Task<LoginResponseDto> Login(LoginDto loginDto);

    Task<LoginResponseDto> Register(RegisterDto registerDto);
}