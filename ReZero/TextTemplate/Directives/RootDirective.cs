using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ReZero.TextTemplate 
{
    public class RootDirective : IDirective
    {
        public string Execute(string input, object data, ITemplateEngine templateEngine)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("string result = ");
            sb.Append("@\"");
            sb.Append(input);
            sb.Append("\";");
            return sb.ToString();
        } 
    }
}
