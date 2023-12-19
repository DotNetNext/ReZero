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
                AppendJoinItem(type, joinInfoList, sb, index, item);
                index++;
            }
            return sb.ToString();
        }

        private void AppendJoinItem(Type type, List<DataModelJoinParameters> joinInfoList, StringBuilder sb, int index, JoinParameter item)
        {
            var leftEntity = _sqlSugarClient!.EntityMaintenance.GetEntityInfo(GetLeftType(type, joinInfoList, item));
            var rightEntity = _sqlSugarClient!.EntityMaintenance.GetEntityInfo(GetRightType(type, joinInfoList, item));
            var leftName = GetLeftName(item,leftEntity);
            var rightName = GetRightName(item,rightEntity);
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
        }

        private static Type GetLeftType(Type type, List<DataModelJoinParameters> joinInfoList, JoinParameter item)
        {
            return item.LeftIndex == 0 ? type : EntityGeneratorManager.GetTypeAsync((joinInfoList[item.LeftIndex].JoinTableId)).GetAwaiter().GetResult();
        }
        private static Type GetRightType(Type type, List<DataModelJoinParameters> joinInfoList, JoinParameter item)
        {
            return item.RightIndex == 0 ? type : EntityGeneratorManager.GetTypeAsync((joinInfoList[item.RightIndex].JoinTableId)).GetAwaiter().GetResult();
        }
        private string GetRightName(JoinParameter item, EntityInfo rightEntity)
        {
            var name=rightEntity.Columns.FirstOrDefault(it => it.PropertyName == item.RightPropertypeName).DbColumnName;
            return _sqlBuilder!.GetTranslationColumnName(PubConst.TableDefaultPreName + item.RightIndex) + "." + _sqlBuilder!.GetTranslationColumnName(name);
        }

        private string GetLeftName(JoinParameter item, EntityInfo leftEntity)
        {
            var name = leftEntity.Columns.FirstOrDefault(it => it.PropertyName == item.RightPropertypeName).DbColumnName;
            return _sqlBuilder!.GetTranslationColumnName(PubConst.TableDefaultPreName + item.LeftIndex) + "." + _sqlBuilder!.GetTranslationColumnName(name);
        } 
        private static string GetShortName(int index)
        { 
            return PubConst.TableDefaultPreName+ index;
        }

    }
}