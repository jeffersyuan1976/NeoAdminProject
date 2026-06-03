namespace NeoAdmin.Blazor.SeedData;

/// <summary>页面内 Tabs 子标签，供全局搜索索引（由宿主通过 <see cref="Register"/> 注入）。</summary>
public static class PageSearchTabSeedData
{
    public sealed record Entry(string PagePath, string TabValue, string TabLabel);

    private static readonly List<Entry> EntriesList = new();

    public static IReadOnlyList<Entry> Entries => EntriesList;

    public static void Register(IEnumerable<Entry> entries)
    {
        EntriesList.Clear();
        EntriesList.AddRange(entries);
    }
}
