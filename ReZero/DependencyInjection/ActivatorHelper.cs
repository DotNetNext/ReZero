using Newtonsoft.Json.Linq;
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
        internal static object CreateInstance(Type? classType, bool nonPublic)
        {
            var isIoc = false;
            foreach (var item in classType!.GetConstructors())
            {
                if (item.GetParameters().Any(it => it.ParameterType.GetInterfaces().Any(i => typeof(IDependencyInjection) == i)))
                {
                    isIoc = true; break;
                }
            }
            if (isIoc)
            {
                return DependencyResolver.Provider!.GetService(classType);
            }
            else
            {
                // If the class has no parameters in the constructor, directly instantiate the object
                return Activator.CreateInstance(classType, nonPublic);
            }
        }
    }
}
