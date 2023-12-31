using Newtonsoft.Json.Linq; 
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Text.RegularExpressions;
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

                var typeName = item.PropertyType.ToString();
                if (StringHasLength(typeName))
                {
                    item.Length = ExtractNumericPart(typeName);
                    item.PropertyType = NativeType.String;
                }
                else if (DecimalHasLength(typeName))
                {
                    var typeInfo = ExtractTypeInformation(typeName);
                    item.Length = typeInfo.Length;
                    item.DecimalDigits = typeInfo.DecimalDigits;
                    item.PropertyType = NativeType.Decimal;
                }
                var propertyType = GetTypeByNativeTypes(item.PropertyType);
                var column = new SugarColumn()
                {
                    ColumnName = item.DbColumnName,
                    IsJson = item.PropertyType == NativeType.Json,
                    IsIdentity = item.IsIdentity,
                    IsPrimaryKey = item.IsPrimarykey,
                    DecimalDigits = item.DecimalDigits,
                    Length = item.Length,
                    ColumnDataType = item.DataType
                };
                if (DataTypeHasLength(column))
                {
                    item.Length = 0;
                    item.DecimalDigits = 0;
                }
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

        private static bool DecimalHasLength(string typeName)
        {
            return typeName.StartsWith("Decimal") && ExtractNumericPart(typeName) > 0;
        }

        private static bool StringHasLength(string typeName)
        {
            return typeName.StartsWith("String") && ExtractNumericPart(typeName) > 0;
        }

        private static bool DataTypeHasLength(SugarColumn column)
        {
            return ExtractNumericPart(column.ColumnDataType ?? "") > 0 && (column.ColumnDataType ?? "").Contains("(") && (column.ColumnDataType ?? "").Contains(")");
        }

        public static DbColumnInfo ExtractTypeInformation(string typeName)
        {
            // 使用正则表达式匹配数字部分
            Match match = Regex.Match(typeName, @"_(\d+)_(\d+)$");

            if (match.Success && match.Groups.Count == 3)
            {
                int length = int.Parse(match.Groups[1].Value);
                int precision = int.Parse(match.Groups[2].Value);

                return new DbColumnInfo { Length = length, DecimalDigits = precision };
            }

            throw new FormatException($"Invalid type format: {typeName}");
        }
        public static int ExtractNumericPart(string input)
        {
            Match match = Regex.Match(input, @"\d+");
            if (match.Success)
            {
                return int.Parse(match.Value);
            }
            return 0; // 默认值，如果未找到数字部分
        }
        public static Type GetTypeByNativeTypes(NativeType nativeTypes)
        {
            switch (nativeTypes)
            {
                case NativeType.Int:
                    return typeof(int);
                case NativeType.UInt:
                    return typeof(uint);
                case NativeType.Short:
                    return typeof(short);
                case NativeType.UShort:
                    return typeof(ushort);
                case NativeType.Long:
                    return typeof(long);
                case NativeType.ULong:
                    return typeof(ulong);
                case NativeType.Byte:
                    return typeof(byte);
                case NativeType.SByte:
                    return typeof(sbyte);
                case NativeType.Float:
                    return typeof(float);
                case NativeType.Double:
                    return typeof(double);
                case NativeType.Decimal:
                    return typeof(decimal);
                case NativeType.Char:
                    return typeof(char);
                case NativeType.Bool:
                    return typeof(bool);
                case NativeType.String:
                    return typeof(string);
                case NativeType.DateTime:
                    return typeof(DateTime);
                case NativeType.TimeSpan:
                    return typeof(TimeSpan);
                case NativeType.Guid:
                    return typeof(Guid);
                case NativeType.ByteArray:
                    return typeof(byte[]);
                case NativeType.Json:
                    return typeof(object); // Assuming Json is a placeholder for any JSON-related type
                default:
                    throw new ArgumentException("Unsupported NativeType");
            }
        }
        public static NativeType GetNativeTypeByType(Type type)
        {
            if (type == typeof(int))
                return NativeType.Int;
            else if (type == typeof(uint))
                return NativeType.UInt;
            else if (type == typeof(short))
                return NativeType.Short;
            else if (type == typeof(ushort))
                return NativeType.UShort;
            else if (type == typeof(long))
                return NativeType.Long;
            else if (type == typeof(ulong))
                return NativeType.ULong;
            else if (type == typeof(byte))
                return NativeType.Byte;
            else if (type == typeof(sbyte))
                return NativeType.SByte;
            else if (type == typeof(float))
                return NativeType.Float;
            else if (type == typeof(double))
                return NativeType.Double;
            else if (type == typeof(decimal))
                return NativeType.Decimal;
            else if (type == typeof(char))
                return NativeType.Char;
            else if (type == typeof(bool))
                return NativeType.Bool;
            else if (type == typeof(string))
                return NativeType.String;
            else if (type == typeof(DateTime))
                return NativeType.DateTime;
            else if (type == typeof(TimeSpan))
                return NativeType.TimeSpan;
            else if (type == typeof(Guid))
                return NativeType.Guid;
            else if (type == typeof(byte[]))
                return NativeType.ByteArray;
            else if (typeof(JToken).IsAssignableFrom(type))
                return NativeType.Json;
            else if (type.IsEnum)
                return NativeType.Int;
            // Check if the type is nullable and get the underlying type
            Type underlyingType = Nullable.GetUnderlyingType(type);
            if (underlyingType != null)
            {
                return GetNativeTypeByType(underlyingType);
            }

            return NativeType.Json;
        }

        internal static NativeType GetNativeTypeByDataType(string dataType)
        {
            return NativeType.Int;
        }
    }
}
