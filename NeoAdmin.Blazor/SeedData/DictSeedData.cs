using FreeSql;
using NeoAdmin.Blazor.Entities;

namespace NeoAdmin.Blazor.SeedData;

public static class DictSeedData
{
    public static void Ensure(IFreeSql freeSql)
    {
        if (!freeSql.Select<SysDict>().Any())
        {
            SeedInitialDicts(freeSql);
        }

        EnsureAuditOpinionDicts(freeSql);
    }

    private static void SeedInitialDicts(IFreeSql freeSql)
    {
        SysDict gender = Insert(freeSql, parentId: 0, name: "Gender", value: string.Empty,
            description: "性别字典分类", sort: 10);

        Insert(freeSql, parentId: gender.Id, name: "男", value: "M",
            description: "男性", sort: 1);
        Insert(freeSql, parentId: gender.Id, name: "女", value: "F",
            description: "女性", sort: 2);

        SysDict status = Insert(freeSql, parentId: 0, name: "Status", value: string.Empty,
            description: "状态字典分类", sort: 20);

        Insert(freeSql, parentId: status.Id, name: "启用", value: "1",
            description: "启用状态", sort: 1);
        Insert(freeSql, parentId: status.Id, name: "禁用", value: "0",
            description: "禁用状态", sort: 2);
    }

    private static void EnsureAuditOpinionDicts(IFreeSql freeSql)
    {
        EnsureDictCategory(
            freeSql,
            categoryName: "AuditOpinionPass",
            categoryDescription: "审批通过意见模板",
            categorySort: 30,
            items:
            [
                ("同意通过", "同意，资料齐全，予以通过。"),
                ("符合要求", "经审核，内容符合要求，同意。"),
                ("无异议", "已核实，无异议，批准通过。"),
                ("提交下一环节", "审核无误，同意提交下一环节。"),
                ("材料完整", "材料完整，同意。"),
                ("同意申请", "同意本次申请。"),
                ("信息准确", "信息准确无误，同意通过。"),
            ]);

        EnsureDictCategory(
            freeSql,
            categoryName: "AuditOpinionReject",
            categoryDescription: "审批未通过意见模板",
            categorySort: 31,
            items:
            [
                ("资料不全", "资料不全，请补充后重新提交。"),
                ("不符合要求", "经审核，内容不符合要求，不予通过。"),
                ("信息有误", "信息有误，请核实后重新提交。"),
                ("缺少附件", "缺少必要附件，请补齐后重新申请。"),
                ("不予通过", "不符合审批标准，不予通过。"),
                ("需修改", "请按意见修改后重新提交。"),
            ]);
    }

    private static void EnsureDictCategory(
        IFreeSql freeSql,
        string categoryName,
        string categoryDescription,
        int categorySort,
        (string Name, string Description)[] items)
    {
        SysDict? category = freeSql.Select<SysDict>()
            .Where(a => a.ParentId == 0 && a.Name == categoryName)
            .First();

        if (category is null)
        {
            category = new SysDict
            {
                ParentId = 0,
                Name = categoryName,
                Value = string.Empty,
                Description = categoryDescription,
                Sort = categorySort,
                Enabled = true
            };
            freeSql.Insert(category).ExecuteAffrows();
        }

        for (var i = 0; i < items.Length; i++)
        {
            (string name, string description) = items[i];
            bool exists = freeSql.Select<SysDict>()
                .Where(a => a.ParentId == category.Id && a.Name == name)
                .Any();
            if (exists)
            {
                continue;
            }

            Insert(freeSql, category.Id, name, value: string.Empty, description, sort: i + 1);
        }
    }

    private static SysDict Insert(IFreeSql freeSql, long parentId, string name, string value, string description, int sort)
    {
        SysDict dict = new()
        {
            ParentId = parentId,
            Name = name,
            Value = value,
            Description = description,
            Sort = sort,
            Enabled = true
        };
        freeSql.Insert(dict).ExecuteAffrows();
        return dict;
    }
}
