using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace ReZero.SuperAPI
{
    /// <summary>
    /// Helper
    /// </summary>
    public partial class QueryCommon : IDataService
    {
        private QueryMethodInfo Join(Type type, DataModel dataModel, QueryMethodInfo queryObject)
        {
            if (!IsAnyJoin(dataModel)) return queryObject;
            int index = 0;
            var joinInfoList = dataModel.JoinParameters ?? new List<DataModelJoinParameters>();
            foreach (var item in joinInfoList)
            {
                index++;
                var shortName = GetShortName(index);
                var JoinType = EntityGeneratorManager.GetTypeAsync(item.JoinTableId).GetAwaiter().GetResult();
                var onSql = GetJoinOnSql(type, item.OnList, shortName, joinInfoList);
                queryObject = queryObject.AddJoinInfo(JoinType, shortName, onSql, item.JoinType);
            }
            return queryObject;
        }

        private string GetJoinOnSql(Type type, List<JoinParameter>? onList, string shortName, List<DataModelJoinParameters> joinInfoList)
        {
            string onSql = string.Empty;
            List<IConditionalModel> conditionalModels = new List<IConditionalModel>();
            StringBuilder sb = new StringBuilder();
            var index = 0;
            foreach (var item in onList ?? new List<JoinParameter>())
            {
                var leftEntity = _sqlSugarClient!.EntityMaintenance.GetEntityInfo(item.LeftIndex==0?type: EntityGeneratorManager.GetTypeAsync((joinInfoList[item.LeftIndex].JoinTableId)).GetAwaiter().GetResult());
                var leftName =  _sqlBuilder!.GetTranslationColumnName("t" + item.LeftIndex ) + "." + _sqlBuilder!.GetTranslationColumnName(item.LeftPropertyName);
                var rightName = _sqlBuilder!.GetTranslationColumnName("t" + item.RightIndex ) + "." + _sqlBuilder!.GetTranslationColumnName(item.RightPropertypeName);
                switch (item.FieldOperator)
                {
                    case FieldOperatorType.Equal:
                        sb.Append($"{(index == 0 ? "" : " AND ")} {leftName}={rightName} ");
                        break;
                    case FieldOperatorType.Like:
                        break;
                    case FieldOperatorType.GreaterThan:
                        break;
                    case FieldOperatorType.GreaterThanOrEqual:
                        break;
                    case FieldOperatorType.LessThan:
                        break;
                    case FieldOperatorType.LessThanOrEqual:
                        break;
                    case FieldOperatorType.In:
                        break;
                    case FieldOperatorType.NotIn:
                        break;
                    case FieldOperatorType.LikeLeft:
                        break;
                    case FieldOperatorType.LikeRight:
                        break;
                    case FieldOperatorType.NoEqual:
                        break;
                    case FieldOperatorType.IsNullOrEmpty:
                        break;
                    case FieldOperatorType.IsNot:
                        break;
                    case FieldOperatorType.NoLike:
                        break;
                    case FieldOperatorType.EqualNull:
                        break;
                    case FieldOperatorType.InLike:
                        break;
                }
                index++;
            }
            return sb.ToString();
        }
        private static string GetShortName(int index)
        {
            return "t" + index;
        }

    }
}