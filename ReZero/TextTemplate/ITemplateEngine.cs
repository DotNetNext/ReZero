using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.TextTemplate
{

    public interface ITemplateEngine: IRender
    { 
        void AddDirective(string name, IDirective directive);
    }
    public interface IRender
    {
        void Render(string template, object data, StringBuilder output);
    }
}
