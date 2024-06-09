using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using System.Collections;
namespace ReZero.SuperAPI
{
    internal class DeleteRange : CommonDataService,IDataService
    {
        public async Task<object> ExecuteAction(DataModel dataModel)
        {
            var db = App.GetDbTableId(dataModel.TableId) ?? App.Db;
            var type = await EntityGeneratorManager.GetTypeAsync(dataModel.TableId);
            base.InitDb(type, db); 
            this.InitDatas(type, db, dataModel);
            var result = await db.DeleteableByObject(dataModel.Data).ExecuteCommandAsync();
            base.ClearAll(dataModel);
            return GetResult(dataModel, result);
        }
        internal void InitDatas(Type type, ISqlSugarClient db, DataModel dataModel) 
        {
            List<object> list = new List<object>();
            var entityInfo=db.EntityMaintenance.GetEntityInfo(type);
            var pk= entityInfo.Columns.FirstOrDefault(it=>it.IsPrimarykey);
            var json = dataModel.DefaultParameters.FirstOrDefault()?.Value?.ToString()??"[]";
            var listType=typeof(List<>).MakeGenericType(pk.UnderType);
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
                list.Add(dataItem.Data);
            }
            dataModel.Data = list;
        }
        private static object GetResult(DataModel dataModel, int result)
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
