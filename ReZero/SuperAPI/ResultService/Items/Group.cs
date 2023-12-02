using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReZero.SuperAPI.Items
{
    internal class Group: IResultService
    {
        public object GetResult(object data, ResultModel result)
        {
            // 检查data是否是可枚举的集合
            if (data is IEnumerable enumerableData)
            {
                var groupName = result.GroupName;

                // 使用LINQ的GroupBy方法根据groupName进行分组
                var groupedData = GroupDataByGroupName(enumerableData, groupName??"");

                return groupedData;
            }

            // 如果data不是可枚举的集合，可以根据实际情况处理，比如抛出异常或返回原始data
            return data;
        }

        private object GroupDataByGroupName(IEnumerable data, string groupName)
        {
            // 这里假设data中的元素有一个名为GroupName的属性，你可能需要根据实际情况调整
            var groupedData = data.Cast<object>().GroupBy(item => item.GetType().GetProperty("GroupName")?.GetValue(item)?.ToString());

            // 获取指定分组的数据
            var resultGroup = groupedData.FirstOrDefault(group => group.Key == groupName);

            return resultGroup?.ToList();
        }
    } 
}
