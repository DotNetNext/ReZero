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
        #region Core
        private void SetColumns(SaveInterfaceListModel saveInterfaceListModel, ZeroInterfaceList zeroInterfaceList)
        {
            var anyColumns = saveInterfaceListModel!.Json!.Columns.Any();
            var anyJoin = saveInterfaceListModel!.Json!.ComplexityColumns.Any();
            var tableId = GetTableId(saveInterfaceListModel);
            var columns = GetTableColums(tableId);
            if (IsDefaultColums(anyColumns, anyJoin))
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
                var entityInfos = GetJoinEntityInfos(joinColumns, tableNames);
                var index = 0;
                foreach (var item in GetJoinComplexityColumns(joinColumns!))
                {
                    index++;
                    var tableInfo = GetJoinEntity(entityInfos, item);
                    AddJoins(zeroInterfaceList, index, item, tableInfo);
                    AddJoinSelectColumns(zeroInterfaceList, index, item, tableInfo);
                }
            }
        }

        private static void AddJoins(ZeroInterfaceList zeroInterfaceList, int index, CommonQueryComplexitycolumn item, ZeroEntityInfo tableInfo)
        {

            zeroInterfaceList.DataModel!.JoinParameters = new List<DataModelJoinParameters>();
            zeroInterfaceList!.DataModel!.JoinParameters.Add(new DataModelJoinParameters()
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
        private void AddDefaultColumns(ZeroInterfaceList zeroInterfaceList, List<ZeroEntityColumnInfo> columns)
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
        private static void AddJoinSelectColumns(ZeroInterfaceList zeroInterfaceList, int index, CommonQueryComplexitycolumn item, ZeroEntityInfo tableInfo)
        {
            var columnsInfo = tableInfo!.ZeroEntityColumnInfos!
                                    .Where(it => it.PropertyName == item.Json!.JoinInfo!.ShowField).First();
            DataModelSelectParameters addColumnItem = new DataModelSelectParameters()
            {
                Name = columnsInfo.PropertyName,
                TableIndex = index,
                AsName = string.IsNullOrEmpty(item.Json!.JoinInfo!.Name) ? columnsInfo.PropertyName : item.Json!.JoinInfo!.Name
            };
            zeroInterfaceList.DataModel!.SelectParameters!.Add(addColumnItem);
        }

        #endregion

        #region Helper 
        private static ZeroEntityInfo GetJoinEntity(List<ZeroEntityInfo> entityInfos, CommonQueryComplexitycolumn item)
        {
            return entityInfos.FirstOrDefault(it => it.DbTableName!.ToLower() == item!.Json!.JoinInfo!.JoinTable!.ToLower() ||
                                                                                 it.ClassName!.ToLower() == item!.Json!.JoinInfo!.JoinTable!.ToLower());
        } 
        private static IEnumerable<CommonQueryComplexitycolumn> GetJoinComplexityColumns(CommonQueryComplexitycolumn[] joinColumns)
        {
            return joinColumns!.Where(it => it.Json!.JoinInfo!.JoinType != ColumnJoinType.SubqueryJoin);
        }

        private static List<ZeroEntityInfo> GetJoinEntityInfos(CommonQueryComplexitycolumn[]? joinColumns, List<string> tableNames)
        {
            return App.Db.Queryable<ZeroEntityInfo>()
                                .Includes(s => s.ZeroEntityColumnInfos)
                                .Where(s =>
                                                  joinColumns.Any(it => tableNames.Contains(s.DbTableName!.ToLower())) ||
                                                  joinColumns.Any(it => tableNames.Contains(s.ClassName!.ToLower()))
                                          )
                                .ToList();
        } 
        private static bool IsDefaultColums(bool anyColumns, bool anyJoin)
        {
            return !anyJoin && !anyColumns;
        }

        private static List<ZeroEntityColumnInfo> GetTableColums(long tableId)
        {
            return App.Db.Queryable<ZeroEntityColumnInfo>().Where(it => it.TableId == tableId).ToList();
        }

        private static long GetTableId(SaveInterfaceListModel saveInterfaceListModel)
        {
            return App.Db.Queryable<ZeroEntityInfo>().Where(it => it.ClassName == saveInterfaceListModel.TableId).First().Id;
        }
        #endregion

    }
}
