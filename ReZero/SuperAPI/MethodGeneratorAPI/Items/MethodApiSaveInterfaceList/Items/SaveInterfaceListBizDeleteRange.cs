using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SqlSugar;

namespace ReZero.SuperAPI
{
    public class SaveInterfaceListBizDeleteRange : BaseSaveInterfaceList, ISaveInterfaceList
    {
        public object SaveInterfaceList(SaveInterfaceListModel saveInterfaceListModel)
        {
            ZeroInterfaceList zeroInterfaceList = new ZeroInterfaceList();
            base.SetCommonProperties(zeroInterfaceList, saveInterfaceListModel);
            this.SetProperties(zeroInterfaceList, saveInterfaceListModel);
            return base.SaveData(zeroInterfaceList);
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
                    Name=pk.PropertyName+"Array",
                    ParameterValidate=new ParameterValidate(){ IsRequired=true },
                    Description=pk.ColumnDescription,
                    ValueType=typeof(JArray).Name,
                    Value="[]"
                }
            };
            zeroInterfaceList.DataModel.ResultType = saveInterfaceListModel?.ResultType;
        }
    }
}
