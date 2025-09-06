using csharp_url_shortener_api.Data;
using csharp_url_shortener_api.Interfaces.Repositories;
using csharp_url_shortener_api.Models;
using Microsoft.EntityFrameworkCore;

namespace csharp_url_shortener_api.Repositories;

public class UrlRepository : IUrlRepository
{
    private readonly DataContext _context;

    public UrlRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Url> CreateUrl(Url url)
    {
        await _context.Urls.AddAsync(url);
        await _context.SaveChangesAsync();
        return url;
    }

    public async Task<IList<Url>> GetUserCreatedUrls(int userId)
    {
        return await _context.Urls
            .Where(u => u.CreatedBy == userId)
            .Include(u => u.UrlClicks)
            .ToListAsync();
    }
}