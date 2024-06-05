using Kdbndp.TypeHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReZero.SuperAPI 
{
    internal class BizDeleteObject : CommonDataService, IDataService
    {
        public async Task<object> ExecuteAction(DataModel dataModel)
        {
            base.ClearZeroInterfaceListCache(dataModel);
            base.ClearZeroEntityInfoInfoCache(dataModel);
            base.ClearZeroDatabaseInfoCache(dataModel);
            var db = App.GetDbTableId(dataModel.TableId) ?? App.Db;
            var type = await EntityGeneratorManager.GetTypeAsync(dataModel.TableId);
            base.InitDb(type, db);
            base.InitData(type, db, dataModel);
            var entity = db.EntityMaintenance.GetEntityInfo(type);
            if (!entity.Columns.Any(it => it.PropertyName.EqualsCase(nameof(DbBase.IsDeleted))))
            {
                throw new Exception(TextHandler.GetCommonText(type.Name + "没有IsDeleted属性不能逻辑删除", type.Name + "Cannot be logically deleted without IsDeleted attribute"));
            }
            CheckSystemData(db,dataModel, type, entity); 
            var column = entity.Columns.FirstOrDefault(it => it.PropertyName.EqualsCase(nameof(DbBase.IsDeleted)));
            column.PropertyInfo.SetValue(dataModel.Data, true);
            await db.UpdateableByObject(dataModel.Data)
                    .UpdateColumns("isdeleted")
                    .ExecuteCommandAsync();
            return true;
        }

    }
}
