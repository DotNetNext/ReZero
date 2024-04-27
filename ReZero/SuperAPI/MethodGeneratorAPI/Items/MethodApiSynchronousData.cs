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
                var randomNum = +PubConst.Common_Random.Next(1, 999999);
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
            var newtTableName = tTableName + randomNum;
            if (isBak == true)
            {
                tdb!.DbMaintenance.BackupTable(tTableName, newtTableName);
            }
            tdb.DbMaintenance.TruncateTable<T>();
            var oldList = odb!.Queryable<T>().ToList();
            tdb.Insertable(oldList).ExecuteCommand();
        }
    }
}
