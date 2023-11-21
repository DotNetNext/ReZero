using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReZero
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
            var columnInfos = db.DbMaintenance.GetColumnInfosByTableName(entityInfo.DbTableName);
            result.ZeroEntityColumnInfos = columnInfos.Select(it => new  ZeroEntityColumnInfo()
            {
                Description = it.ColumnDescription,
                DataType = it.DataType,
                DbCoumnName = it.DbColumnName,
                DecimalDigits = it.DecimalDigits,
                IsIdentity = it.IsIdentity,
                Length = it.Length,
                IsPrimarykey = it.IsPrimarykey,
                IsArray = it.IsArray,
                IsJson = it.IsJson,
                IsNullable = it.IsJson, 
                IsUnsigned = it.IsUnsigned,
                PropertyName = it.PropertyName,
                PropertyType =EntityGeneratorManager.GetNativeTypeByDataType(it.DataType), 
                TableId = it.TableId
            }).ToList();
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
