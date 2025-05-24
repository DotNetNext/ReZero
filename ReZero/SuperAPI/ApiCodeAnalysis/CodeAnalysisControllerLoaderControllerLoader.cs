 using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.Primitives;
using SqlSugar;
using System.Collections.Generic;
using System.IO;
using System;
using System.Reflection;
using System.Threading;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using ReZero.SuperAPI;
using ReZero;
using ReZero.DependencyInjection;
namespace ReZero.SuperAPI
{
    /// <summary>
    /// Utility class for dynamically loading, removing, and updating Controllers, supporting runtime compilation and assembly management.
    /// </summary>
    public class CodeAnalysisControllerLoader
    {
        /// <summary>
        /// Dynamically compiles and loads a Controller into the application.
        /// </summary>
        /// <param name="zeroInterface">Interface definition object containing code text and other information.</param>
        public void LoadController(ZeroInterfaceList zeroInterface)
        {
            // Get code text parameter
            var codeText = zeroInterface!.DataModel!.DefaultParameters.FirstOrDefault(it => it.Name == "codeText");
            // Parse code text into syntax tree (currently an empty string)
            var syntaxTree = CSharpSyntaxTree.ParseText("");
            // Generate a unique assembly name
            var assemblyName = GenerateAssemblyName(zeroInterface);

            // Collect loaded assemblies
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.IsDynamic && !string.IsNullOrWhiteSpace(a.Location))
                .ToList();

            // Load dependencies (including NUGET package DLLs)
            var dependencyPaths = loadedAssemblies
                .Select(a => Path.GetDirectoryName(a.Location))
                .Distinct()
                .Where(p => !string.IsNullOrEmpty(p))
                .ToList();

            // Recursively find all DLL files
            var allDllFiles = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var dir in dependencyPaths)
            {
                foreach (var dll in Directory.GetFiles(dir, "*.dll", SearchOption.AllDirectories))
                {
                    allDllFiles.Add(dll);
                }
            }

            // Load all DLLs as Assembly
            var allAssemblies = new List<Assembly>(loadedAssemblies);
            foreach (var dllPath in allDllFiles)
            {
                try
                {
                    var fileName = Path.GetFileNameWithoutExtension(dllPath);
                    if (!allAssemblies.Any(a => Path.GetFileNameWithoutExtension(a.Location).Equals(fileName, StringComparison.OrdinalIgnoreCase)))
                    {
                        var asm = Assembly.LoadFrom(dllPath);
                        allAssemblies.Add(asm);
                    }
                }
                catch
                {
                    // Ignore DLLs that fail to load
                }
            }

            // Output assembly names containing SqlSugar
            foreach (var item in allAssemblies)
            {
                if (!string.IsNullOrEmpty(item.FullName) && item.FullName.Contains("SqlSugar"))
                {
                    Console.WriteLine(item.FullName);
                }
            }

            // Build references required for compilation
            var references = allAssemblies
                .Where(a => !string.IsNullOrEmpty(a.Location))
                .Select(a => MetadataReference.CreateFromFile(a.Location))
                .Cast<MetadataReference>();

            // Create compilation object
            var compilation = CSharpCompilation.Create(assemblyName,
                new[] { syntaxTree },
                references,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            // Compile code and generate assembly
            using var ms = new MemoryStream();
            var result = compilation.Emit(ms);

            if (!result.Success)
            {
                throw new Exception("Compilation failed: " + string.Join("\n", result.Diagnostics));
            }

            ms.Seek(0, SeekOrigin.Begin);
            var assembly = Assembly.Load(ms.ToArray());

            // Add new assembly to ApplicationPartManager
            var partManager = DependencyResolver.GetRequiredService<ApplicationPartManager>();
            partManager.ApplicationParts.Add(new AssemblyPart(assembly));

            // Notify MVC framework to refresh Action descriptors
            CodeAnalysisControllerLoaderActionDescriptorChangeProvider.Instance.NotifyChanges();
        }

        public static string GenerateAssemblyName(ZeroInterfaceList zeroInterface)
        {
            return nameof(CodeAnalysisControllerLoader) + zeroInterface.Id;
        }

        /// <summary>
        /// Removes a loaded Controller.
        /// </summary>
        /// <param name="zeroInterface">Interface definition object.</param>
        public void RemoveController(ZeroInterfaceList zeroInterface)
        {
            var assemblyName = nameof(CodeAnalysisControllerLoader) + zeroInterface.Id;
            var partManager = DependencyResolver.GetRequiredService<ApplicationPartManager>();

            // Find and remove the corresponding AssemblyPart
            var partToRemove = partManager.ApplicationParts
                .OfType<AssemblyPart>()
                .FirstOrDefault(p => p.Assembly.GetName().Name == assemblyName);

            if (partToRemove != null)
            {
                partManager.ApplicationParts.Remove(partToRemove);
                CodeAnalysisControllerLoaderActionDescriptorChangeProvider.Instance.NotifyChanges();
            }
        }

        /// <summary>
        /// Updates a Controller (removes the old one and then loads the new one).
        /// </summary>
        /// <param name="zeroInterface">Interface definition object.</param>
        public void UpdateController(ZeroInterfaceList zeroInterface)
        {
            // Remove the old Controller first
            RemoveController(zeroInterface);
            // Then load the new Controller
            LoadController(zeroInterface);
        }
    }
}
