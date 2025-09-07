namespace csharp_url_shortener_api.Dtos.ProfitDtos;

public class ProfitGrowthCurrentMonthDto
{
    public DateTime Date { get; set; }
    
    public decimal? ProfitGrowthPercentage { get; set; } // nullable to indicate N/A
}