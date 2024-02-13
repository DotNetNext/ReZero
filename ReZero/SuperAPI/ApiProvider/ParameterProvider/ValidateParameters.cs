using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ReZero.SuperAPI 
{
    internal class ValidateParameters
    {
        public static async Task<List<ErrorParameter>> CheckAsync(DataModel dataModel)
        {
            List<ErrorParameter> errorLists = new List<ErrorParameter>();
            foreach (var item in dataModel.DefaultParameters ?? new List<DataModelDefaultParameter>())
            {
                if (IsRequired(item))
                {
                    AddReuiredError(errorLists, item);
                }
                if (IsInsertUnique(dataModel, item))
                {
                    await AddInsertUniqueError(dataModel, errorLists, item);
                }
                if (item?.ParameterValidate?.IsUnique == true && dataModel.ActionType == ActionType.InsertObject)
                {
                }
            }
            return errorLists;
        }

        #region Add Error
        private static async Task AddInsertUniqueError(DataModel dataModel, List<ErrorParameter> errorLists, DataModelDefaultParameter item)
        {
            var type = await EntityGeneratorManager.GetTypeAsync(dataModel.TableId);
            var db = App.GetDbTableId(dataModel.TableId);
            var entityInfo = db!.EntityMaintenance.GetEntityInfo(type);
            var dbColumnInfo = entityInfo.Columns.FirstOrDefault(it => it.PropertyName.EqualsCase(item.Name!));
            var isDeleteIdColumn = entityInfo.Columns.FirstOrDefault(it => it.PropertyName.EqualsCase(nameof(DbBase.IsDeleted)));
            bool isAny = await IsAnyValue(item, type, db, dbColumnInfo, isDeleteIdColumn, dataModel);
            if (isAny)
            {
                errorLists.Add(new ErrorParameter() { Name = item.Name, ErrorType = "IsUnique", Message = TextHandler.GetCommonText("唯一", "Unique") });
            }
        }
        private static void AddReuiredError(List<ErrorParameter> errorLists, DataModelDefaultParameter item)
        {
            errorLists.Add(new ErrorParameter() { Name = item.Name, ErrorType = "IsRequired", Message = TextHandler.GetCommonText("必填", "Required") });
        }

        #endregion

        #region Validate
        private static bool IsInsertUnique(DataModel dataModel, DataModelDefaultParameter item)
        {
            return item?.ParameterValidate?.IsUnique == true && dataModel.ActionType == ActionType.InsertObject;
        }
        private static bool IsRequired(DataModelDefaultParameter item)
        {
            return item?.ParameterValidate?.IsRequired == true && string.IsNullOrEmpty(item.Value + "");
        }
        private static async Task<bool> IsAnyValue(DataModelDefaultParameter? item, Type type, SqlSugarClient? db, EntityColumnInfo dbColumnInfo, EntityColumnInfo isDeleteIdColumn, DataModel dataModel)
        {
            var condition = new ConditionalModel()
            {
                ConditionalType = ConditionalType.Equal,
                CSharpTypeName = dbColumnInfo.UnderType.Name,
                FieldValue = item!.Value + "",
                FieldName = dbColumnInfo.DbColumnName

            };
            var whereColumns = new List<IConditionalModel>() { condition };
            if (isDeleteIdColumn != null)
            {
                var condition2 = new ConditionalModel()
                {
                    ConditionalType = ConditionalType.Equal,
                    CSharpTypeName = typeof(bool).Name,
                    FieldValue = false.ToString().ToLower(),
                    FieldName = isDeleteIdColumn.DbColumnName
                };
                whereColumns.Add(condition2);
            }
            if (type.Name == nameof(ZeroEntityInfo))
            {
                var condition3 = new ConditionalModel()
                {
                    ConditionalType = ConditionalType.Equal,
                    CSharpTypeName = typeof(long).Name,
                    FieldValue = dataModel.DefaultParameters.First(it => it.Name!.EqualsCase(nameof(ZeroEntityInfo.DataBaseId))).Value + "",
                    FieldName = db!.EntityMaintenance.GetEntityInfo(type).Columns.First(it => it.PropertyName == nameof(ZeroEntityInfo.DataBaseId)).DbColumnName
                };
                whereColumns.Add(condition3);
            }
            return await db!.QueryableByObject(type)
                            .Where(whereColumns)
                            .AnyAsync();
        }
        #endregion

    }
}
