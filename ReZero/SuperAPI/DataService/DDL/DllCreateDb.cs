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
            var connection = dataModel.DefaultParameters.First().Value;
            var DbType = dataModel.DefaultParameters.Last().Value;
            try
            {
                var dbType = (DbType)UtilMethods.ChangeType2(DbType, typeof(DbType));
                if (IsNoSupport(dbType))
                {
                    return GetNoSupportText();
                }
                else
                {
                    CreateDatabase(connection, dbType);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private static void CreateDatabase(object? connection, DbType dbType)
        {
            SqlSugarClient? db = new SqlSugarClient(new ConnectionConfig()
            {
                DbType = dbType,
                ConnectionString = connection + "",
                IsAutoCloseConnection = true,
                MoreSettings = new ConnMoreSettings()
                {
                 
                }
            });
            if (App.Language == Language.CN)
            {
                db.CurrentConnectionConfig.LanguageType = LanguageType.Chinese;
            }
            else 
            {
                db.CurrentConnectionConfig.LanguageType = LanguageType.English;
            }
            db.DbMaintenance.CreateDatabase();
        }

        private static string GetNoSupportText()
        {
            return TextHandler.GetCommonTexst($" dm or oracle no support ", "达梦或者Oracle不支持建库");
        }

        private static bool IsNoSupport(DbType dbType)
        {
            return dbType == SqlSugar.DbType.Dm || dbType == SqlSugar.DbType.Oracle;
        }
    }
}
