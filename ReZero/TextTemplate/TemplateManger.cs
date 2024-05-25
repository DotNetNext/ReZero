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
            return output.ToString();
        }
    }
}