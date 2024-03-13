using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using Newtonsoft.Json;
using static Npgsql.Replication.PgOutput.Messages.RelationMessage;

namespace ReZero.SuperAPI
{
    public class SaveInterfaceListQueryCommon : BaseSaveInterfaceList, ISaveInterfaceList
    {
        public object SaveInterfaceList(SaveInterfaceListModel saveInterfaceListModel)
        {
            ZeroInterfaceList zeroInterfaceList = new ZeroInterfaceList();
            base.SetCommonProperties(zeroInterfaceList, saveInterfaceListModel);
            SetChildObject(zeroInterfaceList);
            SetPage(saveInterfaceListModel, zeroInterfaceList);
            SetColumns(saveInterfaceListModel, zeroInterfaceList);
            SetOrderBy(saveInterfaceListModel, zeroInterfaceList);
            SetWhere(saveInterfaceListModel, zeroInterfaceList);
            return base.InsertData(zeroInterfaceList);
        }

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

        private void SetWhere(SaveInterfaceListModel saveInterfaceListModel, ZeroInterfaceList zeroInterfaceList)
        {

        }

        private void SetOrderBy(SaveInterfaceListModel saveInterfaceListModel, ZeroInterfaceList zeroInterfaceList)
        {
            if (saveInterfaceListModel.Json!.OrderBysEnableSort)
            {
                zeroInterfaceList.DataModel!.OrderDynamicParemters = new List<DataModelDynamicOrderParemter>();
            }
            if (saveInterfaceListModel.Json!.OrderBys.Any())
            {
                zeroInterfaceList.DataModel!.OrderDynamicParemters = new List<DataModelDynamicOrderParemter>();
            }
        }

        private static void SetChildObject(ZeroInterfaceList zeroInterfaceList)
        {
            zeroInterfaceList.DataModel!.DefaultParameters = zeroInterfaceList.DataModel.DefaultParameters ?? new List<DataModelDefaultParameter>();
        }

        private static void SetPage(SaveInterfaceListModel saveInterfaceListModel, ZeroInterfaceList zeroInterfaceList)
        {
            if (saveInterfaceListModel.PageSize)
            {
                zeroInterfaceList!.DataModel!.CommonPage = new DataModelPageParameter
                {
                    PageSize = 20,
                    PageNumber = 1
                };
                zeroInterfaceList.DataModel.DefaultParameters!.AddRange(
                  new List<DataModelDefaultParameter>() {
                             new DataModelDefaultParameter() { Name=nameof(DataModelPageParameter.PageNumber) ,Value=1,FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name, Description = TextHandler.GetCommonText("第几页", "Page number") },
                             new DataModelDefaultParameter() { Name=nameof(DataModelPageParameter.PageSize) ,Value=20,FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name, Description = TextHandler.GetCommonText("每页几条", "Pageize") }
                    }
                  );
                zeroInterfaceList.CustomResultModel = new ResultModel()
                {
                    ResultType = ResultType.Grid
                };
            }
        }
    }
}
