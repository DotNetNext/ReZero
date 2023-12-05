using Kdbndp.TypeHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReZero.SuperAPI 
{
    internal class BizDeleteObject
    {
        public async Task<object> ExecuteAction(DataModel dataModel)
        {
            var db = App.Db;
            var type = await EntityGeneratorManager.GetTypeAsync(dataModel.TableId);
            var entity = db.EntityMaintenance.GetEntityInfo(type);
            if (!entity.Columns.Any(it => it.PropertyName?.ToLower() == "isdeleted")) 
            {
                throw new Exception(TextHandler.GetCommonTexst(type .Name+ "没有IsDeleted属性不能逻辑删除", type.Name + "Cannot be logically deleted without IsDeleted attribute"));
            }
            await db.UpdateableByObject(dataModel.Data)
                    .UpdateColumns("isdeleted")
                    .ExecuteCommandAsync();
            return true;
        }
    }
}
