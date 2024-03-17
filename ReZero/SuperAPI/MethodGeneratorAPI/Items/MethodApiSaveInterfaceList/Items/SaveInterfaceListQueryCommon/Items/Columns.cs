using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReZero.SuperAPI 
{
    public partial class SaveInterfaceListQueryCommon : BaseSaveInterfaceList, ISaveInterfaceList
    {
        private void SetColumns(SaveInterfaceListModel saveInterfaceListModel, ZeroInterfaceList zeroInterfaceList)
        {
            var anyColumns = saveInterfaceListModel!.Json!.Columns.Any();
            var anyJoin = saveInterfaceListModel!.Json!.ComplexityColumns.Any();
            var columns = App.Db.Queryable<ZeroEntityColumnInfo>().Where(it => it.TableId == Convert.ToInt64(saveInterfaceListModel.TableId)).ToList();
            if (!anyJoin && !anyColumns)
            {
                zeroInterfaceList.DataModel!.Columns = columns.Select(it => new DataColumnParameter()
                {
                    Description = it.Description,
                    PropertyName = it.PropertyName

                }).ToList();
            }
            if (anyColumns)
            {
                zeroInterfaceList.DataModel!.Columns = columns
                .Where(it => saveInterfaceListModel!.Json!.Columns.Any(z => z.PropertyName == it.PropertyName)).Select(it => new DataColumnParameter()
                {
                    Description = it.Description,
                    PropertyName = it.PropertyName
                }).ToList();
                zeroInterfaceList.DataModel!.SelectParameters = saveInterfaceListModel!.Json!.Columns
                  .Select(it => new DataModelSelectParameters()
                  {
                      AsName = it.DbColumnName,
                      TableIndex = 0,
                      Name = it.PropertyName,
                  }).ToList();
            }
        } 

    }
}
