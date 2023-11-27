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
        public static string GetCommonTexst(string cn, string en) 
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
        public static string? GetInterfaceCategoryText(object value) 
        {
            return GetText(typeof(InterfaceCategoryInitializerProvider), value);
        }
        public static string? GetInterfaceListText(object value)
        {
            return GetText(typeof(InterfaceListInitializerProvider), value);
        }
        public static string? GetText(Type type, object value)
        {
            var language = App.Language;
            var fieldInfo=type.GetFields()
                .Where(it=>it.GetCustomAttribute<TextCN>()!=null)
                .Where(it=>it.GetValue(null)?.ToString()==value?.ToString())
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
