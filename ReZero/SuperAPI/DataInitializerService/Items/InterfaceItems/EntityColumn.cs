using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    internal partial class InterfaceListInitializerProvider
    {
        private void AddInit_EntityColumnInfo()
        {
            GetEntityPropertiesByEntityId();
        }
        private void GetEntityPropertiesByEntityId()
        { 
            ZeroInterfaceList data1 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = GetEntityPropertiesByEntityId_Id;
                it.GroupName = nameof(ZeroEntityColumnInfo);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(GetEntityInfoById_Id);
                it.Url = GetUrl(it, "GetEntityPropertiesByEntityId");
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroColumnInfo,
                    ActionType = ActionType.QueryCommon,
                    DefaultParameters = new List<DataModelDefaultParameter>() {
                             new DataModelDefaultParameter() { Name = nameof(ZeroEntityColumnInfo.TableId),   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name,Value=0, Description = TextHandler.GetCommonTexst("实体Id", "Entity id") }
                    }
                };
            });
            zeroInterfaceList.Add(data1);
        }

    }
}