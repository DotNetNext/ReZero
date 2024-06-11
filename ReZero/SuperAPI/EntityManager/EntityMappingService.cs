using SqlSugar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace ReZero.SuperAPI
{
    public class EntityMappingService
    {
        public Action<ZeroEntityInfo>? TableInfoConvertFunc { get; set; }

        public Action<ZeroEntityColumnInfo>? TableColumnInfoConvertFunc { get; set; }

        internal static bool IsAnyDefaultValue(DataModel dataModel)
        {
            return dataModel.DefaultValueColumns?.Any() == true;
        }
        internal static object? GetDataByDefaultValueParameters(Type type,ISqlSugarClient db, DataModel dataModel)
        {
            if (dataModel.Data == null)
                return dataModel.Data;
            var entityInfo=db.EntityMaintenance.GetEntityInfo(type);
            var now = DateTime.Now;
            if (dataModel.DefaultValueColumns.Any(it => it.Type == DefaultValueType.CurrentTime)) 
            {
                now = db.GetDate();
            }
            if (dataModel.Data is IList list)
            {
                foreach (var item in list)
                {
                    SetDatefaultValue(item, entityInfo, db, dataModel, now.AddMilliseconds(1));
                }
            }
            else 
            {
                SetDatefaultValue(dataModel.Data, entityInfo, db, dataModel, now);
            } 
            return dataModel.Data;
        }

        private static void SetDatefaultValue(object item, EntityInfo entityInfo, ISqlSugarClient db, DataModel dataModel, DateTime now)
        {
            foreach (var DefaultValue in dataModel.DefaultValueColumns??new List<DataModelDefaultValueColumnParameter>())
            {
                var columnInfo = entityInfo.Columns.FirstOrDefault(it => it.PropertyName.EqualsCase(DefaultValue.PropertyName!));
                var value = columnInfo.PropertyInfo.GetValue(item);
                var defauleValue = UtilMethods.GetDefaultValue(columnInfo.UnderType);
                if (columnInfo != null&& (value == null||value.Equals(defauleValue) || (value is string && value?.ToString()==""))) 
                {
                    try
                    {
                        switch (DefaultValue.Type!)
                        {
                            case DefaultValueType.None:
                                break;
                            case DefaultValueType.FixedValue:
                                columnInfo.PropertyInfo.SetValue(item, UtilMethods.ChangeType2(DefaultValue.Value, columnInfo.UnderType));
                                break;
                            case DefaultValueType.DefaultValue:
                                columnInfo.PropertyInfo.SetValue(item, defauleValue);
                                break;
                            case DefaultValueType.CurrentTime:
                                if (columnInfo.UnderType == typeof(DateTime))
                                {
                                    columnInfo.PropertyInfo.SetValue(item, now);
                                }
                                else
                                {
                                    throw new Exception(TextHandler.GetCommonText(PubConst.ErrorCode_001 + columnInfo.PropertyName + "默认值配置错，只能在时间类型配置：当前时间", PubConst.ErrorCode_001 + columnInfo.PropertyName + " The default value is incorrectly configured and can only be configured for the time type: current time"));
                                }
                                break;
                            case DefaultValueType.ClaimKey:
                                var claim = dataModel.ClaimList.FirstOrDefault(it => it.Key.EqualsCase(DefaultValue.Value!));
                                if (claim.Key != null)
                                {
                                    columnInfo.PropertyInfo.SetValue(item, claim.Value);
                                }
                                else
                                {
                                    throw new Exception(TextHandler.GetCommonText(PubConst.ErrorCode_001+"默认值赋值失败，没有找到 Claim key" + DefaultValue.Value, PubConst.ErrorCode_001+"Default assignment failed, claim key not found " + DefaultValue.Value));
                                }
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        if (!ex.Message.Contains(PubConst.ErrorCode_001))
                            throw new Exception(TextHandler.GetCommonText(columnInfo.PropertyName + "默认值赋值失败 " + ex.Message, columnInfo.PropertyName + "Default assignment failed " + ex.Message));
                        else
                            throw ex;
                    }

                }
            }
        }

        public ZeroEntityInfo ConvertDbToEntityInfo(Type type)
        {
            var db = App.PreStartupDb;
            var entityInfo = db!.EntityMaintenance.GetEntityInfo(type);
            ZeroEntityInfo result = new ZeroEntityInfo()
            {
                DbTableName = entityInfo.DbTableName,
                ClassName=entityInfo.EntityName,
                Description = entityInfo.TableDescription,
            };
            var columnInfos = db.DbMaintenance.GetColumnInfosByTableName(entityInfo.DbTableName,false);
            result.ZeroEntityColumnInfos = columnInfos.Select(it => {

                var propertyInfo = entityInfo.Columns.Where(it=>it.IsIgnore==false).FirstOrDefault(x => x.DbColumnName?.ToLower()==it.DbColumnName?.ToLower());
                if (propertyInfo == null) 
                {
                    return new ZeroEntityColumnInfo() { };
                }
                var data = new ZeroEntityColumnInfo()
                {
                    Description = propertyInfo.ColumnDescription??"",
                    DataType = it.DataType,
                    DbColumnName = propertyInfo.DbColumnName,
                    DecimalDigits = propertyInfo.DecimalDigits,
                    IsIdentity = propertyInfo.IsIdentity,
                    Length = propertyInfo.Length,
                    IsPrimarykey = propertyInfo.IsPrimarykey,
                    IsArray = propertyInfo.IsArray,
                    IsJson = propertyInfo.IsJson,
                    IsNullable = propertyInfo.IsNullable,
                    IsUnsigned = it.IsUnsigned??false,
                    PropertyName = propertyInfo?.PropertyName, 
                    PropertyType = EntityGeneratorManager.GetNativeTypeByType(propertyInfo!.PropertyInfo.PropertyType),
                    TableId = it.TableId,
                    IsInitialized=true
                };
                return data;
            }).ToList();
            var expColumns=entityInfo.Columns.Where(it =>it.IsIgnore==true&&it.ExtendedAttribute != null).ToList();
            foreach(var item in expColumns)
            {
                var data = new ZeroEntityColumnInfo()
                {
                    Description = item.ColumnDescription ?? "",
                    DataType = item.DataType??"",
                    DbColumnName = item.DbColumnName?? item?.PropertyName,
                    DecimalDigits = item!.DecimalDigits,
                    IsIdentity = item.IsIdentity,
                    Length = item.Length,
                    IsPrimarykey = item.IsPrimarykey,
                    IsArray = item.IsArray,
                    IsJson = item.IsJson,
                    IsNullable = item.IsNullable, 
                    PropertyName = item?.PropertyName, 
                    ExtendedAttribute = item?.ExtendedAttribute+"",
                    IsInitialized=true,
                    IsUnsigned=false 
                };
                result.ZeroEntityColumnInfos.Add(data);
            }
            result.ZeroEntityColumnInfos = result.ZeroEntityColumnInfos.Where(it => it.PropertyName != null).ToList();
            // 实现转换逻辑
            return result;
        }
         
        public DbTableInfo ConvertEntityToDbTableInfo(Type type)
        {
            var db = App.PreStartupDb;
            var entityInfo = db!.EntityMaintenance.GetEntityInfo(type);
            DbTableInfo result = new DbTableInfo()
            {
                Name = entityInfo.DbTableName,
                Description = entityInfo.TableDescription,
            };
            var columnInfos = db.DbMaintenance.GetColumnInfosByTableName(entityInfo.DbTableName);
            result.ColumnInfos = columnInfos.Select(it => new DbColumnInfo()
            {
                ColumnDescription = it.ColumnDescription,
                CreateTableFieldSort = it.CreateTableFieldSort,
                DataType = it.DataType,
                DbColumnName = it.DbColumnName,
                DecimalDigits = it.DecimalDigits,
                DefaultValue = it.ColumnDescription,
                InsertServerTime = it.UpdateServerTime,
                UpdateServerTime = it.UpdateServerTime,
                InsertSql = it.InsertSql,
                TableName = it.TableName,
                IsIdentity = it.IsIdentity,
                Length = it.Length,
                IsPrimarykey = it.IsPrimarykey,
                IsArray = it.IsArray,
                IsJson = it.IsJson,
                IsNullable = it.IsJson,
                Scale = it.Scale,
                Value = it.Value,
                IsUnsigned = it.IsUnsigned,
                UpdateSql = it.UpdateSql,
                OracleDataType = it.DataType,
                PropertyName = it.PropertyName,
                PropertyType = it.PropertyType,
                SqlParameterDbType = it.SqlParameterDbType,
                TableId = it.TableId
            }).ToList();
            // 实现转换逻辑
            return result;
        }
    }
}
