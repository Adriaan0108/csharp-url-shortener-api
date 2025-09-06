namespace csharp_url_shortener_api.Helpers;

public static class AmountEarnedHelper
{
    public static decimal CalculateUrlClickAmountEarned(int totalClicks, int sharePerc = 10, int increaseNClicks = 5, int maxSharePerc = 80, decimal clickValue = 10m)
    {
        int increments = totalClicks / increaseNClicks;
        int maxIncrements = (maxSharePerc - sharePerc) / 10; // Stop calculating at max
        int actualIncrements = Math.Min(increments, maxIncrements);
    
        int currentShare = sharePerc + (actualIncrements * 10);
    
        decimal amountEarned = clickValue * currentShare / 100m;
        return amountEarned;
    }
}