using Newtonsoft.Json.Linq;
using ReZero.SuperAPI; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ReZero.DependencyInjection
{
    public class ActivatorHelper
    {
        /// <summary>
        /// Create an instance of the specified class type.
        /// </summary>
        /// <param name="classType">The type of the class to create an instance of.</param>
        /// <param name="nonPublic">Specifies whether to include non-public constructors.</param>
        /// <returns>The created instance of the class.</returns>
        internal static object CreateInstance(Type classType, bool nonPublic)
        {
            if (classType.GetCustomAttribute<ApiAttribute>()!=null)
            {
                var p = DependencyResolver.Provider;
                var result= p!.GetService(classType);
                var diProperties = classType.GetProperties().Where(it => it.GetCustomAttribute<DIAttribute>() != null);
                foreach (var item in diProperties)
                {
                    item.SetValue(result, p!.GetService(item.PropertyType));
                } 
                return result;
            }
            else
            {
                // If the class has no parameters in the constructor, directly instantiate the object
                return Activator.CreateInstance(classType, nonPublic);
            }
        }
    }
}
