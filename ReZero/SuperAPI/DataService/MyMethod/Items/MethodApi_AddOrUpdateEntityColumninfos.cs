using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReZero.SuperAPI
{
    public partial class MethodApi_Easy
    {

        public object AddOrUpdateEntityColumninfos(string columns)
        {
            try
            { 
                List<ZeroEntityColumnInfo> zeroEntityColumns = App.Db.Utilities.DeserializeObject<List<ZeroEntityColumnInfo>>(columns);
                var tableId = zeroEntityColumns.GroupBy(it => it.TableId).Select(it => it.Key).Single();
                var tableInfo = App.Db.Queryable<ZeroEntityInfo>().Where(it => it.Id == tableId).Single();
                this.CheckTableInfo(tableInfo);
                App.Db.Deleteable<ZeroEntityColumnInfo>().Where(it => it.TableId == tableId).ExecuteCommand();
                var newColumns = ConvetSaveColumn(zeroEntityColumns).ToArray();
                App.Db.Insertable(newColumns).ExecuteReturnSnowflakeId();
                return true;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
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
            return newColumns;
        }

        private void CheckTableInfo(ZeroEntityInfo tableInfo)
        {
            if (tableInfo == null)
            {
                throw new Exception(DefaultResult());
            }
            else if (tableInfo.IsInitialized)
            {
                throw new Exception((TextHandler.GetCommonTexst("系统表不能修改", "The system table cannot be modified"));
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
                throw new Exception(TextHandler.GetCommonTexst("属性不能为空", "PropertyName is required"));
            }
            if (!PubMethod.CheckIsPropertyName(item?.PropertyName ?? ""))
            {
                throw new Exception(TextHandler.GetCommonTexst("【" + item!.PropertyName + "】开头必须是字母并且不能有特殊字符", "[" + item!.PropertyName + "]  must start with a letter and cannot have special characters"));
            }
        }
        private string DefaultResult()
        {
            return TextHandler.GetCommonTexst("不能保存", "Cannot save");
        }
    }
}
