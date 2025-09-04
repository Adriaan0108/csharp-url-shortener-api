namespace csharp_url_shortener_api.Models;

public class ProfitSharingRule : BaseModel
{
    public decimal ClickAmount { get; set; }
    
    public double SharePercentage { get; set; }
    
    public double ShareIncreasePercentage { get; set; }
    
    public int IncreaseEveryNClicks { get; set; }
    
    public double MaxShareIncreasePercentage { get; set; }
    public ICollection<UrlClick> UrlClicks { get; set; }
}