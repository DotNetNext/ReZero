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
            var odb = App.GetDbById(originalDb);
            var tdb = App.GetDbById(targetDb);
            try
            {
                odb!.CodeFirst.InitTables(typeof(ZeroEntityInfo), 
                                           typeof(ZeroEntityColumnInfo), 
                                           typeof(ZeroInterfaceCategory), 
                                           typeof(ZeroInterfaceList));
                tdb!.BeginTran(); 
                var randomNum = +PubConst.Common_Random.Next(1, 999999);
                SynchronousTable<ZeroEntityInfo>(odb, tdb, isBak, randomNum);
                SynchronousTable<ZeroEntityColumnInfo>(odb, tdb, isBak, randomNum);
                SynchronousTable<ZeroInterfaceCategory>(odb, tdb, isBak, randomNum);
                SynchronousTable<ZeroInterfaceList>(odb, tdb, isBak, randomNum);
                tdb.CommitTran();
            }
            catch (Exception)
            {
                tdb!.CommitTran();
                throw;
            }
            return true;
        }

        private void SynchronousTable<T>(SqlSugar.SqlSugarClient? odb, SqlSugar.SqlSugarClient? tdb, bool? isBak, int randomNum)
        {
           
            var tTableName = tdb!.EntityMaintenance.GetTableName<T>();
            var newtTableName = tTableName + randomNum;
            if (isBak == true)
            {
                tdb!.DbMaintenance.BackupTable(tTableName, newtTableName);
            } 
            tdb.DbMaintenance.TruncateTable<T>();
            var oldList = odb!.Queryable<T>().ToList();
            if (typeof(T) == typeof(ZeroEntityInfo))
            {
                foreach (var item in oldList as List<ZeroEntityInfo>??new List<ZeroEntityInfo>())
                { 
                    item.DataBaseId = _targetDb;
                }
            }
            tdb.Insertable(oldList).ExecuteCommand();

        }
    }
}
