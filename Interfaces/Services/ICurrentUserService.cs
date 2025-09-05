namespace csharp_url_shortener_api.Interfaces.Services;

public interface ICurrentUserService
{
    int GetCurrentUserId();

    string GetCurrentUserUsername();

    bool IsAuthenticated();
}