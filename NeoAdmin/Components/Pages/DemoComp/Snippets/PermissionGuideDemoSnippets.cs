namespace NeoAdmin.Components.Pages.DemoComp.Snippets;

internal static class PermissionGuideDemoSnippets
{
    public const string HasPage = """
        // 页面权限：角色是否勾选了该 Path 对应的菜单
        await MenuPermissionService.HasPageAsync("/admin/user");
        """;

    public const string HasButton = """
        // 按钮权限：先有页面，再校验子菜单 Path（add / edit / remove）
        await MenuPermissionService.HasButtonAsync("/admin/user", "edit");
        """;

    public const string HasApi = """
        // API：请求 /api/login@ChangePassword → 组 login，动作 ChangePassword
        await MenuPermissionService.HasApiAsync("login", "ChangePassword");
        """;

    public const string RoleMenuTree = """
        控制台                    /admin
        系统管理（目录）
          └ 用户管理（增删改查）   /admin/user
                ├ add
                ├ edit
                └ remove
        """;

    public const string LayoutGuard = """
        // LayoutAdmin：路由变化时校验
        if (!await MenuPermission.HasPageAsync(pagePath))
            NavigationManager.NavigateTo("/forbidden", replace: true);
        """;
}
