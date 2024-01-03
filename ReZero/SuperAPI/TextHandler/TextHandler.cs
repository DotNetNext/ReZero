using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization; 

namespace ReZero.SuperAPI
{
    internal class TextHandler
    {
        /// <summary>
        /// Get the common text based on the language.
        /// </summary>
        /// <param name="cn">The Chinese text.</param>
        /// <param name="en">The English text.</param>
        /// <returns>The common text.</returns>
        public static string GetCommonText(string cn, string en)
        {
            var language = App.Language;
            switch (language)
            {
                case Language.CN:
                    return cn;
                default:
                    return en;
            }
        }

        /// <summary>
        /// Get the interface category text based on the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The interface category text.</returns>
        public static string? GetInterfaceCategoryText(object value)
        {
            return GetText(typeof(InterfaceCategoryInitializerProvider), value);
        }

        /// <summary>
        /// Get the interface list text based on the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The interface list text.</returns>
        public static string? GetInterfaceListText(object value)
        {
            return GetText(typeof(InterfaceListInitializerProvider), value);
        }

        /// <summary>
        /// Get the text based on the type and value.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        /// <returns>The text.</returns>
        public static string? GetText(Type type, object value)
        {
            var language = App.Language;
            var fieldInfo = type.GetFields()
                .Where(it => it.GetCustomAttribute<TextCN>() != null)
                .Where(it => it.GetValue(null)?.ToString() == value?.ToString())
                .FirstOrDefault();
            switch (language)
            {
                case Language.CN:
                    return fieldInfo?.GetCustomAttribute<TextCN>()?.Text;
                default:
                    return fieldInfo?.GetCustomAttribute<TextEN>()?.Text;
            }
        }
    }
}
