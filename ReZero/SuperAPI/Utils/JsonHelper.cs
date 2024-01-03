using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace ReZero.SuperAPI
{
    /// <summary>
    /// Provides helper methods for JSON serialization and deserialization.
    /// </summary>
    public static class JsonHelper
    {
        private static readonly JsonSerializerSettings DefaultJsonSerializerSettings = new JsonSerializerSettings
        {
            Converters = new List<JsonConverter> { new StringLongConverter() },
            // Other settings...
        };

        /// <summary>
        /// Serializes an object to a JSON string.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>The JSON string representation of the object.</returns>
        public static string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(obj, DefaultJsonSerializerSettings);
        }
    }

    /// <summary>
    /// Converts a string or integer to a long value during JSON serialization and deserialization.
    /// </summary>
    public class StringLongConverter : JsonConverter
    {
        /// <summary>
        /// Determines whether this converter can convert the specified object type.
        /// </summary>
        /// <param name="objectType">The type of the object to convert.</param>
        /// <returns>true if the converter can convert the specified type; otherwise, false.</returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(long);
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The JSON writer.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="serializer">The JSON serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The JSON reader.</param>
        /// <param name="objectType">The type of the object to convert.</param>
        /// <param name="existingValue">The existing value of the object being read.</param>
        /// <param name="serializer">The JSON serializer.</param>
        /// <returns>The deserialized object.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                // Parse the value as a long if it is a string in the JSON
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
                // Convert the value directly to long if it is an integer in the JSON
                return Convert.ToInt64(reader.Value);
            }
            else
            {
                throw new JsonSerializationException($"Unexpected token type: {reader.TokenType}");
            }
        }
    }
}
