using csharp_url_shortener_api.Models;

namespace csharp_url_shortener_api.Interfaces.Repositories;

public interface IUrlRepository
{
    Task<Url> CreateUrl(Url url);

    Task<IList<Url>> GetUserCreatedUrls(int userId);

    Task<IList<Url>> GetAllUrls();
}