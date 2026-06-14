using NeoAdmin.Blazor.Entities;

namespace NeoAdmin.Blazor.Components;

/// <summary>NeoFileUpload 多文件批量上传完成结果。</summary>
public sealed class NeoFileUploadBatchResult
{
    public NeoFileUploadBatchResult(int successCount, IReadOnlyList<SysFile> files)
    {
        SuccessCount = successCount;
        Files = files;
    }

    public int SuccessCount { get; }

    public IReadOnlyList<SysFile> Files { get; }
}
