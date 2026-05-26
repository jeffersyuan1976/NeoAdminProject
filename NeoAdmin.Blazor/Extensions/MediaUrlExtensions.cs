using Microsoft.AspNetCore.Components;

namespace NeoAdmin.Blazor.Extensions;

public static class MediaUrlExtensions
{
    /// <summary>将相对路径转为绝对地址；http/https/data 原样返回。</summary>
    /// <example>
    /// <code>
    /// var src = navigation.ToMediaUrl("/uploads/logo.png");
    /// // → "https://localhost:5001/uploads/logo.png"
    /// </code>
    /// </example>
    public static string? ToMediaUrl(this NavigationManager navigation, string? url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            return null;
        }

        if (url.StartsWith("http://", StringComparison.OrdinalIgnoreCase)
            || url.StartsWith("https://", StringComparison.OrdinalIgnoreCase)
            || url.StartsWith("data:", StringComparison.OrdinalIgnoreCase))
        {
            return url;
        }

        return navigation.ToAbsoluteUri(url).ToString();
    }
}
