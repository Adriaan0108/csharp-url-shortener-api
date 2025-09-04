namespace csharp_url_shortener_api.Models;

public class User : BaseModel
{
    public string Username { get; set; }
    
    public string Password { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public ICollection<UrlClick> UrlClicks { get; set; }
    
    public ICollection<Url> UrlsCreated { get; set; }
}