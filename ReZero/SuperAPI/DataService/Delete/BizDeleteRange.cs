using DocumentFormat.OpenXml.Vml.Office;
using Kdbndp.TypeHandlers;
using Newtonsoft.Json;
using SqlSugar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReZero.SuperAPI 
{
    internal class BizDeleteRange : CommonDataService, IDataService
    {
        public async Task<object> ExecuteAction(DataModel dataModel)
        {
            var db = App.GetDbTableId(dataModel.TableId) ?? App.Db;
            var type = await EntityGeneratorManager.GetTypeAsync(dataModel.TableId);
            base.InitDb(type, db);
            this.InitDatas(type, db, dataModel);
            var entity = db.EntityMaintenance.GetEntityInfo(type);
            if (!entity.Columns.Any(it => it.PropertyName.EqualsCase(nameof(DbBase.IsDeleted))))
            {
                throw new Exception(TextHandler.GetCommonText(type.Name + "没有IsDeleted属性不能逻辑删除", type.Name + "Cannot be logically deleted without IsDeleted attribute"));
            }
            CheckSystemData(db, dataModel, type, entity);
            var column = entity.Columns.FirstOrDefault(it => it.PropertyName.EqualsCase(nameof(DbBase.IsDeleted)));
            //column.PropertyInfo.SetValue(dataModel.Data, true);
            var result = db.UpdateableByObject(dataModel.Data)
                    .UpdateColumns("isdeleted")
                    .ExecuteCommandAsync();
            base.ClearAll(dataModel);
            return GetResult(dataModel, result);
        }

        internal void InitDatas(Type type, ISqlSugarClient db, DataModel dataModel)
        {
            List<object> list = new List<object>();
            var entityInfo = db.EntityMaintenance.GetEntityInfo(type);
            var pk = entityInfo.Columns.FirstOrDefault(it => it.IsPrimarykey);
            var column = entityInfo.Columns.FirstOrDefault(it => it.PropertyName.EqualsCase(nameof(DbBase.IsDeleted)));
            var json = dataModel.DefaultParameters.FirstOrDefault()?.Value?.ToString() ?? "[]";
            var listType = typeof(List<>).MakeGenericType(pk.UnderType);
            var objs = ((IList)JsonConvert.DeserializeObject(json, listType)!).Cast<object>().ToList();
            foreach (var item in objs)
            {
                var dataItem = new DataModel()
                {
                    Data = item,
                    DefaultParameters = new List<DataModelDefaultParameter>()
                    {
                     new DataModelDefaultParameter(){ Value=item, Name=pk.PropertyName }
                    }
                };
                base.InitData(type, db, dataItem);
                column.PropertyInfo.SetValue(dataItem.Data,true);
                list.Add(dataItem.Data);
            }
            dataModel.Data = list;
        }

        private static object GetResult(DataModel dataModel, Task<int> result)
        {
            if (dataModel.ResultType == SqlResultType.AffectedRows)
            {
                return result;
            }
            else
            {
                return true;
            }
        }
    }
}
