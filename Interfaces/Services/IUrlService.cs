using csharp_url_shortener_api.Dtos;
using csharp_url_shortener_api.Models;

namespace csharp_url_shortener_api.Interfaces.Services;

public interface IUrlService
{
    Task<Url> CreateUrl(CreateUrlDto createUrlDto);

    Task<IList<UrlWithClickCountDto>> GetUserCreatedUrls();

    Task<IList<UrlWithClickCountDto>> GetAllUrls();
}