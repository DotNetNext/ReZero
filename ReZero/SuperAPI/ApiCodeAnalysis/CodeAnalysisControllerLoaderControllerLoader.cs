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
    public class CodeAnalysisControllerLoader
    {
        public void LoadController(ZeroInterfaceList zeroInterface)
        {
            var codeText=zeroInterface!.DataModel!.DefaultParameters.FirstOrDefault(it => it.Name == "codeText");
            var syntaxTree = CSharpSyntaxTree.ParseText("");
            var assemblyName = Path.GetRandomFileName();

            // 收集已加载的程序集
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.IsDynamic && !string.IsNullOrWhiteSpace(a.Location))
                .ToList();

            // 加载依赖项（包括NUGET包DLL）
            var dependencyPaths = loadedAssemblies
                .Select(a => Path.GetDirectoryName(a.Location))
                .Distinct()
                .Where(p => !string.IsNullOrEmpty(p))
                .ToList();

            // 递归查找所有DLL文件
            var allDllFiles = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var dir in dependencyPaths)
            {
                foreach (var dll in Directory.GetFiles(dir, "*.dll", SearchOption.AllDirectories))
                {
                    allDllFiles.Add(dll);
                }
            }

            // 加载所有DLL为Assembly
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
                    // 忽略加载失败的DLL
                }
            }

            foreach (var item in allAssemblies)
            {
                if (!string.IsNullOrEmpty(item.FullName) && item.FullName.Contains("SqlSugar"))
                {
                    Console.WriteLine(item.FullName);
                }
            }
            var references = allAssemblies
                .Where(a => !string.IsNullOrEmpty(a.Location))
                .Select(a => MetadataReference.CreateFromFile(a.Location))
                .Cast<MetadataReference>();

            var compilation = CSharpCompilation.Create(assemblyName,
                new[] { syntaxTree },
                references,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using var ms = new MemoryStream();
            var result = compilation.Emit(ms);

            if (!result.Success)
            {
                throw new Exception("编译失败: " + string.Join("\n", result.Diagnostics));
            }

            ms.Seek(0, SeekOrigin.Begin);
            var assembly = Assembly.Load(ms.ToArray());

            var partManager = DependencyResolver.GetRequiredService<ApplicationPartManager>();
            partManager.ApplicationParts.Add(new AssemblyPart(assembly));

            CodeAnalysisControllerLoaderActionDescriptorChangeProvider.Instance.NotifyChanges();
        }
    } 
}
