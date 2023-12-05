using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace ReZero.SuperAPI 
{
    public class CommonDataService
    {
        internal void InitData(Type type, ISqlSugarClient db, DataModel dataModel)
        {
            var datas = dataModel.WhereParameters.ToDictionary(it => it.Name, it => it.Value);
            var entityInfo = db.EntityMaintenance.GetEntityInfo(type);
            dataModel.Data = db.DynamicBuilder().CreateObjectByType(type, datas);
            var columnInfos = entityInfo.Columns.Where(it => it.IsPrimarykey).ToList();
            if (IsSinglePrimaryKey(columnInfos))
            {
                var columnInfo = columnInfos.First();
                if (IsSnowFlakeSingle(columnInfo))
                {
                    SetIsSnowFlakeSingle(dataModel, columnInfo);
                }
            }
        }

        private static void SetIsSnowFlakeSingle(DataModel dataModel, EntityColumnInfo columnInfo)
        {
            var value = Convert.ToInt64(columnInfo.PropertyInfo.GetValue(dataModel.Data));
            if (value == 0)
            {
                columnInfo.PropertyInfo.SetValue(dataModel.Data, SnowFlakeSingle.Instance.NextId());
            }
        }

        private static bool IsSnowFlakeSingle(EntityColumnInfo columnInfo)
        {
            return columnInfo.IsIdentity == false && columnInfo.UnderType == typeof(long);
        }

        private static bool IsSinglePrimaryKey(List<EntityColumnInfo> data)
        {
            return data != null && data.Count == 1;
        }
    }
}
