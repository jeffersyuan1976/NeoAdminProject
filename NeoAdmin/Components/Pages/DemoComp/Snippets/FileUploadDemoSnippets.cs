namespace NeoAdmin.Components.Pages.DemoComp.Snippets;

internal static class FileUploadDemoSnippets
{
    public const string Image = """
        <NeoFileUpload @bind-Value="coverUrl"
                       UploadDirectory="demo/file-upload"
                       Accept="image/*"
                       HelpText="仅图片，最大 5MB；选择后自动上传。" />

        <p class="text-sm text-muted-foreground">
            当前地址：@(string.IsNullOrWhiteSpace(coverUrl) ? "未上传" : coverUrl)
        </p>
        """;

    public const string Document = """
        <NeoFileUpload @bind-Value="docUrl"
                       DisplayName="@docName"
                       UploadDirectory="demo/file-upload"
                       Accept=".pdf,.doc,.docx,.xls,.xlsx"
                       ShowLocalPreview="false"
                       HelpText="支持 PDF、Word、Excel。" />
        """;

    public const string CrudEdit = """
        <EditTemplate Context="item">
            <div class="space-y-2">
                <Label>附件</Label>
                <NeoFileUpload @bind-Value="item.AttachmentUrl"
                               DisplayName="@item.AttachmentName"
                               UploadDirectory="my-module"
                               OnUploaded="file => item.AttachmentName = file.OriginFileName" />
            </div>
        </EditTemplate>
        """;

    public const string Parameters = """
        <NeoFileUpload @bind-Value="logoUrl"
                       UploadDirectory="site"
                       Accept="image/*"
                       PreviewLabel="LOGO"
                       EmptyHint="尚未设置 LOGO。"
                       ClearButtonText="清除 LOGO"
                       PreviewImageClass="h-24 w-24 rounded-md border object-contain"
                       OnUploaded="HandleUploaded"
                       OnCleared="HandleCleared" />
        """;
}
