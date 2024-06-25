using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
namespace ReZero.SuperAPI 
{
    public class CommonDataService
    {
        internal void ClearAll(DataModel dataModel) 
        { 
            this.ClearZeroInterfaceListCache(dataModel);
            this.ClearZeroEntityInfoInfoCache(dataModel);
            this.ClearZeroDatabaseInfoCache(dataModel);
        }
        internal void ClearZeroInterfaceListCache(DataModel dataModel)
        {
            if (dataModel.TableId == EntityInfoInitializerProvider.Id_ZeroInterfaceList)
            {
                CacheManager<ZeroInterfaceList>.Instance.ClearCache();
            }
        }
        internal void ClearZeroDatabaseInfoCache(DataModel dataModel)
        {
            if (dataModel.TableId == EntityInfoInitializerProvider.Id_ZeroDatabaseInfo)
            {
                CacheManager<ZeroDatabaseInfo>.Instance.ClearCache();
            }
        }

        internal void ClearZeroEntityInfoInfoCache(DataModel dataModel)
        {
            if (dataModel.TableId == EntityInfoInitializerProvider.Id_ZeroEntityInfo)
            {
                CacheManager<ZeroEntityInfo>.Instance.ClearCache();
            }
        }
        internal void InitData(Type type, ISqlSugarClient db, DataModel dataModel)
        {
            var datas = dataModel.DefaultParameters.ToDictionary(it => it.Name, it => it.Value)!;
            var entityInfo = db.EntityMaintenance.GetEntityInfo(type); 
            dataModel.Data = CreateObjectByType(type, datas!);
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
        public object CreateObjectByType(Type type, Dictionary<string, object> dict)
        {
            object obj = Activator.CreateInstance(type);
            foreach (KeyValuePair<string, object> pair in dict)
            {
                PropertyInfo propertyInfo = type.GetProperty(pair.Key);
                if (propertyInfo == null)
                {
                    propertyInfo = type.GetProperties().FirstOrDefault((PropertyInfo it) => it.Name.EqualsCase(pair.Key));
                }

                if (propertyInfo != null)
                {
                    if (propertyInfo.PropertyType != typeof(string) && pair.Value?.Equals("") == true)
                    { 
                        propertyInfo.SetValue(obj, UtilMethods.GetDefaultValue(propertyInfo.PropertyType));
                    }
                    else
                    {
                        propertyInfo.SetValue(obj, UtilMethods.ChangeType2(pair.Value, propertyInfo.PropertyType));
                    }
                }
            }

            return obj;
        }
        internal void InitDb(Type type, SqlSugar.ISqlSugarClient _sqlSugarClient)
        {
            var tableName = _sqlSugarClient.EntityMaintenance.GetTableName(type);
            if (tableName.StartsWith("zero_") &&
                 (
                    _sqlSugarClient!.CurrentConnectionConfig.DbType == SqlSugar.DbType.Oracle ||
                    _sqlSugarClient.CurrentConnectionConfig.DbType == SqlSugar.DbType.Dm
                   ))
            {
                _sqlSugarClient.CurrentConnectionConfig.MoreSettings.IsAutoToUpper = true;
            }
            if (tableName.StartsWith("zero_") &&
            (
               _sqlSugarClient!.CurrentConnectionConfig.DbType == SqlSugar.DbType.PostgreSQL 
              ))
            {
                _sqlSugarClient.CurrentConnectionConfig.MoreSettings.PgSqlIsAutoToLower = true;
                _sqlSugarClient.CurrentConnectionConfig.MoreSettings.PgSqlIsAutoToLowerCodeFirst = true;
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
                        throw new Exception(TextHandler.GetCommonText(type.Name + "系统数据不能修改", type.Name + " system data cannot be updated "));
                    }
                }
            }
        }

        internal  void RemoveTypeCache(DataModel dataModel)
        {
            if (dataModel.TableId == EntityInfoInitializerProvider.Id_ZeroEntityInfo)
            {
                EntityGeneratorManager.RemoveTypeCacheByTypeId(dataModel.TableId);
            }
        }
        protected  void SetIsSnowFlakeSingle(List<EntityColumnInfo> columnInfos, Type type, DataModel dataModel, EntityColumnInfo columnInfo)
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
                urlColumnInfo.PropertyInfo.SetValue(dataModel.Data, url.Replace(PubConst.Ui_TreeUrlFormatId, value+""));
            }
        }

        protected  bool IsSnowFlakeSingle(EntityColumnInfo columnInfo)
        {
            return columnInfo.IsIdentity == false && columnInfo.UnderType == typeof(long);
        }

        private static bool IsSinglePrimaryKey(List<EntityColumnInfo> data)
        {
            return data != null && data.Count == 1;
        }
    }
}
