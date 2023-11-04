using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ReZero 
{
    internal class PubMethod
    {
        public static List<Type> GetTypesDerivedFromDbBase(Type baseType)
        {
            Assembly assembly= baseType.Assembly;
            List<Type> derivedTypes = new List<Type>();

            Type[] types = assembly.GetTypes();
            foreach (Type type in types)
            {
                if (type.IsSubclassOf(baseType))
                {
                    derivedTypes.Add(type);
                }
            }

            return derivedTypes;
        }
    }
}
