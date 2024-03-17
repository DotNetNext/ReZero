using SqlSugar;
using SqlSugar.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReZero.SuperAPI
{
    public partial class SaveInterfaceListQueryCommon : BaseSaveInterfaceList, ISaveInterfaceList
    {
        private void SetWhere(SaveInterfaceListModel saveInterfaceListModel, ZeroInterfaceList zeroInterfaceList)
        {
            var json = saveInterfaceListModel.Json!;
            if (IsWhere(json))
            {
                foreach (var it in json.Where??new CommonQueryWhere[] { })
                {
                    zeroInterfaceList.DataModel!.DefaultParameters!.Add(new DataModelDefaultParameter()
                    {
                        Name = it.PropertyName,
                        Value = it.ValueType == WhereValueType.Value ? it.Value:null,
                        FieldOperator = Enum.Parse<FieldOperatorType>(it.WhereType),
                        DefaultValue = it.Value, 
                        Description = json.Columns.FirstOrDefault(s=>s.PropertyName==it.PropertyName)?.DbColumnName,
                        ValueIsReadOnly = it.ValueType == WhereValueType.Value ? true : false
                    });
                }
            }
        }

        private static bool IsWhere(CommonQueryConfig json)
        {
            return json.Where?.Any() == true;
        }
    }
}