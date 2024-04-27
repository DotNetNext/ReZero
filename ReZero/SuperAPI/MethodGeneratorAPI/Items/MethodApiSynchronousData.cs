using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    public partial class MethodApi
    {
        private long _targetDb;
        public bool SynchronousData(long originalDb, long targetDb, bool? isBak)
        {
            _targetDb = targetDb;
            var odb = App.Db;
            var tdb = App.GetDbById(targetDb);
            if (!tdb!.Ado.IsValidConnection()) 
            {
                new Exception(TextHandler.GetCommonText("目标数据库连接失败", "The target database connection failed"));
            }
            tdb!.CurrentConnectionConfig.MoreSettings=odb.CurrentConnectionConfig.MoreSettings;
            tdb!.CurrentConnectionConfig.ConfigureExternalServices = odb.CurrentConnectionConfig.ConfigureExternalServices;
            try
            {
                tdb!.CodeFirst.InitTables(typeof(ZeroEntityInfo),
                                           typeof(ZeroEntityColumnInfo),
                                           typeof(ZeroInterfaceCategory),
                                           typeof(ZeroInterfaceList),
                                           typeof(ZeroDatabaseInfo),
                                           typeof(ZeroUserInfo));
                tdb!.BeginTran();
                var randomNum =Convert.ToInt32( DateTime.Now.ToString("HHmmss"));
                SynchronousTable<ZeroEntityInfo>(odb, tdb, isBak, randomNum);
                SynchronousTable<ZeroEntityColumnInfo>(odb, tdb, isBak, randomNum);
                SynchronousTable<ZeroInterfaceCategory>(odb, tdb, isBak, randomNum);
                SynchronousTable<ZeroInterfaceList>(odb, tdb, isBak, randomNum);
                SynchronousTable<ZeroDatabaseInfo>(odb, tdb, isBak, randomNum);
                SynchronousTable<ZeroUserInfo>(odb, tdb, isBak, randomNum);
                var tIno = odb.Queryable<ZeroDatabaseInfo>().First(it => it.Id == targetDb); 
                tdb.Updateable<ZeroDatabaseInfo>()
                    .SetColumns(it => new ZeroDatabaseInfo
                    {
                        DbType = tIno.DbType,
                        Connection = tIno.Connection,
                        EasyDescription = tIno.EasyDescription,
                    })
                    .Where(it => it.Id == 1).ExecuteCommand();
                tdb.Deleteable<ZeroDatabaseInfo>().Where(it => it.Id == targetDb).ExecuteCommand();
                tdb.CommitTran();
            }
            catch (Exception)
            {
                tdb!.CommitTran();
                throw;
            }
            return true;
        }

        private void SynchronousTable<T>(ISqlSugarClient? odb, SqlSugar.SqlSugarClient? tdb, bool? isBak, int randomNum) where T : class, new()
        {

            var tTableName = tdb!.EntityMaintenance.GetTableName<T>();
            var newtTableName = tTableName.ToLower().Replace("zero_","_bak_") + randomNum;
            var oldList = odb!.Queryable<T>().ToList();
            if (isBak == true)
            {
                tdb!.CodeFirst.As<T>(newtTableName).InitTables<T>();
                tdb.DbMaintenance.TruncateTable(newtTableName);
                tdb.Insertable(oldList).AS(newtTableName).ExecuteCommand();
            }
            tdb.DbMaintenance.TruncateTable<T>(); 
            tdb.Insertable(oldList).ExecuteCommand();
        }
    }
}
