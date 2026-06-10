namespace NeoAdminApp.Components.Pages.DemoUI;

internal static class DataDisplayDemoSnippets
{
    public const string DataTable = """
        <DataTable TData="DemoProduct" Data="@products" ShowToolbar="true" InitialPageSize="5">
            <Columns>
                <DataTableColumn TData="DemoProduct" TValue="string"
                                 Property="@(p => p.Name)" Header="名称" Sortable="true" />
                <DataTableColumn TData="DemoProduct" TValue="string"
                                 Property="@(p => p.Category)" Header="分类" Sortable="true" />
                <DataTableColumn TData="DemoProduct" TValue="decimal"
                                 Property="@(p => p.Price)" Header="价格" Sortable="true" />
            </Columns>
        </DataTable>
        """;

    public const string DataGrid = """
        <DataGrid TItem="DemoProduct" Items="@products" Height="300px" PageSize="10" FillWidth="true">
            <Columns>
                <DataGridColumn TItem="DemoProduct" Field="Name" Header="名称" Sortable="true" Flex="1" />
                <DataGridColumn TItem="DemoProduct" Field="Category" Header="分类" Sortable="true" />
                <DataGridColumn TItem="DemoProduct" Field="Price" Header="价格" Sortable="true" Width="100px" />
            </Columns>
        </DataGrid>
        """;

    public const string DataView = """
        <DataView TItem="DemoProduct" Items="@products" PageSize="6" Layout="@dataViewLayout">
            <Fields>
                <DataViewColumn TItem="DemoProduct" Header="名称"
                                Property="@(p => p.Name)" Sortable="true" Filterable="true" />
            </Fields>
            <ListTemplate Context="product">
                <div class="rounded-lg border p-3">@product.Name — @product.Price.ToString("C0")</div>
            </ListTemplate>
            <GridTemplate Context="product">
                <Card><CardContent Class="p-3 text-sm">@product.Name</CardContent></Card>
            </GridTemplate>
        </DataView>
        """;

    public const string TreeView = """
        <TreeView TItem="DemoTreeNode"
                  Items="@treeNodes"
                  ValueField="n => n.Id"
                  TextField="n => n.Name"
                  ChildrenProperty="n => n.Children"
                  SelectionMode="TreeSelectionMode.Single"
                  @bind-SelectedValue="selectedTreeId"
                  DefaultExpandDepth="2"
                  ShowLines="true" />
        """;

    public const string Timeline = """
        <Timeline Align="TimelineAlign.Alternate">
            <TimelineItem Status="TimelineStatus.Completed">
                <IconContent>
                    <TimelineIcon Color="TimelineColor.Primary">
                        <LucideIcon Name="flag" Size="16" />
                    </TimelineIcon>
                </IconContent>
                <ChildContent>
                    <TimelineContent>
                        <TimelineHeader>
                            <TimelineTitle>项目启动</TimelineTitle>
                            <TimelineTime>2025-01-10</TimelineTime>
                        </TimelineHeader>
                        <TimelineDescription>完成立项与资源分配</TimelineDescription>
                    </TimelineContent>
                </ChildContent>
            </TimelineItem>
            <TimelineItem Status="TimelineStatus.InProgress">
                <IconContent>
                    <TimelineIcon Color="TimelineColor.Accent">
                        <LucideIcon Name="code" Size="16" />
                    </TimelineIcon>
                </IconContent>
                <ChildContent>
                    <TimelineContent>
                        <TimelineHeader>
                            <TimelineTitle>核心开发</TimelineTitle>
                            <TimelineTime>2025-02-15</TimelineTime>
                        </TimelineHeader>
                        <TimelineDescription>表单与权限模块开发中</TimelineDescription>
                    </TimelineContent>
                </ChildContent>
            </TimelineItem>
            <TimelineItem Status="TimelineStatus.Pending" ShowConnector="false">
                <IconContent>
                    <TimelineIcon Color="TimelineColor.Muted">
                        <LucideIcon Name="package-check" Size="16" />
                    </TimelineIcon>
                </IconContent>
                <ChildContent>
                    <TimelineContent>
                        <TimelineHeader>
                            <TimelineTitle>正式上线</TimelineTitle>
                            <TimelineTime>2025-04-01</TimelineTime>
                        </TimelineHeader>
                        <TimelineDescription>生产环境发布</TimelineDescription>
                    </TimelineContent>
                </ChildContent>
            </TimelineItem>
        </Timeline>
        """;
}
