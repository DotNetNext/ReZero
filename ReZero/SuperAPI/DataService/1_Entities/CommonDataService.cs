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

        internal static void CheckSystemData(ISqlSugarClient db,DataModel dataModel, Type type, SqlSugar.EntityInfo entity)
        {
            var IsInitializedColumn = entity.Columns.FirstOrDefault(it => it.PropertyName.EqualsCase(nameof(DbBase.IsInitialized)));
            var pkColumns = entity.Columns.Where(it => it.IsPrimarykey).ToList();
            if (IsInitializedColumn != null && pkColumns.Count==1)
            {
                var pkValue=pkColumns.First().PropertyInfo.GetValue(dataModel.Data);
                if (pkValue != null)
                {
                    var IsInitializedColumnValue = db.QueryableByObject(type)
                                                      .Where(new List<IConditionalModel>() { 
                                                        new ConditionalModel()
                                                        {
                                                            FieldName=pkColumns.First().DbColumnName,
                                                            ConditionalType=ConditionalType.Equal,
                                                            FieldValue=pkValue+"",
                                                            CSharpTypeName=pkColumns.First().UnderType.Name
                                                        }
                                                      }).Select(new List<SelectModel>() {
                       new SelectModel(){ FieldName=IsInitializedColumn.DbColumnName, AsName=IsInitializedColumn.DbColumnName }
                    }).First();
                    IsInitializedColumnValue = IsInitializedColumn.PropertyInfo.GetValue(IsInitializedColumnValue);
                    if (Convert.ToBoolean(IsInitializedColumnValue))
                    {
                        throw new Exception(TextHandler.GetCommonTexst(type.Name + "系统数据不能修改", type.Name + " system data cannot be updated "));
                    }
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
