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
                zeroInterfaceList.DataModel!.OrderParemters =
                    saveInterfaceListModel.Json!.OrderBys
                    .OrderBy(it => it.SortId.ObjToInt())
                    .Select(it => new DataModelOrderParemter()
                    {
                        FieldName = it.Name,
                        OrderByType = it.OrderByType!.EqualsCase("asc") ? OrderByType.Asc : OrderByType.Desc,
                        TableIndex = 0
                    }).ToList();
            }
        }
    }
}
