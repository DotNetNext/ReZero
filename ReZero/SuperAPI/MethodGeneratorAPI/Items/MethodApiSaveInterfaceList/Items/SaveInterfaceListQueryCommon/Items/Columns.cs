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
        private void SetColumns(SaveInterfaceListModel saveInterfaceListModel, ZeroInterfaceList zeroInterfaceList)
        {
            var anyColumns = saveInterfaceListModel!.Json!.Columns.Any();
            var anyJoin = saveInterfaceListModel!.Json!.ComplexityColumns.Any();
            var tableId = App.Db.Queryable<ZeroEntityInfo>().Where(it => it.ClassName == saveInterfaceListModel.TableId).First().Id;
            var columns = App.Db.Queryable<ZeroEntityColumnInfo>().Where(it => it.TableId == tableId).ToList();
            if (!anyJoin && !anyColumns)
            {
                AddDefaultColumns(zeroInterfaceList, columns);
            }
            else
            {
                AddMasterColumns(saveInterfaceListModel, zeroInterfaceList, anyColumns, columns);
                AddJoinColumns(saveInterfaceListModel, zeroInterfaceList, anyJoin);
            }
        }

        private static void AddJoinColumns(SaveInterfaceListModel saveInterfaceListModel, ZeroInterfaceList zeroInterfaceList, bool anyJoin)
        {
            if (anyJoin)
            {
                var joinColumns = saveInterfaceListModel!.Json!.ComplexityColumns;
                var tableNames = joinColumns.Select(it => it.Json!.JoinInfo!.JoinTable!.ToLower()).ToList();
                var entityInfos = App.Db.Queryable<ZeroEntityInfo>()
                    .Includes(s => s.ZeroEntityColumnInfos)
                    .Where(s => 
                                      joinColumns.Any(it => tableNames.Contains(s.DbTableName!.ToLower()))||
                                      joinColumns.Any(it => tableNames.Contains(s.ClassName!.ToLower()))
                              )
                    .ToList();
                var index = 0;
                zeroInterfaceList.DataModel!.JoinParameters = new List<DataModelJoinParameters>();
                foreach (var item in joinColumns!.Where(it => it.Json!.JoinInfo!.JoinType != ColumnJoinType.SubqueryJoin))
                {
                    index++;
                    var tableInfo = entityInfos.FirstOrDefault(it => it.DbTableName!.ToLower() == item!.Json!.JoinInfo!.JoinTable!.ToLower()||
                                                                     it.ClassName!.ToLower() == item!.Json!.JoinInfo!.JoinTable!.ToLower());
                    zeroInterfaceList.DataModel.JoinParameters.Add(new DataModelJoinParameters()
                    {
                        JoinTableId = tableInfo!.Id,
                        OnList = new List<JoinParameter>()
                        {
                            new JoinParameter()
                            {
                                FieldOperator=FieldOperatorType.Equal,
                                LeftIndex=index-1,
                                LeftPropertyName=item.Json!.JoinInfo!.MasterField,
                                RightPropertyName=item.Json!.JoinInfo!.JoinField,
                                RightIndex=index
                            }
                        }
                    });
                    var columnsInfo = tableInfo!.ZeroEntityColumnInfos!
                        .Where(it=>it.PropertyName== item.Json!.JoinInfo!.ShowField).First();
                    var addColumnItem = new DataColumnParameter()
                    {
                        PropertyName = columnsInfo.PropertyName,
                        Description = columnsInfo.Description,
                        PropertyType = columnsInfo.PropertyType,
                        AsName = string.IsNullOrEmpty(item.Json!.JoinInfo!.Name) ? columnsInfo.PropertyName : item.Json!.JoinInfo!.Name
                    };
                    zeroInterfaceList.DataModel!.Columns!.Add(addColumnItem);
                }
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
        private  void AddDefaultColumns(ZeroInterfaceList zeroInterfaceList, List<ZeroEntityColumnInfo> columns)
        {
            if (this.isPage)
            {
                zeroInterfaceList.DataModel!.Columns = columns.Select(it => new DataColumnParameter()
                {
                    Description = it.PropertyName,
                    PropertyName = it.PropertyName,
                    AsName = it.PropertyName

                }).ToList();
            }
            else
            {
                zeroInterfaceList.DataModel!.Columns = columns.Select(it => new DataColumnParameter()
                {
                    Description = it.DbColumnName,
                    PropertyName = it.PropertyName,
                    AsName = it.DbColumnName

                }).ToList();
            }
        }

    }
}
