using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    internal partial class InterfaceListInitializerProvider
    { 
        private void AddInit_CodeList()
        {
            GetDbTypeList();
            GetNativeTypeList();
        }

        private void GetDbTypeList()
        {
            ZeroInterfaceList data = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = GetDbTypeListId;
                it.GroupName = nameof(MethodApi);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100004;
                it.Name = TextHandler.GetInterfaceListText(GetDbTypeListId);
                it.Url = GetUrl(it, "GetDbTypeList");
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroDatabaseInfo,
                    ActionType = ActionType.MethodGeneratorAPI,
                    MyMethodInfo = new MyMethodInfo()
                    {
                        MethodClassFullName = typeof(MethodApi).FullName,
                        MethodArgsCount = 0,
                        MethodName = nameof(MethodApi.GetDbTypeSelectDataSource)
                    }
                };
            });
            zeroInterfaceList.Add(data);
        }
        private void GetNativeTypeList()
        {
            ZeroInterfaceList data = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = GetNativeTypeId;
                it.GroupName = nameof(MethodApi);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100004;
                it.Name = TextHandler.GetInterfaceListText(GetNativeTypeId);
                it.Url = GetUrl(it, "GetNativeTypeList");
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroDatabaseInfo,
                    ActionType = ActionType.MethodGeneratorAPI,
                    MyMethodInfo = new MyMethodInfo()
                    {
                        MethodClassFullName = typeof(MethodApi).FullName,
                        MethodArgsCount = 0,
                        MethodName = nameof(MethodApi.GetNativeTypeSelectDataSource)
                    }
                };
            });
            zeroInterfaceList.Add(data);
        }
    }
}
