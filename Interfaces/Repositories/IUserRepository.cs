using csharp_url_shortener_api.Models;

namespace csharp_url_shortener_api.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User> GetUserByUsername(string username);

    Task<User> CreateUser(User user);
}