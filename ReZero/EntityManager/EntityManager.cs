using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReZero
{
    public class EntityManager
    {
        public async static Task<Type> GetTypeAsync(long tableId)
        {
            var db = App.Db;
            var tableInfo = await db.Queryable<ZeroEntityInfo>().Includes(x => x.ZeroEntityColumnInfos).InSingleAsync(tableId);
            var builder = db.DynamicBuilder().CreateClass(tableInfo.ClassName, new SqlSugar.SugarTable()
            {
                TableName = tableInfo.DbTableName
            });
            foreach (var item in tableInfo.ZeroEntityColumnInfos ?? new List<ZeroEntityColumnInfo>())
            {
                builder.CreateProperty(item.PropertyName, GetType(item.NativeTypes), new SugarColumn()
                {
                    ColumnName = item.DbCoumnName
                });
            }
            var type = builder.BuilderType();
            return type;
        }

        private static Type GetType(NativeTypes nativeTypes)
        {
            return typeof(string);
        }
    }
}
