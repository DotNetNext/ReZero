using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace ReZero.SuperAPI
{
    internal class UpdateObject : CommonDataService, IDataService
    {
        public async Task<object> ExecuteAction(DataModel dataModel)
        {  
            var db = App.GetDbTableId(dataModel.TableId) ?? App.Db;
            var type = await EntityGeneratorManager.GetTypeAsync(dataModel.TableId);
            base.InitDb(type, db);
            base.InitData(type, db, dataModel);
            CheckSystemData(db, dataModel, type, db.EntityMaintenance.GetEntityInfo(type));
            this.SetDefaultValue(dataModel, db, type);
            var updateable = db.UpdateableByObject(dataModel.Data);
            UpdateCommonMethodInfo updateCommonMethodInfo = null!;
            if (!string.IsNullOrEmpty(dataModel.TableColumns))
            {
                updateCommonMethodInfo = updateable.UpdateColumns(dataModel.TableColumns.Split(","));
            }
            else 
            {
                updateCommonMethodInfo= updateable.UpdateColumns(dataModel.DefaultParameters.Select(it => it.Name).ToArray());
            }
            var result=await updateCommonMethodInfo.ExecuteCommandAsync();
            base.ClearAll(dataModel);
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
                dataModel.Data = EntityMappingService.GetDataByDefaultValueParameters(type, db, dataModel);
            }
        }
    }
}
