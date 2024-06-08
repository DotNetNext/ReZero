using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Collections;
using SqlSugar;
namespace ReZero.SuperAPI 
{
    public class SaveInterfaceListInsertRange : BaseSaveInterfaceList, ISaveInterfaceList
    {
        public object SaveInterfaceList(SaveInterfaceListModel saveInterfaceListModel)
        {
            ZeroInterfaceList zeroInterfaceList = new ZeroInterfaceList();
            base.SetCommonProperties(zeroInterfaceList, saveInterfaceListModel);
            this.SetProperties(zeroInterfaceList, saveInterfaceListModel);
            base.ApplyDefaultAndClearIfNotEmpty(zeroInterfaceList);
            return base.SaveData(zeroInterfaceList);
        } 
        private void SetProperties(ZeroInterfaceList zeroInterfaceList, SaveInterfaceListModel saveInterfaceListModel)
        {
            var entityInfo = base.GetEntityInfo(zeroInterfaceList!.DataModel!.TableId!);
            var listType= typeof(List<>).MakeGenericType(entityInfo.Type);
            var listInstance = (IList)Activator.CreateInstance(listType) ;
            listInstance.Add(Activator.CreateInstance(entityInfo.Type));
            var json = new SerializeService().SerializeObject(listInstance);
            zeroInterfaceList.DataModel.DefaultParameters = 
                new List<DataModelDefaultParameter>() {
                   new DataModelDefaultParameter() 
                   {
                        Value=json,
                        Name="Data",
                        ValueType=typeof(JArray).Name, 
                        Description=""
                   }
                };
            zeroInterfaceList.DataModel.DefaultValueColumns = saveInterfaceListModel.Json?.DefaultValueColumns;
        }
    }
}
