using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using NeoAdmin.Blazor.Auth;
using NeoAdmin.Blazor.Menus;
using NeoAdmin.Blazor.Services;

namespace NeoAdmin.Blazor.Mvc;

/// <summary>
/// API 路径权限过滤器（对齐旧版 AdminOmniFilter）。
/// </summary>
public sealed class NeoAdminPermissionFilter : IAsyncActionFilter
{
  private readonly MenuPermissionService _permissionService;
  private readonly NeoAdminAuthService _authService;
  private readonly ILogger<NeoAdminPermissionFilter> _logger;

  public NeoAdminPermissionFilter(
    MenuPermissionService permissionService,
    NeoAdminAuthService authService,
    ILogger<NeoAdminPermissionFilter> logger)
  {
    _permissionService = permissionService;
    _authService = authService;
    _logger = logger;
  }

  public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
  {
    if (context.ActionDescriptor is not ControllerActionDescriptor actionDescriptor)
    {
      await next();
      return;
    }

    AllowAnonymousAttribute? allowAnonymous =
      actionDescriptor.MethodInfo.GetCustomAttribute<AllowAnonymousAttribute>()
      ?? actionDescriptor.ControllerTypeInfo.GetCustomAttribute<AllowAnonymousAttribute>();

    if (allowAnonymous is not null)
    {
      await next();
      return;
    }

    string requestPath = context.HttpContext.Request.Path.Value ?? string.Empty;
    if (!requestPath.StartsWith("/api", StringComparison.OrdinalIgnoreCase))
    {
      await next();
      return;
    }

    if (requestPath.StartsWith("/api/community/", StringComparison.OrdinalIgnoreCase))
    {
      await next();
      return;
    }

    string? token = GetBearerToken(context.HttpContext);
    ApiResult<UserSummaryResponse> check = await _authService.CheckAsync(token);
    if (!check.Succeeded || check.Data is null)
    {
      context.Result = new JsonResult(ApiResult.RequireLogin)
      {
        StatusCode = StatusCodes.Status401Unauthorized
      };
      return;
    }

    (string groupPath, string? actionName) = MenuPathHelper.ParseApiRequestPath(requestPath);
    if (string.IsNullOrWhiteSpace(groupPath))
    {
      await next();
      return;
    }

    bool allowed = string.IsNullOrWhiteSpace(actionName)
      ? await _permissionService.HasApiAsync(groupPath)
      : await _permissionService.HasApiAsync(groupPath, actionName);

    if (allowed)
    {
      await next();
      return;
    }

    _logger.LogWarning(
      "API 权限拒绝：UserId={UserId}，Path={Path}，Group={Group}，Action={Action}",
      check.Data.Id,
      requestPath,
      groupPath,
      actionName);

    context.Result = new JsonResult(ApiResult.NoPermission)
    {
      StatusCode = StatusCodes.Status401Unauthorized
    };
  }

  private static string? GetBearerToken(HttpContext httpContext)
  {
    string? authorization = httpContext.Request.Headers.Authorization.FirstOrDefault();
    if (string.IsNullOrWhiteSpace(authorization))
    {
      return null;
    }

    return authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)
      ? authorization[7..].Trim()
      : authorization.Trim();
  }
}
