using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
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
            zeroInterfaceList.DataModel.DefaultParameters = new List<DataModelDefaultParameter>();
            foreach (var item in entityInfo.Columns.Where(it => it.IsIdentity == false && it.IsOnlyIgnoreInsert == false && it.IsIgnore == false))
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
        }
    }
}
