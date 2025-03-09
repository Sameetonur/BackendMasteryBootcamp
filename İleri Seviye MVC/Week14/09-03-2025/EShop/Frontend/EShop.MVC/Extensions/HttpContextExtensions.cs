using System;

namespace EShop.MVC.Extensions
{
    public static class HttpContextExtensions
    {
        private static string? _apiBaseUrl;

        public static string? GetApiBaseUrl(this HttpContext context)
        {
            if (_apiBaseUrl == null)
            {
                var configuration = context.RequestServices.GetRequiredService<IConfiguration>();
                _apiBaseUrl = configuration.GetSection("ApiSettings:BaseUri").Value!;
            }
            return _apiBaseUrl;
        }
    }
}
