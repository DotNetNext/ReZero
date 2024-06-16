using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SqlSugar;

namespace ReZero.SuperAPI
{
    public class SaveInterfaceListInsertOrUpdateRange : BaseSaveInterfaceList, ISaveInterfaceList
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
            var listType = typeof(List<>).MakeGenericType(entityInfo.Type);
            var listInstance = (IList)Activator.CreateInstance(listType);
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
            zeroInterfaceList.DataModel.ResultType = saveInterfaceListModel?.ResultType;
            zeroInterfaceList.DataModel.TableColumns = saveInterfaceListModel?.TableColumns;
            if (!string.IsNullOrEmpty(zeroInterfaceList?.DataModel?.TableColumns??null)) 
            {
                var cols = entityInfo.Columns.Where(it => it.IsPrimarykey || it.IsIdentity).Select(it=>it.PropertyName).ToList();
                cols.AddRange(zeroInterfaceList?.DataModel?.TableColumns?.Split(','));
                zeroInterfaceList!.DataModel.DefaultParameters =
                    zeroInterfaceList.DataModel.DefaultParameters.Where(it => cols.Contains(it.Name!)|| cols.Contains(it.PropertyName!)).ToList();
            }
        }
    }
}
