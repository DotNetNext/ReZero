using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    internal class EntityPropertyMappingService
    {
        public static object ConvertValue(object value, string valueType)
        {
            switch (valueType)
            {
                case "Boolean":
                    return Convert.ToBoolean(value);
                case "Byte":
                    return Convert.ToByte(value);
                case "SByte":
                    return Convert.ToSByte(value);
                case "Char":
                    return Convert.ToChar(value);
                case "Decimal":
                    return Convert.ToDecimal(value);
                case "Double":
                    return Convert.ToDouble(value);
                case "Single":
                    return Convert.ToSingle(value);
                case "Int32":
                    return Convert.ToInt32(value);
                case "UInt32":
                    return Convert.ToUInt32(value);
                case "Int64":
                    return Convert.ToInt64(value);
                case "UInt64":
                    return Convert.ToUInt64(value);
                case "Int16":
                    return Convert.ToInt16(value);
                case "UInt16":
                    return Convert.ToUInt16(value);
                case "String":
                    return Convert.ToString(value);
                case "DateTime":
                    return Convert.ToDateTime(value);
                case "Guid":
                    return new Guid(Convert.ToString(value));
                case "Byte[]":
                    // 假设 value 是字节数组的合法表示（例如十六进制字符串），进行转换
                    return HexStringToByteArray(Convert.ToString(value));
                default:
                    return value;
            }
        }
        // 将十六进制字符串转换为字节数组
        private static byte[] HexStringToByteArray(string hex)
        {
            if (hex.Length % 2 != 0)
            {
                throw new ArgumentException("Hex string must have an even number of characters.");
            }

            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }

            return bytes;
        }
    }
}