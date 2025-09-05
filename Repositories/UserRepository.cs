using csharp_url_shortener_api.Data;
using csharp_url_shortener_api.Interfaces.Repositories;
using csharp_url_shortener_api.Models;
using Microsoft.EntityFrameworkCore;

namespace csharp_url_shortener_api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<User> GetUserByUsername(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }
    
    public async Task<User> CreateUser(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }
}