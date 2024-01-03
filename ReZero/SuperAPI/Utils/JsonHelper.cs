using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters; 


namespace ReZero.SuperAPI 
{ 
    public static class JsonHelper
    {
        private static readonly JsonSerializerSettings DefaultJsonSerializerSettings = new JsonSerializerSettings
        {
            Converters = new List<JsonConverter> { new StringLongConverter() },
            // 其他设置...
        };

        public static string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(obj, DefaultJsonSerializerSettings);
        }
    }

    public class StringLongConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(long);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                // 如果 JSON 中的值是字符串，将其解析为 long 类型
                if (long.TryParse(reader.Value?.ToString(), out long result))
                {
                    return result;
                }
                else
                {
                    throw new JsonSerializationException($"Unable to parse '{reader.Value}' as long.");
                }
            }
            else if (reader.TokenType == JsonToken.Integer)
            {
                // 如果 JSON 中的值是整数，直接转换为 long 类型
                return Convert.ToInt64(reader.Value);
            }
            else
            {
                throw new JsonSerializationException($"Unexpected token type: {reader.TokenType}");
            }
        }
    }
}
