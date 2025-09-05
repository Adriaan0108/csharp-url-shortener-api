using csharp_url_shortener_api.Dtos.AuthDtos;
using csharp_url_shortener_api.Models;
using Riok.Mapperly.Abstractions;

namespace csharp_url_shortener_api.Helpers;

[Mapper]
public static partial class MappingProfile
{
    // auth
    public static partial LoginResponseDto ToLoginResponseDto(User user);
    
    public static partial User ToUser(RegisterDto registerDto);
}