using System.ComponentModel;
using System.Reflection;

namespace NeoAdmin.Blazor.Extensions;

public static class EnumExtensions
{
    /// <summary>读取枚举字段的 Description，无则返回枚举名。</summary>
    /// <example>
    /// <code>
    /// SysMenuType.按钮.ToDescription(); // 有 Description 时返回中文说明，否则 "按钮"
    /// </code>
    /// </example>
    public static string ToDescription(this Enum item)
    {
        string name = item.ToString();
        FieldInfo? field = item.GetType().GetField(name);
        DescriptionAttribute? attribute = field?.GetCustomAttribute<DescriptionAttribute>(inherit: false);
        return attribute?.Description ?? name;
    }
}
