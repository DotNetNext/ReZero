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
        private void SetOrderBy(SaveInterfaceListModel saveInterfaceListModel, ZeroInterfaceList zeroInterfaceList)
        {
            if (saveInterfaceListModel.Json!.OrderBysEnableSort)
            {
                zeroInterfaceList.DataModel!.OrderDynamicParemters = new List<DataModelDynamicOrderParemter>();
            }
            if (saveInterfaceListModel.Json!.OrderBys.Any())
            {
                AddDefaultOrderBy(saveInterfaceListModel, zeroInterfaceList);
                AddMergeOrderBy(saveInterfaceListModel, zeroInterfaceList);
            }
        }
        private void AddMergeOrderBy(SaveInterfaceListModel saveInterfaceListModel, ZeroInterfaceList zeroInterfaceList)
        {

            zeroInterfaceList.DataModel!.MergeOrderByFixedParemters =
                saveInterfaceListModel.Json!.OrderBys
                .OrderBy(it => it.SortId.ObjToInt())
                .Where(it => IsMergeOrder(it))
                .Where(it => !string.IsNullOrEmpty(it.OrderByType))
                .Select(it => {
                    var left = it.Name!.Split(" AS ")[0];
                    var joinClassName = left.Split(".").First().Trim();
                    var joinPropertyName = left.Split(".").Last().Trim();
                    var asName = it.Name!.Split(" AS ")[1];
                    var joinEntity = App.Db.Queryable<ZeroEntityInfo>().Includes(it => it.ZeroEntityColumnInfos).Where(it => it.ClassName == joinClassName).First();
                    var entityColumns = joinEntity.ZeroEntityColumnInfos;
                    var columnInfo = entityColumns.FirstOrDefault(x => x.PropertyName == joinPropertyName);
                    var type = columnInfo.PropertyType;
                    var result = new DataModelOrderParemter()
                    {
                        FieldName = asName,
                        OrderByType = it.OrderByType!.EqualsCase("asc") ? OrderByType.Asc : OrderByType.Desc,
                        TableIndex = 0
                    }; 
                    return result;
                }).ToList();
        }
        private void AddDefaultOrderBy(SaveInterfaceListModel saveInterfaceListModel, ZeroInterfaceList zeroInterfaceList)
        {
            zeroInterfaceList.DataModel!.OrderByFixedParemters =
                saveInterfaceListModel.Json!.OrderBys
                .OrderBy(it => it.SortId.ObjToInt())
                .Where(it => !IsMergeOrder(it))
                .Where(it => !string.IsNullOrEmpty(it.OrderByType))
                .Select(it => new DataModelOrderParemter()
                {
                    FieldName = it.Name,
                    OrderByType = it.OrderByType!.EqualsCase("asc") ? OrderByType.Asc : OrderByType.Desc,
                    TableIndex = 0
                }).ToList();
        }

        private bool IsMergeOrder(CommonQueryOrderby it)
        {
            return it.Name!.Contains(" AS ") && it.Name.Contains(".");
        }

    }
}
