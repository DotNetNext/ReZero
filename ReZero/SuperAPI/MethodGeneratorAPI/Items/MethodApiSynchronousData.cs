using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    public partial class MethodApi
    {
        public bool SynchronousData(long originalDb, long targetDb, bool? isBak) 
        {
            var odb=App.GetDbById(originalDb);
            var tdb = App.GetDbById(targetDb);
            SynchronousTable<ZeroEntityInfo>(odb, tdb,isBak);
            SynchronousTable<ZeroEntityColumnInfo>(odb, tdb, isBak);
            SynchronousTable<ZeroInterfaceCategory>(odb, tdb, isBak);
            SynchronousTable<ZeroInterfaceList>(odb, tdb, isBak);
            return true;
        }

        private void SynchronousTable<T>(SqlSugar.SqlSugarClient? odb, SqlSugar.SqlSugarClient? tdb, bool? isBak)
        {
            var tTableName = tdb!.EntityMaintenance.GetTableName<T>();
            var newtTableName= tTableName + PubConst.Common_Random.Next(1,999999);
            if (isBak==true)
            {
                tdb!.DbMaintenance.BackupTable(tTableName, newtTableName);
            }
            try
            {
                tdb.BeginTran();
                tdb.DbMaintenance.TruncateTable<T>();
                var oldList = odb!.Queryable<T>().ToList();
                tdb.Insertable(oldList).ExecuteCommand();
                tdb.CommitTran();
            }
            catch (Exception)
            {
                tdb.CommitTran();
                throw;
            }
        }
    }
}
