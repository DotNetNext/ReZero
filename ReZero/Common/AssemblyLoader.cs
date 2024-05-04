using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
namespace ReZero
{
    public static class AssemblyExtensions
    {
        public static Assembly[] GetAllDependentAssemblies(this Assembly rootAssembly, Func<string, bool> whereFunc)
        {
            var result= AssemblyLoader.GetAllDependentAssemblies(rootAssembly, whereFunc);
            return result;
        }
    }
    public class AssemblyLoader
    {
        public static Assembly[] GetAllDependentAssemblies(Assembly rootAssembly,Func<string,bool> whereFunc)
        {
            var visited = new HashSet<Assembly>();
            var assemblies = new List<Assembly>();
            CollectDependentAssemblies(rootAssembly, assemblies, visited, whereFunc);
            return assemblies.ToArray();
        }

        private static void CollectDependentAssemblies(Assembly assembly, List<Assembly> assemblies, HashSet<Assembly> visited, Func<string, bool> whereFunc)
        {
            if (visited.Contains(assembly)) return;

            visited.Add(assembly);
            assemblies.Add(assembly);

            foreach (var referencedAssemblyName in assembly.GetReferencedAssemblies())
            {
                try
                {
                    if (!whereFunc(referencedAssemblyName.FullName)) 
                    {
                        continue;
                    }
                    Assembly referencedAssembly = Assembly.Load(referencedAssemblyName);
                    CollectDependentAssemblies(referencedAssembly, assemblies, visited,whereFunc);
                }
                catch (Exception)
                {
                    // Ignore assemblies that cannot be loaded  
                }
            }
        }
    }
}
