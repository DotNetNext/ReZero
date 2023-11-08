using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace ReZero 
{
    internal class TextHandler
    {
        public string GetText(Type type, object value)
        {
            var language = App.Language;
            var fields=type.GetFields().Where(it=>it.GetCustomAttribute<TextCN>()!=null).ToList();
            return "";
        }

       
    }
}
