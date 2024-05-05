using ReZero.SuperAPI.ApiDynamic.Entities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
namespace ReZero.SuperAPI 
{
    public class AttibuteInterfaceInitializerService
    {
        /// <summary>
        /// Get the list of types with the DynamicApiAttribute
        /// </summary>
        /// <param name="types">The list of types</param>
        /// <returns>The list of types with the DynamicApiAttribute</returns>
        public static List<Type> GetTypesWithDynamicApiAttribute(List<Type> types)
        {
            List<Type> typesWithDynamicApiAttribute = new List<Type>();

            foreach (var type in types)
            {
                // Check if the type has the DynamicApiAttribute
                if (type.GetCustomAttributes(typeof(DynamicApiAttribute), true).Length > 0)
                {
                    typesWithDynamicApiAttribute.Add(type);
                }
            }

            return typesWithDynamicApiAttribute;
        }


        /// <summary>
        /// Get the list of methods with the DynamicMethodAttribute for a given type
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>The list of methods with the DynamicMethodAttribute</returns>
        public static List<MethodInfo> GetMethodsWithDynamicMethodAttribute(Type type)
        {
            List<MethodInfo> methodsWithDynamicMethodAttribute = new List<MethodInfo>();

            MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var method in methods)
            {
                if (method.GetCustomAttributes(typeof(DynamicMethodAttribute), true).Length > 0)
                {
                    methodsWithDynamicMethodAttribute.Add(method);
                }
            }

            return methodsWithDynamicMethodAttribute;
        }

        internal static ZeroInterfaceList GetZeroInterfaceItem(Type type, MethodInfo method)
        {
            ZeroInterfaceList result = new ZeroInterfaceList();
            return result;
        } 
        public static void InitDynamicAttributeApi(List<Type>? types)
        {
            types = AttibuteInterfaceInitializerService.GetTypesWithDynamicApiAttribute(types ?? new List<Type>());
            List<ZeroInterfaceList> zeroInterfaceLists = new List<ZeroInterfaceList>();
            foreach (var type in types)
            {
                var methods = AttibuteInterfaceInitializerService.GetMethodsWithDynamicMethodAttribute(type);
                if (methods.Any())
                {
                    foreach (var method in methods)
                    {
                        var addItem = AttibuteInterfaceInitializerService.GetZeroInterfaceItem(type, method);
                        zeroInterfaceLists.Add(addItem);
                    }
                }
            }
            App.Db.Insertable(zeroInterfaceLists).ExecuteCommand();
        }
    }
}
