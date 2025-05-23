using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    public class ElementSqlScript : BaseElement, IEelementActionType
    {
        public List<ActionTypeFormElementModel> GetModels()
        {
            var result = new List<ActionTypeFormElementModel>();
            base.AddActionTypeFormElementModels(result);
            base.AddActionTypeElementModel(result, this);
            RemoveCommonItem(result);
            result.Insert(2, new ActionTypeFormElementModel()
            {
                Text = TextHandler.GetCommonText("返回类型", "Result type"),
                ElementType = ElementType.Select,
                Name = nameof(SaveInterfaceListModel.ResultType),
                Value = ((int)SqlResultType.Query).ToString(),
                IsRequired = true,
                SelectDataSource = new List<ActionTypeFormElementSelectDataSourceModel>() {
                 new ActionTypeFormElementSelectDataSourceModel(){
                    Key=((int)SqlResultType.Query).ToString(),
                    Value=TextHandler.GetCommonText("查询", "Query"),
                 },
                 new ActionTypeFormElementSelectDataSourceModel(){
                    Key=((int)SqlResultType.AffectedRows).ToString(),
                    Value=TextHandler.GetCommonText("受影响行数", "Affected rows"),
                 },
                 new ActionTypeFormElementSelectDataSourceModel(){
                    Key=((int)SqlResultType.DataSet).ToString(),
                    Value=TextHandler.GetCommonText("DataSet", "DataSet"),
                 }
                 ,
                 new ActionTypeFormElementSelectDataSourceModel(){
                    Key=((int)SqlResultType.PageQuery).ToString(),
                    Value=TextHandler.GetCommonText("分页查询", "Page query"),
                 }
                },
            });
            result.Insert(3, new ActionTypeFormElementModel()
            {
                Text = TextHandler.GetCommonText("Sql脚本", "Sql script"),
                ElementType = ElementType.SqlText,
                Name = nameof(SaveInterfaceListModel.Sql),
                Value = "\r\nselect * from tableName  where  id={int:id} and name={string:name} "
            });
            return result;
        }

        private static void RemoveCommonItem(List<ActionTypeFormElementModel> result)
        {
            result.RemoveAll(it => it.Name == "TableId");
        }
    }
}
