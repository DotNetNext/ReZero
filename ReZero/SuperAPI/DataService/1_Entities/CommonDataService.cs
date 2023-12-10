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
            var datas = dataModel.DefaultParameters.ToDictionary(it => it.Name, it => it.Value);
            var entityInfo = db.EntityMaintenance.GetEntityInfo(type);
            dataModel.Data = db.DynamicBuilder().CreateObjectByType(type, datas);
            var columnInfos = entityInfo.Columns.Where(it => it.IsPrimarykey).ToList();
            if (IsSinglePrimaryKey(columnInfos))
            {
                var columnInfo = columnInfos.First();
                if (IsSnowFlakeSingle(columnInfo))
                {
                    SetIsSnowFlakeSingle(entityInfo.Columns, type,dataModel, columnInfo);
                }
            }
        }

        private static void SetIsSnowFlakeSingle(List<EntityColumnInfo> columnInfos, Type type, DataModel dataModel, EntityColumnInfo columnInfo)
        {
            var value = Convert.ToInt64(columnInfo.PropertyInfo.GetValue(dataModel.Data));
            if (value == 0)
            {
                value = SnowFlakeSingle.Instance.NextId();
                columnInfo.PropertyInfo.SetValue(dataModel.Data,value);
            }
            if (type.Name == nameof(ZeroInterfaceCategory)) 
            {
                var urlColumnInfo = columnInfos.First(it => it.PropertyName == nameof(ZeroInterfaceCategory.Url));
                var url = urlColumnInfo.PropertyInfo.GetValue(dataModel.Data)+"";
                urlColumnInfo.PropertyInfo.SetValue(dataModel.Data, url.Replace(PubConst.TreeUrlFormatId, value+""));
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
