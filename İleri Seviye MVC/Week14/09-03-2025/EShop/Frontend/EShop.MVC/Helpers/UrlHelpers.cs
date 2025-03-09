using System;
using System.Reflection.Metadata;
using EShop.MVC.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EShop.MVC.Helpers
{
    public static class UrlHelpers
    {
        //http://lochalhost:5210/images/product/elektril.png
        public static string ApiContent(this IUrlHelper urlHelper, string? path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }
            var baseUrl =urlHelper.ActionContext.HttpContext.GetApiBaseUrl().TrimEnd('/');
            baseUrl = baseUrl.EndsWith("/api") ? baseUrl[..^4]: baseUrl;
            
          
            return $"{baseUrl}{path}";


        }
    }
}
