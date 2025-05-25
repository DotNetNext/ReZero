using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using SqlSugar;

namespace ReZero.SuperAPI
{
    public class SaveInterfaceListMethodGeneratorAPI : BaseSaveInterfaceList, ISaveInterfaceList
    {
        public object SaveInterfaceList(SaveInterfaceListModel saveInterfaceListModel)
        {
            ZeroInterfaceList zeroInterfaceList = new ZeroInterfaceList();
            this.SetCommonProperties(zeroInterfaceList, saveInterfaceListModel);
            this.SetGroupName(zeroInterfaceList);
            this.SetDataModel(saveInterfaceListModel, zeroInterfaceList);
            this.SetParameters(zeroInterfaceList);
            return base.SaveData(zeroInterfaceList);
        }

        private  void SetParameters(ZeroInterfaceList zeroInterfaceList)
        {
            var loader = new CodeAnalysisControllerLoader();
            var assemblyObj = loader.UpdateController(zeroInterfaceList);
            zeroInterfaceList.DataModel!.AssemblyName = assemblyObj.GetName().Name;
            var type = assemblyObj.GetTypes().FirstOrDefault(it => it.Name == PubConst.Common_DynamicApiEntry);
            var method = type?.GetMethods()?.FirstOrDefault(it => it.Name == PubConst.Common_DynamicApiEntry_InvokeAsync);
            ValidateDynamicApiEntry(type, method);
            var parameters = type?.GetMethod("InvokeAsync")?.GetParameters();
            var types = parameters.Select(p => p.ParameterType).ToArray(); 
            zeroInterfaceList.DataModel.MyMethodInfo = new MyMethodInfo()
            {
                MethodClassFullName = type?.FullName,
                MethodName = "InvokeAsync",
                MethodArgsCount = type?.GetMethod("InvokeAsync")?.GetParameters().Length ?? 0,
                ArgsTypes = types
            };
            AttibuteInterfaceInitializerService.InitializeDefaultParameters(type!, method!, false, zeroInterfaceList);
        }

        private static void ValidateDynamicApiEntry(Type? type, MethodInfo? memthod)
        {
            if (type == null)
            {
                throw new Exception(TextHandler.GetCommonText("缺少DynamicApiEntry类", "需要改成英文"));
            }
            else if (memthod == null)
            {
                throw new Exception(TextHandler.GetCommonText("DynamicApiEntry类中缺少InvokeAsync方法", "需要改成英文"));
            }
        }

        private static bool IsPageQueryResult(ZeroInterfaceList zeroInterfaceList)
        {
            return zeroInterfaceList!.DataModel!.ResultType == SqlResultType.PageQuery;
        } 

        private  void SetDataModel(SaveInterfaceListModel saveInterfaceListModel, ZeroInterfaceList zeroInterfaceList)
        {
            zeroInterfaceList.DataModel!.DataBaseId = saveInterfaceListModel!.Json!.DataBaseId ?? 0;
            zeroInterfaceList.DataModel.ActionType = ActionType.MethodGeneratorAPI;
            zeroInterfaceList.DataModel.Sql = saveInterfaceListModel.Sql;
            zeroInterfaceList.DataModel.CSharpText = saveInterfaceListModel.CSharpText;
            zeroInterfaceList.DataModel.ResultType = saveInterfaceListModel?.ResultType;
        }

        private  void SetGroupName(ZeroInterfaceList zeroInterfaceList)
        {
            if (string.IsNullOrEmpty(zeroInterfaceList.GroupName))
            {
                zeroInterfaceList.GroupName = "C#";
            }
        }

        protected override void SetCommonProperties(ZeroInterfaceList zeroInterfaceList, SaveInterfaceListModel saveInterfaceListModel)
        {
            // Set default values for ZeroInterfaceList
            zeroInterfaceList.IsInitialized = false;
            zeroInterfaceList.IsDeleted = false;
            zeroInterfaceList.Name = saveInterfaceListModel.Name;
            zeroInterfaceList.Url = base.GetUrl(saveInterfaceListModel).Replace("//","/");
            zeroInterfaceList.DatabaseId = saveInterfaceListModel?.Json?.DataBaseId;
            zeroInterfaceList.IsAttributeMethod = false;
            zeroInterfaceList.GroupName = !string.IsNullOrEmpty(saveInterfaceListModel?.GroupName) ? saveInterfaceListModel?.GroupName! : saveInterfaceListModel?.TableId!;
            zeroInterfaceList.InterfaceCategoryId = Convert.ToInt64(saveInterfaceListModel?.InterfaceCategoryId);

            // Set creator information
            var options = SuperAPIModule._apiOptions;
            var userInfo = options?.DatabaseOptions!.GetCurrentUserCallback();
            zeroInterfaceList.Creator = userInfo?.UserName;
            zeroInterfaceList.CreateTime = DateTime.Now;

            // Set default HttpMethod if not specified
            if (zeroInterfaceList.HttpMethod == null)
            {
                zeroInterfaceList.HttpMethod = HttpRequestMethod.All.ToString();
            }
             
            //primary key
            zeroInterfaceList.Id = saveInterfaceListModel!.Json?.Id ?? 0;

            if (zeroInterfaceList.DataModel == null) 
            {
                zeroInterfaceList.DataModel = new DataModel();
            }
            //update info
            SetCurrentData(zeroInterfaceList.DataModel!, saveInterfaceListModel);

        }
        private void SetProperties(ZeroInterfaceList zeroInterfaceList, SaveInterfaceListModel saveInterfaceListModel)
        {
            var entityInfo = base.GetEntityInfo(zeroInterfaceList!.DataModel!.TableId!);
            var pk = entityInfo.Columns.FirstOrDefault(it => it.IsPrimarykey);
            base.Check(pk);
            zeroInterfaceList.DataModel.DefaultParameters = new List<DataModelDefaultParameter>()
            {
                new DataModelDefaultParameter(){
                    FieldOperator=FieldOperatorType.Equal,
                    Name=pk.PropertyName,
                    ParameterValidate=new ParameterValidate(){ IsRequired=true },
                    Description=pk.ColumnDescription,
                    ValueType=pk.UnderType.Name
                }
            };
        }
    }
}
