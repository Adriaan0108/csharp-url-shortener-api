using csharp_url_shortener_api.Dtos;
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
    
    // url
    public static partial Url ToUrl(CreateUrlDto createUrlDto);
    
    public static partial UrlWithClickCountDto ToUrlWithClickCountDto(Url url);
    
    public static partial UrlClick ToUrlClick(CreateUrlClickDto  createUrlClickDto);
}