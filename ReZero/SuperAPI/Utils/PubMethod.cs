using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
namespace ReZero.SuperAPI
{
    /// <summary>
    /// 
    /// </summary>
    internal class PubMethod
    {
        /// <summary>
        /// Get the types derived from the base type.
        /// </summary>
        /// <param name="baseType">The base type</param>
        /// <returns>The types derived from the base type</returns>
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
 