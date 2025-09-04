namespace csharp_url_shortener_api.Models;

public class Url : BaseModel
{
    public string OriginalUrl { get; set; }
    
    public string ShortUrl { get; set; }
    
    public int CreatedBy { get; set; }
    
    public User Creator { get; set; }
    
    public ICollection<UrlClick> UrlClicks { get; set; }
}