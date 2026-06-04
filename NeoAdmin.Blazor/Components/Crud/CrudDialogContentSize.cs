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
}

public static class CrudDialogContentSizeExtensions
{
    /// <summary>
    /// 编辑弹窗定位：距视口顶部固定，覆盖 NeoUI 默认垂直居中（top-[50%] translate-y-[-50%]）。
    /// 样式见 wwwroot/css/dialog-scroll.css 中的 top-[8vh]。
    /// </summary>
    public const string AnchorTopClass = "top-[8vh] translate-y-0";

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
        _ => "sm:max-w-2xl",
    };

    /// <summary>弹窗宽度 + 顶部锚定 Class，供 CrudTable / NeoAllocTable 等编辑对话框使用。</summary>
    public static string ToDialogClass(this CrudDialogContentSize size) =>
        $"{size.ToClass()} {AnchorTopClass}";
}
