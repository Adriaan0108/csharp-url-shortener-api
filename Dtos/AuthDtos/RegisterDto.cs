namespace csharp_url_shortener_api.Dtos.AuthDtos;

public class RegisterDto
{
    public string Username { get; set; }
    public string Password { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
}