using FreeScheduler;
using FreeSql;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NeoAdmin.Blazor.Attributes;
using NeoAdmin.Blazor.Entities;

namespace NeoAdmin.Jobs;

/// <summary>
/// IP 白名单定时任务。
/// </summary>
public static class IpWhitelistJobs
{
    /// <summary>
    /// 每 5 分钟清空 IP 白名单表。
    /// </summary>
    [Scheduler("ip-whitelist.clear", "0 */5 * * * *")]
    public static async Task ClearIpWhitelist(IServiceProvider serviceProvider, TaskInfo task)
    {
        IFreeSql freeSql = serviceProvider.GetRequiredService<IFreeSql>();
        ILogger logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(typeof(IpWhitelistJobs));

        logger.LogInformation("ClearIpWhitelist 开始，TaskId={TaskId}", task.Id);

        int deleted = await freeSql.Delete<SysIpWhitelist>().ExecuteAffrowsAsync();

        logger.LogInformation("ClearIpWhitelist 完成，删除行数={Deleted}", deleted);
    }
}
