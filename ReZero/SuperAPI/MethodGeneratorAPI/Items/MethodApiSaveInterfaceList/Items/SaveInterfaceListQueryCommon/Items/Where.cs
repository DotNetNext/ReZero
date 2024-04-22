using SqlSugar;
using SqlSugar.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace ReZero.SuperAPI
{
    public partial class SaveInterfaceListQueryCommon : BaseSaveInterfaceList, ISaveInterfaceList
    {
        private void SetWhere(SaveInterfaceListModel saveInterfaceListModel, ZeroInterfaceList zeroInterfaceList)
        {
            var json = saveInterfaceListModel.Json!;
            if (IsWhere(json))
            {
                zeroInterfaceList.DataModel!.WhereRelation=json.WhereRelation;
                zeroInterfaceList.DataModel.WhereRelationTemplate = json.WhereRelationTemplate; 
                foreach (var it in json.Where??new CommonQueryWhere[] { })
                {
                    if (it.PropertyName == null) 
                    {
                        throw new Exception(TextHandler.GetCommonText("Condition No column name is configured", "条件没有配置列名"));
                    }
                    var type = this.zeroEntityInfo!
                                       .ZeroEntityColumnInfos.FirstOrDefault(x => x.PropertyName == it.PropertyName).PropertyType;
                    zeroInterfaceList.DataModel!.DefaultParameters!.Add(new DataModelDefaultParameter()
                    {
                        Id=it.Id, 
                        Name = it.PropertyName, 
                        ValueType= EntityGeneratorManager.GetTypeByNativeTypes(type).Name,
                        Value = it.ValueType == WhereValueType.Value ? it.Value:null,
                        FieldOperator = Enum.Parse<FieldOperatorType>(it.WhereType),
                        DefaultValue = it.ValueType == WhereValueType.Value ? it.Value : null,
                        Description = json.Columns.FirstOrDefault(s=>s.PropertyName==it.PropertyName)?.DbColumnName,
                        ValueIsReadOnly = it.ValueType == WhereValueType.Value ? true : false
                    });
                    var currentParameter=zeroInterfaceList.DataModel!.DefaultParameters.Last();
                    if (it.ValueType == WhereValueType.ClaimKey) 
                    {
                       currentParameter.Value = it.Value;
                       currentParameter.ValueType =PubConst.Orm_WhereValueTypeClaimKey;                      
                       currentParameter.ValueIsReadOnly = true;
                    }
                }
            }
        }

        private static bool IsWhere(CommonConfig json)
        {
            return json.Where?.Any() == true;
        }
    }
}