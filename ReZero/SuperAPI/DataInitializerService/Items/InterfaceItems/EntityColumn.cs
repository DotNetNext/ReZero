using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    internal partial class InterfaceListInitializerProvider
    {
        private void AddInit_EntityColumnInfo()
        {
            GetEntityColuminsByEntityId();
            UpdateEntityColumnInfo();
            AddEntityColumnInfo();
            DeleteEntityColumnInfo();
        }
        private void GetEntityColuminsByEntityId()
        { 
            ZeroInterfaceList data1 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = GetEntityColumnsByEntityId_Id;
                it.GroupName = nameof(ZeroEntityColumnInfo);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(GetEntityColumnsByEntityId_Id);
                it.Url = GetUrl(it, "GetEntityColuminsByEntityId");
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

        private void UpdateEntityColumnInfo()
        {
            //修改实体
            ZeroInterfaceList data5 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.POST.ToString();
                it.Id = UpdateEntityColumnInfoId;
                it.GroupName = nameof(ZeroEntityColumnInfo);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(UpdateEntityColumnInfoId);
                it.Url = GetUrl(it, "UpdateEntityColumnInfo");
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroColumnInfo,
                    ActionType = ActionType.UpdateObject,
                    DefaultParameters = new List<DataModelDefaultParameter>()
                    {
                        new DataModelDefaultParameter() { Name=nameof(ZeroEntityColumnInfo.Id),ValueType = typeof(long).Name },
                        new DataModelDefaultParameter() { Name=nameof(ZeroEntityColumnInfo.DbCoumnName) ,ParameterValidate=new ParameterValidate(){IsRequired=true} ,ValueType = typeof(string).Name },
                        new DataModelDefaultParameter() { Name=nameof(ZeroEntityColumnInfo.PropertyName) ,ParameterValidate=new ParameterValidate(){IsRequired=true} ,ValueType = typeof(string).Name },
                        new DataModelDefaultParameter() { Name=nameof(ZeroEntityColumnInfo.PropertyType), ParameterValidate= new ParameterValidate(){IsRequired=true},ValueType = typeof(NativeType).Name },
                        new DataModelDefaultParameter() { Name=nameof(ZeroEntityColumnInfo.DataType),ValueType = typeof(string).Name },
                        new DataModelDefaultParameter() { Name=nameof(ZeroEntityColumnInfo.IsNullable), ParameterValidate= new ParameterValidate(){IsRequired=true},ValueType = typeof(bool).Name },
                        new DataModelDefaultParameter() { Name=nameof(ZeroEntityColumnInfo.IsPrimarykey), ParameterValidate= new ParameterValidate(){IsRequired=true},ValueType = typeof(bool).Name },
                        new DataModelDefaultParameter() { Name=nameof(ZeroEntityColumnInfo.IsPrimarykey), ParameterValidate= new ParameterValidate(){IsRequired=true},ValueType = typeof(bool).Name },
                    }
                };
            });
            zeroInterfaceList.Add(data5);
        }

        private void AddEntityColumnInfo()
        {
            //添加实体
            ZeroInterfaceList data4 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.POST.ToString();
                it.Id = AddEntityColumnInfoId;
                it.GroupName = nameof(ZeroEntityColumnInfo);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(AddEntityColumnInfoId);
                it.Url = GetUrl(it, "AddEntityColumnInfo");
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroColumnInfo,
                    ActionType = ActionType.InsertObject,
                    DefaultParameters = new List<DataModelDefaultParameter>()
                    {
                        new DataModelDefaultParameter() { Name=nameof(ZeroEntityColumnInfo.Id),ValueType = typeof(long).Name },
                        new DataModelDefaultParameter() { Name=nameof(ZeroEntityColumnInfo.DbCoumnName) ,ParameterValidate=new ParameterValidate(){IsRequired=true} ,ValueType = typeof(string).Name },
                        new DataModelDefaultParameter() { Name=nameof(ZeroEntityColumnInfo.PropertyName) ,ParameterValidate=new ParameterValidate(){IsRequired=true} ,ValueType = typeof(string).Name },
                        new DataModelDefaultParameter() { Name=nameof(ZeroEntityColumnInfo.PropertyType), ParameterValidate= new ParameterValidate(){IsRequired=true},ValueType = typeof(NativeType).Name },
                        new DataModelDefaultParameter() { Name=nameof(ZeroEntityColumnInfo.DataType),ValueType = typeof(string).Name },
                        new DataModelDefaultParameter() { Name=nameof(ZeroEntityColumnInfo.IsNullable), ParameterValidate= new ParameterValidate(){IsRequired=true},ValueType = typeof(bool).Name },
                        new DataModelDefaultParameter() { Name=nameof(ZeroEntityColumnInfo.IsPrimarykey), ParameterValidate= new ParameterValidate(){IsRequired=true},ValueType = typeof(bool).Name },
                        new DataModelDefaultParameter() { Name=nameof(ZeroEntityColumnInfo.IsPrimarykey), ParameterValidate= new ParameterValidate(){IsRequired=true},ValueType = typeof(bool).Name },
                    }
                };
            });
            zeroInterfaceList.Add(data4);
        }

        private void DeleteEntityColumnInfo()
        {
            //实体删除
            ZeroInterfaceList data3 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = DeleteEntityColumnInfoId;
                it.GroupName = nameof(ZeroEntityColumnInfo);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(DeleteEntityColumnInfoId);
                it.Url = GetUrl(it, "DeleteEntityColumnInfo");
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroColumnInfo,
                    ActionType = ActionType.BizDeleteObject,
                    DefaultParameters = new List<DataModelDefaultParameter>() {
                             new DataModelDefaultParameter() { Name = nameof(ZeroEntityInfo.Id),   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name,Value=0, Description = TextHandler.GetCommonTexst("主键", "Id") },
                              new DataModelDefaultParameter() { Name = nameof(ZeroEntityInfo.IsDeleted),   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(bool).Name,Value="true", Description = TextHandler.GetCommonTexst("是否删除", "IsDeleted") }
                         }
                };
            });
            zeroInterfaceList.Add(data3);
        }

    }
}