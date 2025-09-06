using csharp_url_shortener_api.Dtos;
using csharp_url_shortener_api.Helpers;
using csharp_url_shortener_api.Interfaces.Repositories;
using csharp_url_shortener_api.Interfaces.Services;
using csharp_url_shortener_api.Models;

namespace csharp_url_shortener_api.Services;

public class UrlService : IUrlService
{
    private readonly IUrlRepository _urlRepository;
    
    private readonly ICurrentUserService _currentUserService;
    
    public UrlService(IUrlRepository urlRepository,  ICurrentUserService currentUserService)
    {
        _urlRepository = urlRepository;
        _currentUserService = currentUserService;
    }

    public async Task<Url> CreateUrl(CreateUrlDto createUrlDto)
    {
       var url = MappingProfile.ToUrl(createUrlDto);

       var userId = _currentUserService.GetCurrentUserId();
       
       url.CreatedBy = userId;

       url.ShortUrl = UrlShortenerHelper.GenerateShortCode(8);
       
       return await _urlRepository.CreateUrl(url);
    }

    public async Task<IList<UrlWithClickCountDto>> GetUserCreatedUrls()
    {
        var userId = _currentUserService.GetCurrentUserId();
        var urls = await  _urlRepository.GetUserCreatedUrls(userId);
        
        var result = urls
            .Select(u =>
            {
                var dto = MappingProfile.ToUrlWithClickCountDto(u); // map base properties
                dto.ClickCount = u.UrlClicks?.Count ?? 0;           // manually set ClickCount
                return dto;
            })
            .ToList();

        return result;
    }
    
    public async Task<IList<UrlWithClickCountDto>> GetAllUrls()
    {
        var urls = await  _urlRepository.GetAllUrls();
        
        var result = urls
            .Select(u =>
            {
                var dto = MappingProfile.ToUrlWithClickCountDto(u); 
                dto.ClickCount = u.UrlClicks?.Count ?? 0;           
                return dto;
            })
            .ToList();

        return result;
    }
}