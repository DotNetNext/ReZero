using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ReZero.SuperAPI
{
    public class SaveInterfaceListUpdateObject : BaseSaveInterfaceList, ISaveInterfaceList
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
            zeroInterfaceList.DataModel.DefaultParameters = new List<DataModelDefaultParameter>();
            foreach (var item in entityInfo.Columns.Where(it =>it.IsOnlyIgnoreUpdate == false&&it.IsIgnore==false))
            {
                zeroInterfaceList.DataModel.DefaultParameters.Add(new DataModelDefaultParameter()
                {
                    FieldOperator = FieldOperatorType.Equal,
                    Name = item.PropertyName,
                    ParameterValidate = item.IsNullable ? null : new ParameterValidate() { IsRequired = true },
                    Description = item.ColumnDescription,
                    ValueType = item.UnderType.Name
                }); 
            }
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
