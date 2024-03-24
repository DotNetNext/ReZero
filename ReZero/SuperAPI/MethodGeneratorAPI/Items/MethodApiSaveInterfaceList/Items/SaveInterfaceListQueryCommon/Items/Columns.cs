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
                AddDefaultColumns(zeroInterfaceList, columns);
            }
            else
            {
                AddMasterColumns(saveInterfaceListModel, zeroInterfaceList, anyColumns, columns);
                AddJoinColumns(saveInterfaceListModel,zeroInterfaceList, anyJoin);
            }
        }

        private static void AddJoinColumns(SaveInterfaceListModel saveInterfaceListModel, ZeroInterfaceList zeroInterfaceList, bool anyJoin)
        {
            if (anyJoin)
            {
              //  var joinColumns = saveInterfaceListModel!.Json!.ComplexityColumns;
                //var joinTable = App.Db
                //    .Queryable<ZeroEntityInfo>()
                //    .Includes(it=>it.ZeroEntityColumnInfos)
                //    .Where(it => joinColumns.Any(it=>it.n)).First();
                //foreach (var item in joinColumns!)
                //{
                //    item.DbColumnName = item.DbColumnName ?? item.PropertyName;
                //}
            }
        }
        private static void AddMasterColumns(SaveInterfaceListModel saveInterfaceListModel, ZeroInterfaceList zeroInterfaceList, bool anyColumns, List<ZeroEntityColumnInfo> columns)
        {
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
        private static void AddDefaultColumns(ZeroInterfaceList zeroInterfaceList, List<ZeroEntityColumnInfo> columns)
        {
            zeroInterfaceList.DataModel!.Columns = columns.Select(it => new DataColumnParameter()
            {
                Description = it.Description,
                PropertyName = it.PropertyName

            }).ToList();
        }

    }
}
