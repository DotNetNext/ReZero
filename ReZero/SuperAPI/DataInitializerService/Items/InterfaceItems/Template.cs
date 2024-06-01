using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    internal partial class InterfaceListInitializerProvider
    {

        public void AddInit_Template()
        { 

            GetTemplatePageList();

            DeleteTemplate();

            AddTemplate();

            UpdateTemplate();

            GetTemplateById();

            GetTemplateTypeList();

            GetTemplateListByTypeId();
        }

        private void GetTemplateById()
        { 
            ZeroInterfaceList data6 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = GetTemplateById_Id;
                it.GroupName = nameof(ZeroTemplate);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(GetTemplateById_Id);
                it.Url = GetUrl(it, "GetTemplateById");
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroTemplate,
                    ActionType = ActionType.QueryByPrimaryKey,
                    DefaultParameters = new List<DataModelDefaultParameter>() {
                             new DataModelDefaultParameter() { Name = nameof(ZeroTemplate.Id),   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name,Value=0, Description = TextHandler.GetCommonText("主键", "Id") }
                         }
                };
            });
            zeroInterfaceList.Add(data6);
        }

        private void UpdateTemplate()
        { 
            ZeroInterfaceList data5 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.POST.ToString();
                it.Id = GetUpdateTemplateId;
                it.GroupName = nameof(ZeroTemplate);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(GetUpdateTemplateId);
                it.Url = GetUrl(it, "UpdateTemplate");
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroTemplate,
                    ActionType = ActionType.UpdateObject,
                    DefaultParameters = new List<DataModelDefaultParameter>()
                    {
                        new DataModelDefaultParameter() { Name=nameof(ZeroTemplate.Id),ValueType = typeof(long).Name },
                        new DataModelDefaultParameter() { Name=nameof(ZeroTemplate.Title) ,ParameterValidate=
                        new ParameterValidate()
                        {
                            IsRequired=true
                        } ,ValueType = typeof(string).Name },
                        new DataModelDefaultParameter() { Name=nameof(ZeroTemplate.TemplateContentStyle) ,ParameterValidate=
                        new ParameterValidate()
                        {
                            IsRequired=true
                        } ,ValueType = typeof(string).Name },
                        new DataModelDefaultParameter() { Name=nameof(ZeroTemplate.Url) ,ParameterValidate=
                        new ParameterValidate()
                        {
                            IsRequired=true
                        } ,ValueType = typeof(string).Name },
                        new DataModelDefaultParameter() { Name=nameof(ZeroTemplate.TemplateContent) ,ParameterValidate=
                        new ParameterValidate()
                        {
                            IsRequired=true
                        } ,ValueType = typeof(string).Name },
                        new DataModelDefaultParameter() { Name=nameof(ZeroTemplate.TypeId) ,ParameterValidate=
                        new ParameterValidate()
                        {
                            IsRequired=true
                        } ,ValueType = typeof(long).Name },
                    }
                };
            });
            zeroInterfaceList.Add(data5);
        }

        private void AddTemplate()
        {
            //添加动态接口分类
            ZeroInterfaceList data4 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.POST.ToString();
                it.Id = GetAddTemplateId;
                it.GroupName = nameof(ZeroTemplate);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(GetAddTemplateId);
                it.Url = GetUrl(it, "AddTemplate");
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroTemplate,
                    ActionType = ActionType.InsertObject,
                    DefaultParameters = new List<DataModelDefaultParameter>()
                    {
                        new DataModelDefaultParameter() { Name=nameof(ZeroTemplate.Id) },
                        new DataModelDefaultParameter() { Name=nameof(ZeroTemplate.Title) ,ParameterValidate=
                        new ParameterValidate()
                        {
                            IsRequired=true
                        } ,ValueType = typeof(string).Name },
                        new DataModelDefaultParameter() { Name=nameof(ZeroTemplate.TemplateContent) ,ParameterValidate=
                        new ParameterValidate()
                        {
                            IsRequired=true
                        } ,ValueType = typeof(string).Name },
                        new DataModelDefaultParameter() { Name=nameof(ZeroTemplate.Url) ,ParameterValidate=
                        new ParameterValidate()
                        {
                            IsRequired=true
                        } ,ValueType = typeof(string).Name },
                        new DataModelDefaultParameter() { Name=nameof(ZeroTemplate.TypeId) ,ParameterValidate=
                        new ParameterValidate()
                        {
                            IsRequired=true
                        } ,ValueType = typeof(long).Name },
                        new DataModelDefaultParameter() { Name=nameof(ZeroTemplate.TemplateContentStyle) ,ParameterValidate=
                        new ParameterValidate()
                        {
                            IsRequired=true
                        } ,ValueType = typeof(string).Name },
                        DataInitHelper.GetIsDynamicParameter(),
                        new DataModelDefaultParameter() {
                                                Name=nameof(ZeroEntityInfo.Creator),
                                                InsertParameter=new InsertParameter(){
                                                     IsUserName=true
                                                },
                                                 Value="" ,
                                                 ValueType = typeof(string).Name },
                        new DataModelDefaultParameter() {
                                                Name=nameof(ZeroEntityInfo.CreateTime),
                                                InsertParameter=new InsertParameter(){
                                                     IsDateTimeNow=true
                                                },
                                                 Value="" ,
                                                 ValueType = typeof(string).Name },

                    }
                };
            });
            zeroInterfaceList.Add(data4);
        }

        private void DeleteTemplate()
        {
            //动态接口分类删除
            ZeroInterfaceList data3 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = DeleteTemplateId;
                it.GroupName = nameof(ZeroTemplate);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(DeleteTemplateId);
                it.Url = GetUrl(it, "DeleteTemplate");
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroTemplate,
                    ActionType = ActionType.BizDeleteObject,
                    DefaultParameters = new List<DataModelDefaultParameter>() {
                             new DataModelDefaultParameter() { Name = nameof(ZeroTemplate.Id),   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name,Value=0, Description = TextHandler.GetCommonText("主键", "Id") },
                              new DataModelDefaultParameter() { Name = nameof(ZeroTemplate.IsDeleted),   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(bool).Name,Value="true", Description = TextHandler.GetCommonText("是否删除", "IsDeleted") }
                         }
                };
            });
            zeroInterfaceList.Add(data3);
        }

        private void GetTemplatePageList()
        { 
            ZeroInterfaceList data2 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = GetTemplatePageId;
                it.GroupName = nameof(ZeroTemplate);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(GetTemplatePageId);
                it.Url = GetUrl(it, "GetTemplatePageList");
                it.CustomResultModel = new ResultModel()
                {
                    ResultType = ResultType.Grid
                };
                it.DataModel = new DataModel()
                {
                    CommonPage = new DataModelPageParameter
                    {
                        PageSize = 20,
                        PageNumber = 1
                    },
                    Columns = new List<DataColumnParameter>()
                    {

                        new DataColumnParameter(){
                            PropertyName= nameof(ZeroTemplate.Id) ,
                            Description=TextHandler.GetCommonText("ID", "Primary key")
                        },
                        new DataColumnParameter(){
                            PropertyName= nameof(ZeroTemplate.Title) ,
                            Description=TextHandler.GetCommonText("名称", "Name")
                        },
                       new DataColumnParameter(){
                            PropertyName= nameof(ZeroTemplate.TypeId) ,
                            Description=TextHandler.GetCommonText("类型", "TypeId")
                        },
                       new DataColumnParameter(){
                            PropertyName= nameof(ZeroTemplate.TemplateContentStyle) ,
                            Description=TextHandler.GetCommonText("样式", "Style")
                        },
                        new DataColumnParameter(){
                            PropertyName= nameof(ZeroTemplate.Url) ,
                            Description=TextHandler.GetCommonText("生成路径", "Path")
                        }
                    },
                    TableId = EntityInfoInitializerProvider.Id_ZeroTemplate,
                    ActionType = ActionType.QueryCommon,
                    DefaultParameters = new List<DataModelDefaultParameter>() {
                             new DataModelDefaultParameter() { Name = nameof(ZeroTemplate.Title),   FieldOperator=FieldOperatorType.Like,  ValueType = typeof(string).Name ,ValueIsReadOnly=true, Description = TextHandler.GetCommonText("标题", "Title") },
                             new DataModelDefaultParameter() { Name = nameof(ZeroTemplate.IsDeleted),   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(bool).Name,Value="false",ValueIsReadOnly=true, Description = TextHandler.GetCommonText("IsDeleted", "IsDeleted") },
                             new DataModelDefaultParameter() { Name=nameof(DataModelPageParameter.PageNumber) ,Value=1,FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name, Description = TextHandler.GetCommonText("第几页", "Page number") },
                             new DataModelDefaultParameter() { Name=nameof(DataModelPageParameter.PageSize) ,Value=20,FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name, Description = TextHandler.GetCommonText("每页几条", "Pageize") }
                    }
                };
            });
            zeroInterfaceList.Add(data2);
        }

        private void GetTemplateListByTypeId()
        {
            ZeroInterfaceList data2 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = GetTemplateByTypeId_Id;
                it.GroupName = nameof(ZeroTemplate);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(GetTemplateByTypeId_Id);
                it.Url = GetUrl(it, "GetTemplateListByTypeId"); 
                it.DataModel = new DataModel()
                { 
                    Columns = new List<DataColumnParameter>()
                    {

                        new DataColumnParameter(){
                            PropertyName= nameof(ZeroTemplate.Id) ,
                            Description=TextHandler.GetCommonText("ID", "Primary key")
                        },
                        new DataColumnParameter(){
                            PropertyName= nameof(ZeroTemplate.Title) ,
                            Description=TextHandler.GetCommonText("名称", "Name")
                        },
                        new DataColumnParameter(){
                            PropertyName= nameof(ZeroTemplate.Url) ,
                            Description=TextHandler.GetCommonText("生成路径", "Path")
                        }
                    },
                    TableId = EntityInfoInitializerProvider.Id_ZeroTemplate,
                    ActionType = ActionType.QueryCommon,
                    DefaultParameters = new List<DataModelDefaultParameter>() {
                             new DataModelDefaultParameter() { Name = nameof(ZeroTemplate.TypeId),   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(int).Name , Description = TextHandler.GetCommonText("分类Id", "Type id") },
                             new DataModelDefaultParameter() { Name = nameof(ZeroTemplate.IsDeleted),   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(bool).Name,Value="false",ValueIsReadOnly=true, Description = TextHandler.GetCommonText("IsDeleted", "IsDeleted") },
                    }
                };
            });
            zeroInterfaceList.Add(data2);
        }

        private void GetTemplateTypeList()
        {
          
            ZeroInterfaceList data2 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = GetTemplateTypeId;
                it.GroupName = nameof(ZeroTemplateType);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(GetTemplateTypeId);
                it.Url = GetUrl(it, "TemplateTypeList");
                it.DataModel = new DataModel()
                {
                    Columns = new List<DataColumnParameter>()
                    {

                        new DataColumnParameter(){
                            PropertyName= nameof(ZeroTemplateType.Id) ,
                            Description=TextHandler.GetCommonText("ID", "Primary key")
                        },
                        new DataColumnParameter(){
                            PropertyName= nameof(ZeroTemplateType.Name) ,
                            Description=TextHandler.GetCommonText("名称", "Name")
                        }
                    },
                    TableId = EntityInfoInitializerProvider.Id_ZeroTemplateType,
                    ActionType = ActionType.QueryCommon 
                };
            });
            zeroInterfaceList.Add(data2);
        } 
    }
}
