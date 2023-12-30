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
        public static async Task<List<ErrorParameter>> Check(DataModel dataModel)
        {
            List<ErrorParameter> errorLists = new List<ErrorParameter>();
            foreach (var item in dataModel.DefaultParameters ?? new List<DataModelDefaultParameter>())
            {
                if (item?.ParameterValidate?.IsRequired == true && string.IsNullOrEmpty(item.Value + ""))
                {
                    errorLists.Add(new ErrorParameter() { Name = item.Name, ErrorType = "IsRequired", Message = TextHandler.GetCommonText("必填", "Required") });
                }
                if (item?.ParameterValidate?.IsUnique == true && dataModel.ActionType == ActionType.InsertObject)
                {
                    var type = await EntityGeneratorManager.GetTypeAsync(dataModel.TableId);
                    var db = App.GetDbTableId(dataModel.TableId);
                    var entityInfo = db!.EntityMaintenance.GetEntityInfo(type);
                    var dbColumnInfo = entityInfo.Columns.FirstOrDefault(it => it.PropertyName.EqualsCase(item.Name!));
                    bool isAny = await IsAnyValue(item, type, db, dbColumnInfo);
                    if (isAny)
                    {
                        errorLists.Add(new ErrorParameter() { Name = item.Name, ErrorType = "IsUnique", Message = TextHandler.GetCommonText("唯一", "Unique") });
                    }
                }
                if (item?.ParameterValidate?.IsUnique == true && dataModel.ActionType == ActionType.InsertObject)
                {
                }
            }
            return errorLists;
        }

        private static async Task<bool> IsAnyValue(DataModelDefaultParameter? item, Type type, SqlSugarClient? db, EntityColumnInfo dbColumnInfo)
        {
            var condition = new ConditionalModel()
            {
                ConditionalType = ConditionalType.Equal,
                CSharpTypeName = dbColumnInfo.UnderType.Name,
                FieldValue = item!.Value + "",
                FieldName = dbColumnInfo.DbColumnName

            };
            return await db!.QueryableByObject(type)
                            .Where(new List<IConditionalModel>() { condition })
                            .AnyAsync();
        }

    
    }
}
