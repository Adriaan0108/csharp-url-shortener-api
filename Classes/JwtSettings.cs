namespace csharp_url_shortener_api.Classes;

public class JwtSettings
{
    public string Key { get; set; }

    public string Issuer { get; set; }

    public string Audience { get; set; }
}