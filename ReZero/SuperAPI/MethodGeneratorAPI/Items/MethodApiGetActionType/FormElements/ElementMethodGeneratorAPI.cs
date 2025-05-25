using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    public class ElementMethodGeneratorAPI : BaseElement, IEelementActionType
    {
        public List<ActionTypeFormElementModel> GetModels()
        {
            var result = new List<ActionTypeFormElementModel>();
            base.AddActionTypeFormElementModels(result);
            base.AddActionTypeElementModel(result, this);
            RemoveCommonItem(result);
            result.Insert(3, new ActionTypeFormElementModel()
            {
                Text = TextHandler.GetCommonText("C#文本", "C# code text"),
                ElementType = ElementType.CSharpText,
                Name = "CSharpText",
                Value = @"using SqlSugar;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReZero.DependencyInjection;
using Microsoft.AspNetCore.Http;

/// <summary>
/// 动态 API 入口类，所有动态 API 请求均由此类处理。
/// </summary>
public class DynamicApiEntry
{
    /// <summary>
    /// 动态 API 的统一入口方法。
    /// </summary>
    /// <param name=""name"">参数可以修改，可以多个简单类型，或者实体 </param>
    /// <returns>动态执行结果</returns>
    public async Task<object> InvokeAsync(string name)
    {
        //可以用Rezero中的ioc获取注入对象
        var httpContext = DependencyResolver.GetService<IHttpContextAccessor>();
   
        //业务可以修改
        var result= CheckPermission(name); 
        //业务可以修改

        return await Task.FromResult<object>(result);
    }

    // 可以写辅助方法，也可以写类
    private bool CheckPermission(string apiName)
    {
        return true;
    }
}"
            });
            return result; 
        }
        private static void RemoveCommonItem(List<ActionTypeFormElementModel> result)
        {
            result.RemoveAll(it => it.Name == "TableId");
        }
    }
}
