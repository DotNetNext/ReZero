using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    internal partial class InterfaceListInitializerProvider
    {
        private void AddInit_Other()
        {
            GetAllTables();
        }
        private void GetAllTables()
        {
            //获取数据库所有
            ZeroInterfaceList data1 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = GetTableAllId;
                it.GroupName = nameof(DbTableInfo);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100004;
                it.Name = TextHandler.GetInterfaceListText(GetTableAllId);
                it.Url = GetUrl(it, "GetAllTables");
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroDatabaseInfo,
                    ActionType = ActionType.DllGetTables,
                    DefaultParameters = new List<DataModelDefaultParameter>()
                    {
                        new DataModelDefaultParameter() { Name ="databaseId",   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name,  Description = TextHandler.GetCommonText("库ID", "DatabaseId") },
                    }
                };
            });
            zeroInterfaceList.Add(data1);
        }
    }
}
