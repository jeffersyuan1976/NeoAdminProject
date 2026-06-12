using FreeScheduler;
using FreeSql;

namespace NeoAdmin.Blazor.Core.Scheduling;

internal static class FreeSqlSchedulerSetup
{
    public static void ConfigureEntities(IFreeSql freeSql)
    {
        freeSql.CodeFirst.ConfigEntity<TaskInfo>(entity =>
        {
            entity.Name("SysTask");
            entity.Property(a => a.Id).StringLength(30);
            entity.Property(a => a.Topic).StringLength(100);
            entity.Property(a => a.Body).StringLength(-1);
            // 与 FreeScheduler FreeSqlHandler 一致：枚举持久化为字符串（Running/Paused/Completed）
            entity.Property(a => a.Status).MapType(typeof(string));
            entity.Property(a => a.Interval).MapType(typeof(string));
            entity.Property(a => a.IntervalArgument).StringLength(50);
        });
        freeSql.CodeFirst.ConfigEntity<TaskLog>(entity =>
        {
            entity.Name("SysTaskLog");
            entity.Property(a => a.TaskId).StringLength(30);
            entity.Property(a => a.Exception).StringLength(-1);
            entity.Property(a => a.Remark).StringLength(-1);
        });
    }

    public static void SyncStructure(IFreeSql freeSql)
    {
        freeSql.CodeFirst.SyncStructure<TaskInfo>();
        freeSql.CodeFirst.SyncStructure<TaskLog>();
    }
}
