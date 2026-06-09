namespace NeoAdmin.Blazor.Components;

/// <summary>
/// CrudTable 编辑弹窗宽度（Tailwind sm:max-w-*，样式见 wwwroot/css/dialog-max-width.css）。
/// </summary>
public enum CrudDialogContentSize
{
    Sm,
    Md,
    Lg,
    Xl,
    X2l,
    X3l,
    X4l,
    X5l,
    X6l,
    X7l,

    /// <summary>全屏编辑弹窗（占满视口，见 wwwroot/css/dialog-scroll.css）。</summary>
    FullScreen,
}

public static class CrudDialogContentSizeExtensions
{
    /// <summary>
    /// 编辑弹窗定位：距视口顶部固定，覆盖 NeoUI 默认垂直居中（top-[50%] translate-y-[-50%]）。
    /// 样式见 wwwroot/css/dialog-scroll.css 中的 top-[8vh]。
    /// </summary>
    public const string AnchorTopClass = "top-[8vh] translate-y-0";

    /// <summary>全屏弹窗 Class，覆盖 NeoUI DialogContent 默认居中与 max-width。</summary>
    public const string FullScreenDialogClass = "crud-dialog-fullscreen";

    public static bool IsFullScreen(this CrudDialogContentSize size) =>
        size == CrudDialogContentSize.FullScreen;

    public static string ToClass(this CrudDialogContentSize size) => size switch
    {
        CrudDialogContentSize.Sm => "sm:max-w-sm",
        CrudDialogContentSize.Md => "sm:max-w-md",
        CrudDialogContentSize.Lg => "sm:max-w-lg",
        CrudDialogContentSize.Xl => "sm:max-w-xl",
        CrudDialogContentSize.X2l => "sm:max-w-2xl",
        CrudDialogContentSize.X3l => "sm:max-w-3xl",
        CrudDialogContentSize.X4l => "sm:max-w-4xl",
        CrudDialogContentSize.X5l => "sm:max-w-5xl",
        CrudDialogContentSize.X6l => "sm:max-w-6xl",
        CrudDialogContentSize.X7l => "sm:max-w-7xl",
        CrudDialogContentSize.FullScreen => FullScreenDialogClass,
        _ => "sm:max-w-2xl",
    };

    /// <summary>弹窗宽度 + 顶部锚定 Class，供 CrudTable / NeoAllocTable 等编辑对话框使用。</summary>
    public static string ToDialogClass(this CrudDialogContentSize size) =>
        size.IsFullScreen()
            ? FullScreenDialogClass
            : $"{size.ToClass()} {AnchorTopClass}";

    /// <summary>编辑区滚动容器 Class；全屏时使用 flex-1 占满剩余高度。</summary>
    public static string ToEditorBodyClass(this CrudDialogContentSize size) =>
        size.IsFullScreen()
            ? "min-h-0 min-w-0 flex-1 space-y-4 overflow-x-hidden overflow-y-auto p-2"
            : "max-h-[70vh] min-w-0 space-y-4 overflow-x-hidden overflow-y-auto p-2";

    /// <summary>全屏时 EditForm 需纵向 flex 布局以撑满弹窗。</summary>
    public static string? ToEditFormClass(this CrudDialogContentSize size) =>
        size.IsFullScreen() ? "flex min-h-0 flex-1 flex-col" : null;
}
