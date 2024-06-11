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
            var cacheManage = SqlSugar.ReflectionInoCore<Type>.GetInstance();
            var key = GetTypeCacheKey(tableId);
            if (cacheManage.ContainsKey(key))
            {
                return cacheManage[key];
            }
            else
            {
                var result = await GetType(tableId);
                cacheManage.Add(key, result);
                return result;
            }
        } 

        public static void RemoveTypeCacheByTypeId(long tableId) 
        {
            var cacheManage = SqlSugar.ReflectionInoCore<Type>.GetInstance();
            var key = GetTypeCacheKey(tableId);
            cacheManage.Remove(key);
        } 
        private static string GetTypeCacheKey(long tableId)
        {
            return string.Format(PubConst.CacheKey_Type, tableId);
        }
        private static async Task<Type> GetType(long tableId)
        {
            var db = App.Db;
            var tableInfo = await db.Queryable<ZeroEntityInfo>().Includes(x => x.ZeroEntityColumnInfos).InSingleAsync(tableId);
            CheckTableInfo(tableInfo);
            var builder = db.DynamicBuilder().CreateClass(tableInfo.ClassName, new SqlSugar.SugarTable()
            {
                TableName = tableInfo.DbTableName,
                TableDescription=tableInfo.Description
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
                else if (typeName == NativeType.StringMax + "")
                {
                    item.PropertyType = NativeType.String;
                    item.Length = int.MaxValue;
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
                    ColumnDataType = item.DataType,
                    ColumnDescription = item.Description,
                    IsNullable = item.IsNullable
                };
                if (item.PropertyType==NativeType.String&&column.Length == 0) 
                {
                    column.Length = 255;
                }
                if (column.Length == int.MaxValue)
                {
                    column.ColumnDataType = StaticConfig.CodeFirst_BigString;
                    column.Length = 0;
                    column.DecimalDigits = 0;
                }
                if (DataTypeHasLength(column))
                {
                    item.Length = 0;
                    item.DecimalDigits = 0;
                }
                if (item.ExtendedAttribute?.ToString() == PubConst.Ui_TreeChild)
                {
                    propertyType = typeof(DynamicOneselfTypeList);
                    column.IsIgnore = true;
                }
                if (item.PropertyType == NativeType.IsIgnore) 
                {
                    column.IsIgnore = true;
                }
                builder.CreateProperty(item.PropertyName, propertyType, column);
            }
            var type = builder.BuilderType();
            return type;
        }

        private static void CheckTableInfo(ZeroEntityInfo tableInfo)
        {
            if (tableInfo == null)
            {
                throw new Exception(TextHandler.GetCommonText("实体不存在了", "The entity does not exist"));
            }
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
                case NativeType.IsIgnore:
                    return typeof(object); // Assuming Json is a placeholder for any JSON-related type
                default:
                    if (nativeTypes.ToString().ToLower().StartsWith("string")) 
                    {
                        return typeof(string);
                    }
                    else if (nativeTypes.ToString().ToLower().StartsWith("decimal"))
                    {
                        return typeof(decimal);
                    } 
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

        public static Type GetTypeByString(string typeName)
        {
            if (typeName == null)
                return typeof(object); // Default to object type if no match is found

            typeName = typeName.Trim().ToLower();

            if (typeName == "int")
                return typeof(int);
            else if (typeName == "uint")
                return typeof(uint);
            else if (typeName == "short")
                return typeof(short);
            else if (typeName == "ushort")
                return typeof(ushort);
            else if (typeName == "long")
                return typeof(long);
            else if (typeName == "ulong")
                return typeof(ulong);
            else if (typeName == "byte")
                return typeof(byte);
            else if (typeName == "sbyte")
                return typeof(sbyte);
            else if (typeName == "float")
                return typeof(float);
            else if (typeName == "double")
                return typeof(double);
            else if (typeName == "decimal")
                return typeof(decimal);
            else if (typeName == "char")
                return typeof(char);
            else if (typeName == "bool")
                return typeof(bool);
            else if (typeName == "string")
                return typeof(string);
            else if (typeName == "datetime")
                return typeof(DateTime);
            else if (typeName == "timespan")
                return typeof(TimeSpan);
            else if (typeName == "guid")
                return typeof(Guid);
            else if (typeName == "byte[]")
                return typeof(byte[]);
            else if (typeName == "jtoken")
                return typeof(Newtonsoft.Json.Linq.JToken); // Assuming JToken is from Newtonsoft.Json.Linq
            else
                return typeof(object); // Default to object type if no match is found
        }

        public static string GetNativeTypeName(string  name)
        {
            string result= name??string.Empty; 
            if (name == "Int32")
            {
                result = "int";
            }
            else if (name == "Int64")
            {
                result = "long";
            }
            else if (name == "Int16")
            {
                result = "short";
            }
            else if (name == "String")
            {
                result = "string";
            }
            else if (name == "Decimal")
            {
                result = "decimal";
            }
            else if (name == "Byte")
            {
                result = "byte";
            }
            else if (name == "Double")
            {
                result = "double";
            }
            else if (name == "Boolean")
            {
                result = "bool";
            }
            return result;
        }
    }
}
