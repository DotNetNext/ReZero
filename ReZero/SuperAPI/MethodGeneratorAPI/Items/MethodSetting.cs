using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ReZero.SuperAPI
{
    public partial class MethodApi
    {
        public bool  UpdateSetting(int typeId,int childTypeId,string value) 
        {
            var db = App.Db;
            if (typeId == PubConst.Setting_EntityType && childTypeId == PubConst.Setting_ImportUnunderlineType) 
            {
                var newValue = Convert.ToBoolean(value?.ToLower());
                db.Updateable<ZeroSysSetting>()
                    .SetColumns(it => it.BoolValue == newValue)
                    .Where(it => it.TypeId == PubConst.Setting_EntityType)
                    .Where(it => it.ChildTypeId == PubConst.Setting_ImportUnunderlineType)
                    .ExecuteCommand();
            }
            return true;
        }
        public object GetSetting(int typeId, int childTypeId)
        {
            var db = App.Db;
            var result = db.Queryable<ZeroSysSetting>()
                 .Where(it => it.TypeId == typeId)
                    .Where(it => it.ChildTypeId == childTypeId).First();
            return result;
        }
    }
}
