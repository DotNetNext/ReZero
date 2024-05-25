using DocumentFormat.OpenXml.Office2016.Drawing.Command;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using ReZero.TextTemplate;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions; 
namespace ReZero.TextTemplate
{
    public class TextTemplateManager
    {
        IRender? customRenderer = null;
        public TextTemplateManager(IRender? customRenderer=null) 
        {
            this.customRenderer = customRenderer;
        }
        public  string RenderTemplate(string template, object data)
        {
            if (this.customRenderer != null)
            {
                return CustomRender(template, data, this.customRenderer);
            }
            else
            { 
                return DefaultRender(template, data);
            }
        }

        private  string CustomRender(string template, object data, IRender renderer)
        {
            var result = new StringBuilder();
            renderer.Render(template, data, result);
            return result.ToString();
        }

        private  string DefaultRender(string template, object data)
        {
            var engine = new TemplateEngine();
            engine.AddDirective("root", new RootDirective());
            engine.AddDirective("default", new DefaultDirective());
            engine.AddDirective("member", new MemberDirective());
            var output = new StringBuilder();
            engine.Render(template, data, output);
            var options = GetOptions();
            try
            {
                output.AppendLine("result");
                var result = CSharpScript.EvaluateAsync<string>(output.ToString(), options, data).GetAwaiter().GetResult();
                output.Clear();
                output.Append(result);
            }
            catch (CompilationErrorException e)
            {
                throw new Exception(string.Join(Environment.NewLine, e.Diagnostics));
            }
            return output.ToString();
        }
        private static ScriptOptions? scriptOptions;
        private static object objLock = new object();
        private static ScriptOptions GetOptions()
        {
            if (scriptOptions != null)
                return scriptOptions;
            lock (objLock)
            {
                var namespaces = new[]
                   {
                    // System命名空间
                    "System",
                    "System.Collections",
                    "System.Collections.Generic",
                    "System.IO",
                    "System.Linq",
                    "System.Text",
                    "System.Text.RegularExpressions"
                };
                var result = ScriptOptions.Default.AddReferences(AppDomain.CurrentDomain.GetAssemblies())
                                                          .WithImports(namespaces);
                scriptOptions = result;

                return result;
            }
        }
    }
}