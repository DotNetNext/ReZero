using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.TextTemplate
{
    public interface IDirective
    {
        string Execute(string input, object data, ITemplateEngine templateEngine);
    }

}
