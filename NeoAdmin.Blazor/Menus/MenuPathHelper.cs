namespace NeoAdmin.Blazor.Menus;

/// <summary>
/// 菜单路径规范化与比较（对齐旧版 AuthPath 的大小写与斜杠规则）。
/// </summary>
public static class MenuPathHelper
{
  private static readonly HashSet<string> ExemptPageKeys = new(StringComparer.OrdinalIgnoreCase)
  {
    "login",
    "forbidden"
  };

  public static string NormalizePagePath(string? path)
  {
    if (string.IsNullOrWhiteSpace(path))
    {
      return "/";
    }

    path = path.Split('?', 2)[0].Trim();
    if (path == "/")
    {
      return "/";
    }

    path = path.TrimEnd('/');
    return path.StartsWith('/') ? path : "/" + path;
  }

  public static string NormalizeCompareKey(string? path)
  {
    if (string.IsNullOrWhiteSpace(path))
    {
      return string.Empty;
    }

    if (path == "/")
    {
      return "/";
    }

    return path.Trim().Trim('/').ToLowerInvariant();
  }

  public static bool PathsEqual(string? left, string? right) =>
    string.Equals(NormalizeCompareKey(left), NormalizeCompareKey(right), StringComparison.Ordinal);

  public static bool IsExemptPage(string? path) =>
    ExemptPageKeys.Contains(NormalizeCompareKey(path));

  /// <summary>
  /// 从 HTTP 请求路径解析 API 菜单组与动作（如 /api/login@Login）。
  /// </summary>
  public static (string GroupPath, string? ActionName) ParseApiRequestPath(string? requestPath)
  {
    if (string.IsNullOrWhiteSpace(requestPath))
    {
      return (string.Empty, null);
    }

    string path = requestPath;
    if (path.StartsWith("/api/", StringComparison.OrdinalIgnoreCase))
    {
      path = path[5..];
    }
    else if (path.StartsWith("/api", StringComparison.OrdinalIgnoreCase))
    {
      path = path[4..].TrimStart('/');
    }

    path = path.Trim('/');
    int atIndex = path.IndexOf('@');
    if (atIndex > 0)
    {
      return (
        path[..atIndex].Trim('/'),
        path[(atIndex + 1)..].Trim('/'));
    }

    string[] parts = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
    if (parts.Length >= 2)
    {
      return (parts[0], parts[^1]);
    }

    return (path, null);
  }
}
