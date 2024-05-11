using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReZero.SuperAPI
{
    internal class QueryTree : CommonDataService, IDataService
    {
        public async Task<object> ExecuteAction(DataModel dataModel)
        {
            var db = App.GetDbTableId(dataModel.TableId) ?? App.Db;
            RefAsync<int> count = 0;
            var parameter = dataModel.TreeParameter;
            var type = await EntityGeneratorManager.GetTypeAsync(dataModel.TableId);
            base.InitDb(type, db);
            var entity = db.EntityMaintenance.GetEntityInfo(type);
            var pkColumnInfo = entity.Columns.FirstOrDefault(it => it.IsPrimarykey);
            CheckEntityInfo(pkColumnInfo);
            var pkValue = UtilMethods.ChangeType2(dataModel.DefaultParameters.First().Value, pkColumnInfo.PropertyInfo.PropertyType);
            var data = await db.QueryableByObject(type)
                .InSingleAsync(pkValue);
            var typeBuilder = GetTypeBuilder(db, parameter, type, entity);
            var parentCodeName = entity.Columns.FirstOrDefault(it => it.PropertyName.EqualsCase(parameter?.ParentCodePropertyName ?? ""));
            var codeName = entity.Columns.FirstOrDefault(it => it.PropertyName.EqualsCase(parameter?.CodePropertyName ?? ""));
            CheckPars(parentCodeName, codeName);
            object? parentId = new object();
            var treeType = typeBuilder.WithCache().BuilderType();
            if (data != null)
                parentId = data.GetType()?.GetProperty(parentCodeName.PropertyName)?.GetValue(data) ?? 1;
            var result = await db.QueryableByObject(treeType)
                          .ToTreeAsync(parameter?.ChildPropertyName, parentCodeName.PropertyName, parentId, codeName.PropertyName);
            return result;
        }

        private static void CheckEntityInfo(EntityColumnInfo pkColumnInfo)
        {
            if (pkColumnInfo == null)
            {
                throw new Exception(TextHandler.GetCommonText("实体没有配置主键", "The entity is not configured with a primary key"));
            }
        } 
        private static DynamicProperyBuilder GetTypeBuilder(ISqlSugarClient db, DataModelTreeParameter? parameter, Type type, EntityInfo entity)
        {
            var typeBuilder = db.DynamicBuilder().CreateClass("Tree" + type.Name, new SugarTable() { TableName = entity.DbTableName }
           )
           .CreateProperty(parameter?.ChildPropertyName, typeof(DynamicOneselfTypeList), new SugarColumn() { IsIgnore = true });
            foreach (var item in entity.Columns)
            {
                typeBuilder.CreateProperty(item.PropertyName, item.PropertyInfo.PropertyType, new SugarColumn()
                {
                    ColumnName = item.DbColumnName
                });
            }

            return typeBuilder;
        } 
        private static void CheckPars(EntityColumnInfo parentCodeName, EntityColumnInfo codeName)
        {
            if (parentCodeName == null)
            {
                throw new Exception(TextHandler.GetCommonText("实体没有配置父级编码", "The entity is not configured with a parent code"));
            }
            if (codeName == null)
            {
                throw new Exception(TextHandler.GetCommonText("实体没有配置编码", "The entity is not configured with a code"));
            }
        }
    }
}
