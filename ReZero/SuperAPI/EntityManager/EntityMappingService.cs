using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReZero.SuperAPI
{
    public class EntityMappingService
    {
        public Action<ZeroEntityInfo>? TableInfoConvertFunc { get; set; }

        public Action<ZeroEntityColumnInfo>? TableColumnInfoConvertFunc { get; set; }

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
