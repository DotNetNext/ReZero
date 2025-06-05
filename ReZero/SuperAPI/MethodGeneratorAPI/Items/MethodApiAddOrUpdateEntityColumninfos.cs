using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReZero.SuperAPI
{
    public partial class MethodApi
    {

        public object AddOrUpdateEntityColumninfos(string columns)
        {
            try
            {
                List<ZeroEntityColumnInfo> zeroEntityColumns = App.Db.Utilities.DeserializeObject<List<ZeroEntityColumnInfo>>(columns);
                var tableId = zeroEntityColumns.GroupBy(it => it.TableId).Select(it => it.Key).Single();
                EntityGeneratorManager.RemoveTypeCacheByTypeId(tableId);
                var tableInfo = App.Db.Queryable<ZeroEntityInfo>().Where(it => it.Id == tableId).Single();
                this.CheckTableInfo(tableInfo);
                App.Db.Deleteable<ZeroEntityColumnInfo>().Where(it => it.TableId == tableId).ExecuteCommand();
                var newColumns = ConvetSaveColumn(zeroEntityColumns).ToArray();
                this.CheckColumns(newColumns);
                App.Db.Insertable(newColumns).ExecuteReturnSnowflakeId();
                tableInfo.ColumnCount = newColumns.Length;
                App.Db.Updateable(tableInfo).UpdateColumns(it => new { it.ColumnCount }).ExecuteCommand();
                CacheManager<ZeroEntityInfo>.Instance.ClearCache();
                CacheManager<ZeroEntityColumnInfo>.Instance.ClearCache();
                return true;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private  void CheckColumns(ZeroEntityColumnInfo[] newColumns)
        {
            if (IsRepeatColumn(newColumns))
            {
                throw new Exception(TextHandler.GetCommonText("列名重复", "Column name repeat"));
            }
            foreach (var item in newColumns)
            {
                if (item.IsPrimarykey && item.IsNullable) 
                {
                    throw new Exception(TextHandler.GetCommonText("主键不能为null", "Primary key cannot be null"));
                }
            }
        }

        private static bool IsRepeatColumn(ZeroEntityColumnInfo[] newColumns)
        {
            return newColumns
                   .Where(it => it.PropertyName != null)
                   .GroupBy(it => it.PropertyName?.ToLower()).Any(it => it.Count() > 1) ||
                   newColumns
                   .Where(it => it.DbColumnName != null)
                   .GroupBy(it => it.DbColumnName?.ToLower())
                   .Any(it => it.Count() > 1);
        }

        private List<ZeroEntityColumnInfo> ConvetSaveColumn(List<ZeroEntityColumnInfo> zeroEntityColumns)
        {
            var newColumns = zeroEntityColumns;
            foreach (var item in newColumns)
            {
                CheckTtem(item);
            }
            if (!newColumns.Any())
            {
                throw new Exception(DefaultResult());
            }
            if (newColumns.Any(it => it.IsIdentity && it.PropertyType == NativeType.String)) 
            {
                throw new Exception(TextHandler.GetCommonText("字符串类型不能设置自增", "String type cannot be set to auto-increment"));
            }
            if (newColumns.Any(it => it.IsIdentity)&& newColumns.Count()==1)
            {
                throw new Exception(TextHandler.GetCommonText("存在自增表里面至少2个字段", "If self-increment columns exist: Requires 2 columns"));
            }
            return newColumns;
        }

        private void CheckTableInfo(ZeroEntityInfo tableInfo)
        {
            
            if (tableInfo == null)
            {
                throw new Exception(DefaultResult());
            }
            else if (!PubMethod.CheckIsPropertyName(tableInfo.ClassName!))
            {
                throw new Exception(TextHandler.GetCommonText("【 实体名错误 " + tableInfo.ClassName! + "】开头必须是字母并且不能有特殊字符", "[ Class name" + tableInfo.ClassName! + "]  must start with a letter and cannot have special characters"));
            }
            else if (tableInfo.IsInitialized)
            {
                throw new Exception((TextHandler.GetCommonText("系统表不能修改", "The system table cannot be modified")));
            }
        }
        private void CheckTtem(ZeroEntityColumnInfo? item)
        {
            if (string.IsNullOrEmpty(item!.DbColumnName))
            {
                item.DbColumnName = item.PropertyName;
            }
            if (string.IsNullOrEmpty(item?.PropertyName ?? ""))
            {
                throw new Exception(TextHandler.GetCommonText("属性不能为空", "PropertyName is required"));
            }
            if (!PubMethod.CheckIsPropertyName(item?.PropertyName ?? ""))
            {
                throw new Exception(TextHandler.GetCommonText("【" + item!.PropertyName + "】开头必须是字母并且不能有特殊字符", "[" + item!.PropertyName + "]  must start with a letter and cannot have special characters"));
            }
        }
        private string DefaultResult()
        {
            return TextHandler.GetCommonText("不能保存", "Cannot save");
        }
    }
}
