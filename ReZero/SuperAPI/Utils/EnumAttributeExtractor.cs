using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ReZero.SuperAPI 
{ 
    public static class EnumAttributeExtractor
    {
        /// <summary>
        /// Gets the attribute values of the specified enum type.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <returns>A list of EnumAttributeValues.</returns>
        public static List<EnumAttributeValues> GetEnumAttributeValues<T>() where T : Enum
        {
            var values = Enum.GetValues(typeof(T)).Cast<T>().ToList();

            List<EnumAttributeValues> attributeValuesList = new List<EnumAttributeValues>();

            foreach (var value in values)
            {
                var enumType = typeof(T);
                var fieldInfo = enumType.GetField(value.ToString());

                var chineseTextAttribute = GetCustomAttribute<ChineseTextAttribute>(fieldInfo);
                var englishTextAttribute = GetCustomAttribute<EnglishTextAttribute>(fieldInfo);
                var textGroupAttribute = GetCustomAttribute<TextGroupAttribute>(fieldInfo);

                var attributeValues = new EnumAttributeValues
                { 
                    Value=Convert.ToInt64(value),
                    Text = App.Language == Language.CN ?  chineseTextAttribute?.Text:englishTextAttribute?.Text,
                    TextGroup = App.Language == Language.CN ? textGroupAttribute?.cnText : textGroupAttribute?.enText
                };

                attributeValuesList.Add(attributeValues);
            }

            return attributeValuesList;
        }

        private static T GetCustomAttribute<T>(FieldInfo fieldInfo) where T : Attribute
        {
            return (T)Attribute.GetCustomAttribute(fieldInfo, typeof(T));
        }

        /// <summary>
        /// Represents the values of an enum attribute.
        /// </summary>
        public class EnumAttributeValues
        {
            /// <summary>
            /// Gets or sets the Chinese text.
            /// </summary>
            public string? Text { get; set; } 

            /// <summary>
            /// Gets or sets the text group.
            /// </summary>
            public string? TextGroup { get; set; }
            /// <summary>
            /// Enum value
            /// </summary>

            public long Value { get;   set; }
        }
    }
}
