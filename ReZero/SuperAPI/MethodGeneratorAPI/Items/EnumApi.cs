using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReZero.SuperAPI 
{
    public partial class MethodApi
    {
        public List<EnumItemInfo> GetDbTypeSelectDataSource()
        {
            List<EnumItemInfo> enumItemInfos = new List<EnumItemInfo>();
            var dts = UtilMethods.EnumToDictionary<DbType>();
            foreach (var item in dts)
            {
                enumItemInfos.Add(new EnumItemInfo() { Name = item.Key, Value = Convert.ToInt32(item.Value) + "" });
            }
            return enumItemInfos.Take(7).ToList();
        }
        public List<EnumItemInfo> GetNativeTypeSelectDataSource()
        {
            List<EnumItemInfo> enumItemInfos = new List<EnumItemInfo>();
            var dts = UtilMethods.EnumToDictionary<NativeType>();
            foreach (var item in dts)
            {
                enumItemInfos.Add(new EnumItemInfo() { Name = item.Key, Value = Convert.ToInt32(item.Value) + "" });
            }
            return enumItemInfos.ToList();
        } 
        public static object GetWhereTypeList()
        {
            return SqlSugar.UtilMethods.EnumToDictionary<FieldOperatorType>()
                .Select(it => new { Key = it.Value.ToString(), Value = it.Value }).ToList();
        }
    }
}
