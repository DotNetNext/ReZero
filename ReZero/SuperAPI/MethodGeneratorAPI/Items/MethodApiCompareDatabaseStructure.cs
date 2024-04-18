using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReZero.SuperAPI 
{
    public partial class MethodApi
    { 
        public object CompareDatabaseStructure(List<string> ids)
        {
            List<string> tableDifferences = new List<string>();
            var result = string.Empty; 
            var dbRoot = App.Db;
            var entities=dbRoot.Queryable<ZeroEntityInfo>().In(ids.Select(it=>Convert.ToInt64(it)).ToList()).ToList(); 
            foreach (var entity in entities)
            {
                var codeFirstDb =App.GetDbTableId(entity.Id)!;
                var type = EntityGeneratorManager.GetTypeAsync(entity.Id).GetAwaiter().GetResult();
                if (codeFirstDb.DbMaintenance.IsAnyTable(codeFirstDb.EntityMaintenance.GetTableName(type),false))
                {
                    var diff = codeFirstDb.CodeFirst.SetStringDefaultLength(255).GetDifferenceTables(type).ToDiffString();
                    if (diff != null && !diff.Contains("No change"))
                    {
                        tableDifferences.Add(diff);
                    }
                }
            }
            if (tableDifferences.Count == 0)
            {
                result = $"<span class='diff_bule diff_success'>{TextHandler.GetCommonText("此操作没有风险，可以继续！！", "This operation is not risky and can continue!!")}</span>";
            }
            else
            {
                result = string.Join("", tableDifferences).Replace("\n", "<br>");
            }
            result = result.Replace("<br>----", "<h3 class='diff_h3'>");
            result = result.Replace("----", "</h3>");
            result = result.Replace("Table:", $"{TextHandler.GetCommonText("表名","Table")}:");
            result = result.Replace("Add column", $"<span class='diff_bule'>{TextHandler.GetCommonText("添加列", "Add column")}</span>");
            result = result.Replace("Update column", $"<span class='diff_yellow'>{TextHandler.GetCommonText("更新列", "Update column")}</span>");
            result = result.Replace("Delete column", $"<span class='diff_red'>{TextHandler.GetCommonText("删除列", "Delete column")}</span>");
            return result;
        }
    }
}
