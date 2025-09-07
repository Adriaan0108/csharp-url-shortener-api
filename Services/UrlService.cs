using csharp_url_shortener_api.Dtos;
using csharp_url_shortener_api.Dtos.ProfitDtos;
using csharp_url_shortener_api.Helpers;
using csharp_url_shortener_api.Interfaces.Repositories;
using csharp_url_shortener_api.Interfaces.Services;
using csharp_url_shortener_api.Models;

namespace csharp_url_shortener_api.Services
{
    public class UrlService : IUrlService
    {
        private readonly IUrlRepository _urlRepository;
        private readonly ICurrentUserService _currentUserService;

        public UrlService(IUrlRepository urlRepository, ICurrentUserService currentUserService)
        {
            _urlRepository = urlRepository;
            _currentUserService = currentUserService;
        }

        public async Task<Url> CreateUrl(CreateUrlDto createUrlDto)
        {
            var url = MappingProfile.ToUrl(createUrlDto);

            var userId = _currentUserService.GetCurrentUserId();

            url.CreatedBy = userId;
            url.ShortUrl = UrlShortenerHelper.GenerateShortCode(8);

            return await _urlRepository.CreateUrl(url);
        }

        public async Task<UrlClick> CreateUrlClick(CreateUrlClickDto createUrlClickDto)
        {
            var urlClick = MappingProfile.ToUrlClick(createUrlClickDto);

            var userId = _currentUserService.GetCurrentUserId();

            urlClick.UserId = userId;
            urlClick.ProfitSharingRuleId = 1; // hardcode for now

            var clicks = await _urlRepository.GetUrlClicks(createUrlClickDto.UrlId);

            // future idea - fetch ProfitSharingRuleId (by last created or active state) and pass its values into CalculateUrlClickAmountEarned
            var amountEarned = AmountEarnedHelper.CalculateUrlClickAmountEarned(clicks.Count);
            urlClick.AmountEarned = amountEarned;

            return await _urlRepository.CreateUrlClick(urlClick);
        }

        public async Task<IList<UrlWithClickCountDto>> GetUserCreatedUrls()
        {
            var userId = _currentUserService.GetCurrentUserId();
            var urls = await _urlRepository.GetUserCreatedUrls(userId);

            var result = urls
                .Select(u =>
                {
                    var dto = MappingProfile.ToUrlWithClickCountDto(u); // map base properties
                    dto.ClickCount = u.UrlClicks?.Count ?? 0;           // manually set ClickCount
                    return dto;
                })
                .ToList();

            return result;
        }

        public async Task<IList<UrlWithClickCountDto>> GetAllUrls()
        {
            var urls = await _urlRepository.GetAllUrls();

            var result = urls
                .Select(u =>
                {
                    var dto = MappingProfile.ToUrlWithClickCountDto(u);

                    dto.ClickCount = u.UrlClicks?.Count ?? 0;
                    dto.AmountEarned = u.UrlClicks?.Sum(c => c.AmountEarned) ?? 0;

                    return dto;
                })
                .ToList();

            return result;
        }

        public async Task<IList<ProfitGrowthCurrentMonthDto>> GetProfitGrowthForCurrentMonth()
        {
            var userId = _currentUserService.GetCurrentUserId();
            var urls = await _urlRepository.GetUserCreatedUrls(userId);

            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;

            // Group clicks by day and sum earnings
            var dailyTotals = new Dictionary<int, decimal>();

            foreach (var url in urls)
            {
                if (url.UrlClicks != null)
                {
                    foreach (var click in url.UrlClicks)
                    {
                        if (click.CreatedAt.Month == currentMonth && click.CreatedAt.Year == currentYear)
                        {
                            int day = click.CreatedAt.Day;

                            if (!dailyTotals.ContainsKey(day))
                            {
                                dailyTotals[day] = 0;
                            }

                            dailyTotals[day] += click.AmountEarned;
                        }
                    }
                }
            }

            var result = new List<ProfitGrowthCurrentMonthDto>();
            var daysInMonth = DateTime.DaysInMonth(currentYear, currentMonth);

            // Start from day 2 to compare with day 1
            for (int day = 2; day <= daysInMonth; day++)
            {
                var previousDay = day - 1;

                // Get earnings (default to 0 if no clicks)
                decimal previousDayEarnings = dailyTotals.ContainsKey(previousDay) ? dailyTotals[previousDay] : 0;
                decimal currentDayEarnings = dailyTotals.ContainsKey(day) ? dailyTotals[day] : 0;

                decimal? growthPercentage = null;

                // Assignment-friendly logic: show growth when there's meaningful comparison
                if (previousDayEarnings > 0)
                {
                    // Normal percentage calculation
                    growthPercentage = ((currentDayEarnings - previousDayEarnings) / previousDayEarnings) * 100;
                }
                else if (previousDayEarnings == 0 && currentDayEarnings > 0)
                {
                    // First day with earnings - show as 100% (started earning)
                    growthPercentage = 100;
                }
                // If both are 0, leave as null (no activity to show)

                result.Add(new ProfitGrowthCurrentMonthDto
                {
                    Date = new DateTime(currentYear, currentMonth, day),
                    ProfitGrowthPercentage = growthPercentage
                });
            }

            return result;
        }
    }
}
