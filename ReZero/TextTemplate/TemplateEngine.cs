using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.TextTemplate
{
    public class TemplateEngine : ITemplateEngine
    {
        private readonly Dictionary<string, IDirective> directives = new Dictionary<string, IDirective>();

        public void AddDirective(string name, IDirective directive)
        {
            directives[name] = directive;
        }

        public void Render(string template, object data, StringBuilder output)
        {
            foreach (var directive in directives)
            {
                template = directive.Value.Execute(template, data, this);
            }
            output.Append(template);
        }
    }
}
