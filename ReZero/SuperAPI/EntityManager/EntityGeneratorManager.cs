using Newtonsoft.Json.Linq; 
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReZero.SuperAPI
{
    public class EntityGeneratorManager
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
                var propertyType = GetTypeByNativeTypes(item.PropertyType);
                var column = new SugarColumn()
                {
                    ColumnName = item.DbCoumnName,
                    IsJson = item.PropertyType == NativeTypes.Json,
                    IsIdentity = item.IsIdentity,
                    IsPrimaryKey = item.IsPrimarykey,
                };
                if (item.ExtendedAttribute?.ToString() == PubConst.TreeChild) 
                { 
                    propertyType = typeof(DynamicOneselfTypeList);
                    column.IsIgnore = true;
                }
                builder.CreateProperty(item.PropertyName, propertyType, column); 
            }
            var type = builder.BuilderType();
            return type;
        } 
        public static Type GetTypeByNativeTypes(NativeTypes nativeTypes)
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
        public static NativeTypes GetNativeTypeByType(Type type)
        {
            if (type == typeof(int))
                return NativeTypes.Int;
            else if (type == typeof(uint))
                return NativeTypes.UInt;
            else if (type == typeof(short))
                return NativeTypes.Short;
            else if (type == typeof(ushort))
                return NativeTypes.UShort;
            else if (type == typeof(long))
                return NativeTypes.Long;
            else if (type == typeof(ulong))
                return NativeTypes.ULong;
            else if (type == typeof(byte))
                return NativeTypes.Byte;
            else if (type == typeof(sbyte))
                return NativeTypes.SByte;
            else if (type == typeof(float))
                return NativeTypes.Float;
            else if (type == typeof(double))
                return NativeTypes.Double;
            else if (type == typeof(decimal))
                return NativeTypes.Decimal;
            else if (type == typeof(char))
                return NativeTypes.Char;
            else if (type == typeof(bool))
                return NativeTypes.Bool;
            else if (type == typeof(string))
                return NativeTypes.String;
            else if (type == typeof(DateTime))
                return NativeTypes.DateTime;
            else if (type == typeof(TimeSpan))
                return NativeTypes.TimeSpan;
            else if (type == typeof(Guid))
                return NativeTypes.Guid;
            else if (type == typeof(byte[]))
                return NativeTypes.ByteArray;
            else if (typeof(JToken).IsAssignableFrom(type))
                return NativeTypes.Json;
            else if (type.IsEnum)
                return NativeTypes.Int;
            // Check if the type is nullable and get the underlying type
            Type underlyingType = Nullable.GetUnderlyingType(type);
            if (underlyingType != null)
            {
                return GetNativeTypeByType(underlyingType);
            }

            return NativeTypes.Json;
        }

        internal static NativeTypes GetNativeTypeByDataType(string dataType)
        {
            return NativeTypes.Int;
        }
    }
}
