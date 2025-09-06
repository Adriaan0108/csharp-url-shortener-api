namespace csharp_url_shortener_api.Dtos;

public class UrlWithClickCountDto
{
    public UrlWithClickCountDto() {} // explicit parameterless constructor for Mapperly
    public string OriginalUrl { get; set; }
    
    public string ShortUrl { get; set; }
    
    public decimal AmountEarned { get; set; }
    
    public int CreatedBy { get; set; }
    
    public int Id  { get; set; }
    
    public DateTime CreatedAt  { get; set; }
    
    public int ClickCount { get; set; }
}