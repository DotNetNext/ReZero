using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    internal partial class InterfaceListInitializerProvider
    { 
        private void AddInit_CodeList()
        {
            ZeroInterfaceList data = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = GetDbTypeList;
                it.GroupName = nameof(EnumApi);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100004;
                it.Name = TextHandler.GetInterfaceListText(GetDbTypeList);
                it.Url = GetUrl(it, "GetDbTypeList");
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroDatabaseInfo,
                    ActionType = ActionType.MyMethod,
                    MyMethodInfo = new MyMethodInfo()
                    {
                        MethodClassFullName = typeof(EnumApi).FullName,
                        MethodArgsCount = 0,
                        MethodName = nameof(EnumApi.GetDbTypeSelectDataSource)
                    }
                };
            });
            zeroInterfaceList.Add(data);
        }
    }
}
