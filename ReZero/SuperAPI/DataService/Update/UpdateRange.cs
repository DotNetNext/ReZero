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
    internal class UpdateRange : CommonDataService, IDataService
    {
        public async Task<object> ExecuteAction(DataModel dataModel)
        {
            var db = App.GetDbTableId(dataModel.TableId) ?? App.Db;
            var type = await EntityGeneratorManager.GetTypeAsync(dataModel.TableId);
            base.InitDb(type, db);
            this.InitData(type, db, dataModel);
            CheckSystemData(db, dataModel, type, db.EntityMaintenance.GetEntityInfo(type));
            this.SetDefaultValue(dataModel, db, type);
            int result = await ExecuteUpdate(dataModel, db);
            base.ClearAll(dataModel);
            return GetResult(dataModel, result);
        }

        private static async Task<int> ExecuteUpdate(DataModel dataModel, ISqlSugarClient db)
        {
            var list= ((IList)dataModel.Data!).Cast<object>().ToList();
            var result = 0;
            try
            {
                db.Ado.BeginTran();
                await db.Utilities.PageEachAsync(list, 1, async item =>
                {
                    var updateable = db.UpdateableByObject(item);
                    UpdateCommonMethodInfo updateCommonMethodInfo = null!;
                    updateCommonMethodInfo = GetUpdateable(dataModel, updateable);
                    result += await updateCommonMethodInfo.ExecuteCommandAsync();
                });
                db.Ado.CommitTran();
            }
            catch (Exception)
            {
                db.Ado.RollbackTran();
                throw;
            }
            return result;
        }

        internal new void InitData(Type type, ISqlSugarClient db, DataModel dataModel)
        {
            var json = dataModel?.DefaultParameters?.FirstOrDefault().Value + "";
            object obj = JsonConvert.DeserializeObject(json, typeof(List<>).MakeGenericType(type))!;
            dataModel!.Data = obj;
        }
        private static UpdateCommonMethodInfo GetUpdateable(DataModel dataModel, UpdateMethodInfo updateable)
        {
            UpdateCommonMethodInfo updateCommonMethodInfo; 
            if (!string.IsNullOrEmpty(dataModel.TableColumns))
            {
                updateCommonMethodInfo = updateable.UpdateColumns(dataModel.TableColumns.Split(","));
            }
            else
            {
                updateCommonMethodInfo = updateable.UpdateColumns();
            }

            return updateCommonMethodInfo;
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

        private void SetDefaultValue(DataModel dataModel, ISqlSugarClient db, Type type)
        {
            if (EntityMappingService.IsAnyDefaultValue(dataModel))
            {
                foreach (var item in (IList)dataModel.Data!)
                {
                    var para = new DataModel()
                    {
                        Data = item,
                        DefaultValueColumns = dataModel.DefaultValueColumns
                    };
                    EntityMappingService.GetDataByDefaultValueParameters(type, db, para);
                }
            }
        }
    }
}
