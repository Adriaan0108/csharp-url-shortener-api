using csharp_url_shortener_api.Dtos;
using csharp_url_shortener_api.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace csharp_url_shortener_api.Controllers;

[Route("/url")]
[ApiController]
public class UrlController : ControllerBase
{
    private readonly IUrlService _urlService;

    public UrlController(IUrlService urlService)
    {
        _urlService = urlService;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateUrl([FromBody] CreateUrlDto createUrlDto)
    {
        var url = await _urlService.CreateUrl(createUrlDto);

        return Ok(url);
    }
    
    [HttpPost("click")]
    [Authorize]
    public async Task<IActionResult> CreateUrlClick([FromBody] CreateUrlClickDto createUrlClickDto)
    {
        var urlClick = await _urlService.CreateUrlClick(createUrlClickDto);

        return Ok(urlClick);
    }
    
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserCreatedUrls()
    {
        var urls = await _urlService.GetUserCreatedUrls();

        return Ok(urls);
    }
    
    [HttpGet("all")]
    [Authorize]
    public async Task<IActionResult> GetAllUrls()
    {
        var urls = await _urlService.GetAllUrls();

        return Ok(urls);
    }
}