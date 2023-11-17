using Newtonsoft.Json.Linq;
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

        public static Type GetType(NativeTypes nativeTypes)
        {
            switch (nativeTypes)
            {
                case NativeTypes.Int:
                    return typeof(int);
                case NativeTypes.UInt:
                    return typeof(uint);
                case NativeTypes.Short:
                    return typeof(short);
                case NativeTypes.UShort:
                    return typeof(ushort);
                case NativeTypes.Long:
                    return typeof(long);
                case NativeTypes.ULong:
                    return typeof(ulong);
                case NativeTypes.Byte:
                    return typeof(byte);
                case NativeTypes.SByte:
                    return typeof(sbyte);
                case NativeTypes.Float:
                    return typeof(float);
                case NativeTypes.Double:
                    return typeof(double);
                case NativeTypes.Decimal:
                    return typeof(decimal);
                case NativeTypes.Char:
                    return typeof(char);
                case NativeTypes.Bool:
                    return typeof(bool);
                case NativeTypes.String:
                    return typeof(string);
                case NativeTypes.DateTime:
                    return typeof(DateTime);
                case NativeTypes.TimeSpan:
                    return typeof(TimeSpan);
                case NativeTypes.Guid:
                    return typeof(Guid);
                case NativeTypes.ByteArray:
                    return typeof(byte[]);
                case NativeTypes.Json:
                    return typeof(object); // Assuming Json is a placeholder for any JSON-related type
                default:
                    throw new ArgumentException("Unsupported NativeType");
            }
        }
    }
}
