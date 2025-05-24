using System;
using System.Collections.Generic;
using System.Linq;
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
           
        }

        private static bool IsPageQueryResult(ZeroInterfaceList zeroInterfaceList)
        {
            return zeroInterfaceList!.DataModel!.ResultType == SqlResultType.PageQuery;
        } 

        private  void SetDataModel(SaveInterfaceListModel saveInterfaceListModel, ZeroInterfaceList zeroInterfaceList)
        {
            zeroInterfaceList.DataModel!.DataBaseId = saveInterfaceListModel!.Json!.DataBaseId ?? 0;
            zeroInterfaceList.DataModel.ActionType = ActionType.SqlScript;
            zeroInterfaceList.DataModel.Sql = saveInterfaceListModel.Sql;
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
