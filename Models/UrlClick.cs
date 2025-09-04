namespace csharp_url_shortener_api.Models;

public class UrlClick : BaseModel
{
    public int UserId { get; set; }
    
    public User User { get; set; }
    
    public int UrlId { get; set; }
    
    public Url Url { get; set; }
    
    public int ProfitSharingRuleId { get; set; }
    
    public ProfitSharingRule ProfitSharingRule { get; set; }
    
    public decimal AmountEarned { get; set; }
}