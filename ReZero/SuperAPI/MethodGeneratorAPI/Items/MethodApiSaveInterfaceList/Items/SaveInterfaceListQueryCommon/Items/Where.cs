using SqlSugar;
using SqlSugar.Extensions;
using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
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
                        throw new Exception(TextHandler.GetCommonText("条件没有配置列名", "Condition No column name is configured"));
                    }
                    if (IsMergeWhere(it))
                    {
                        AddMergeWhereItem(zeroInterfaceList, it, json);
                    }
                    else
                    {
                        AddDefaultWhere(zeroInterfaceList, json, it);
                    } 
                }
            }
        }

        private void AddDefaultWhere(ZeroInterfaceList zeroInterfaceList, CommonConfig json, CommonQueryWhere it)
        {
            var type = this.zeroEntityInfo!
                               .ZeroEntityColumnInfos.FirstOrDefault(x => x.PropertyName == it.PropertyName).PropertyType;
            zeroInterfaceList.DataModel!.DefaultParameters!.Add(new DataModelDefaultParameter()
            {
                Id = it.Id,
                Name = it.PropertyName,
                ValueType = EntityGeneratorManager.GetTypeByNativeTypes(type).Name,
                Value = it.ValueType == WhereValueType.Value ? it.Value : null,
                FieldOperator = Enum.Parse<FieldOperatorType>(it.WhereType),
                DefaultValue = it.ValueType == WhereValueType.Value ? it.Value : null,
                Description = json.Columns.FirstOrDefault(s => s.PropertyName == it.PropertyName)?.DbColumnName,
                ValueIsReadOnly = it.ValueType == WhereValueType.Value ? true : false
            });
            var currentParameter = zeroInterfaceList.DataModel!.DefaultParameters.Last();
            if (it.ValueType == WhereValueType.ClaimKey)
            {
                currentParameter.Value = it.Value;
                currentParameter.ValueType = PubConst.Orm_WhereValueTypeClaimKey;
                currentParameter.ValueIsReadOnly = true;
            }
        }

        private void AddMergeWhereItem(ZeroInterfaceList zeroInterfaceList, CommonQueryWhere it, CommonConfig json)
        {
            InitItem(zeroInterfaceList);

            var left= it.PropertyName!.Split(" AS ")[0];
            var joinClassName = left.Split(".").First().Trim();
            var joinPropertyName = left.Split(".").Last().Trim();
            var asName = it.PropertyName!.Split(" AS ")[1];
            var joinEntity = App.Db.Queryable<ZeroEntityInfo>().Includes(it=>it.ZeroEntityColumnInfos).Where(it => it.ClassName == joinClassName).First();
            var entityColumns= joinEntity.ZeroEntityColumnInfos;
            var type = entityColumns.FirstOrDefault(x => x.PropertyName == joinPropertyName).PropertyType;
            var item = new DataModelDefaultParameter()
            {
                Id = it.Id,
                Name = asName,
                ValueType = EntityGeneratorManager.GetTypeByNativeTypes(type).Name,
                Value = it.ValueType == WhereValueType.Value ? it.Value : null,
                FieldOperator = Enum.Parse<FieldOperatorType>(it.WhereType),
                DefaultValue = it.ValueType == WhereValueType.Value ? it.Value : null,
                Description = asName,
                ValueIsReadOnly = it.ValueType == WhereValueType.Value ? true : false,
                IsMergeWhere=true
            };
            zeroInterfaceList!.DataModel!.MergeDefaultParameters!.Add(item);

            var currentParameter = zeroInterfaceList.DataModel!.MergeDefaultParameters.Last();
            if (it.ValueType == WhereValueType.ClaimKey)
            {
                currentParameter.Value = it.Value;
                currentParameter.ValueType = PubConst.Orm_WhereValueTypeClaimKey;
                currentParameter.ValueIsReadOnly = true;
            } 
            zeroInterfaceList!.DataModel!.DefaultParameters!.Add(item);
        }

        private  void InitItem(ZeroInterfaceList zeroInterfaceList)
        {
            if (zeroInterfaceList.DataModel!.MergeDefaultParameters == null)
            {
                zeroInterfaceList.DataModel!.MergeDefaultParameters = new List<DataModelDefaultParameter>();
            }
        } 
        private  bool IsMergeWhere(CommonQueryWhere it)
        {
            return it.PropertyName!.Contains(" AS ") && it.PropertyName.Contains(".");
        }

        private static bool IsWhere(CommonConfig json)
        {
            return json.Where?.Any() == true;
        }
    }
}