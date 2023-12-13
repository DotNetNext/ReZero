using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReZero.SuperAPI
{
    internal class DllCreateDb : IDataService
    {
        public async Task<object> ExecuteAction(DataModel dataModel)
        {
            await Task.Delay(0);
            var connection = dataModel
               .DefaultParameters.First().Value;
            var DbType = dataModel
              .DefaultParameters.Last().Value;
            try
            {
                SqlSugarClient? db = new SqlSugarClient(new ConnectionConfig()
                {
                    DbType = (DbType)Convert.ToInt32(DbType),
                    ConnectionString = connection + "",
                    IsAutoCloseConnection = true
                });
                db.DbMaintenance.CreateDatabase();
                return true;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
