namespace csharp_url_shortener_api.Interfaces.Services;

public interface IPasswordHasherService
{
    string HashPassword(string password);

    bool VerifyPassword(string password, string hashedPassword);
}