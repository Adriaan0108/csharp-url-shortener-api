using NanoidDotNet;

namespace csharp_url_shortener_api.Helpers;

public static class UrlShortenerHelper
{
    public static string GenerateShortCode(int length = 8)
    {
        return Nanoid.Generate(size: length);
    }
}
    
