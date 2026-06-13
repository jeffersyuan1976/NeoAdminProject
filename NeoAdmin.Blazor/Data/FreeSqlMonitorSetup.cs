using FreeSql;
using Microsoft.Extensions.Logging;

namespace NeoAdmin.Blazor.Data;

/// <summary>
/// 开发调试：通过 FreeSql AOP 记录 SQL 语句与执行耗时。
/// </summary>
internal static class FreeSqlMonitorSetup
{
    public static void Configure(IFreeSql freeSql, ILogger logger)
    {
        freeSql.Aop.CommandAfter += (_, e) =>
        {
            string sql = e.Command.CommandText;
            if (e.Exception is not null)
            {
                logger.LogError(
                    e.Exception,
                    "SQL 执行失败，耗时 {ElapsedMs}ms，SQL={Sql}",
                    e.ElapsedMilliseconds,
                    sql);
                return;
            }

            logger.LogInformation(
                "SQL 耗时 {ElapsedMs}ms：{Sql}",
                e.ElapsedMilliseconds,
                sql);
        };
    }
}
