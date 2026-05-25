# NeoAdmin.Templates

NeoAdmin 的 `dotnet new` 项目模板，结构与 [NovaAdmin.Templates](https://github.com/3bDjrvHs50kiZIJb5/NovaAdmin.Blazor) 类似：打包后可通过 `dotnet new neoadmin` 生成宿主 Web 项目。

生成的项目引用 **NeoAdmin.Blazor** NuGet 包，并包含博客 CRUD、REST API、定时任务等扩展示例。

## 安装模板（本地）

```bash
# 1. 打包核心库（模板依赖此 NuGet 包）
cd /path/to/NeoAdmin
dotnet pack NeoAdmin.Blazor/NeoAdmin.Blazor.csproj -c Release -o ./nupkg

# 2. 安装模板
dotnet new install ./NeoAdmin.Templates
```

新建项目时若尚未发布到 nuget.org，需指定本地源：

```bash
mkdir MyAdmin && cd MyAdmin
dotnet new neoadmin -n MyAdminApp --nuget-source /path/to/NeoAdmin/nupkg
```

## 创建项目

```bash
mkdir MyCompany.Admin && cd MyCompany.Admin
dotnet new neoadmin -n MyCompany.Admin -o .
dotnet watch run
```

或在空目录外一层指定输出文件夹（会生成 `项目名/项目名.csproj`）：

```bash
dotnet new neoadmin -n MyCompany.Admin -o MyCompany.Admin
cd MyCompany.Admin
dotnet watch run
```

| 项 | 说明 |
|----|------|
| 短名 | `neoadmin` |
| 源名替换 | `NeoAdminApp` → 你的项目名 |
| 默认库文件 | `neoadmin.db` |
| 默认账号 | `admin` / `admin`（见 `appsettings.json`） |

## 打包发布模板

```bash
dotnet pack NeoAdmin.Templates/NeoAdmin.Templates.csproj -c Release -o ./nupkg
# 将 nupkg 中的 NeoAdmin.Templates.*.nupkg 推送到 NuGet 或私有源
```

发布 **NeoAdmin.Blazor** 与 **NeoAdmin.Templates** 时，请保持 `NeoAdminApp.csproj` 中的 `NeoAdmin.Blazor` 版本号与包版本一致。

## 目录结构

```
NeoAdmin.Templates/
├── NeoAdmin.Templates.csproj    # Template 包工程
├── README.md
└── content/
    ├── .template.config/
    │   └── template.json
    └── NeoAdminApp/             # 生成后的宿主项目骨架
        ├── Program.cs
        ├── Components/          # 控制台、博客示例页
        ├── Entities/ / SeedData / Api / Jobs
        └── docker-compose.yaml
```
