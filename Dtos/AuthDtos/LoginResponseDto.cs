namespace csharp_url_shortener_api.Dtos.AuthDtos;

public class LoginResponseDto
{
    public string Token { get; set; }

    public string Username { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
    
    public int Id { get; set; }

    public DateTime ExpiresAt { get; set; }
}