# NeoAdmin.Blazor

基于 **ASP.NET Core Blazor Server** 的后台管理框架核心库：NeoUI 组件、FreeSql CRUD、RBAC、字典/参数、定时任务、审批流、REST API 等。

## 安装

```bash
dotnet add package NeoAdmin.Blazor
```

## 快速集成

在宿主 `Program.cs` 中：

```csharp
builder.AddNeoAdminSerilog();

builder.Services.AddNeoUIPrimitives();
builder.Services.AddNeoUIComponents();
builder.Services.AddNeoAdmin(builder.Configuration);
builder.Services.AddNeoAdminApi(Assembly.GetExecutingAssembly());

// ...

app.UseNeoAdminSerilogRequestLogging();
app.UseNeoAdmin();
app.MapRazorComponents<YourApp>()
    .AddAdditionalAssemblies(typeof(NeoAdmin.Blazor.Components.LayoutAdmin).Assembly)
    .AddInteractiveServerRenderMode();
```

## 项目模板

推荐使用 [NeoAdmin.Templates](https://www.nuget.org/packages/NeoAdmin.Templates) 通过 `dotnet new neoadmin` 生成完整宿主项目。

## 核心模块目录（`Core/`）

按功能划分的平台能力，避免根目录散落多个小文件夹：

| 路径 | 命名空间 | 职责 |
|------|----------|------|
| `Core/Identity/` | `NeoAdmin.Blazor.Core.Identity` | 登录、Token、API 统一返回体 |
| `Core/Authorization/` | `NeoAdmin.Blazor.Core.Authorization` | REST API 路径权限过滤器 |
| `Core/Navigation/` | `NeoAdmin.Blazor.Core.Navigation` | 菜单树、路径解析、菜单 CRUD |
| `Core/Workflow/` | `NeoAdmin.Blazor.Core.Workflow` | 审批流规则与审批按钮定义 |
| `Core/Scheduling/` | `NeoAdmin.Blazor.Core.Scheduling` | FreeScheduler 注册与任务同步 |

## 文档与源码

- 仓库：<https://github.com/3bDjrvHs50kiZIJb5/NeoAdminProject>
- NeoUI：<https://neoui.io>
- FreeSql：<https://freesql.net>
