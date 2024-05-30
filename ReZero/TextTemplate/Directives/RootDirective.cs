using SqlSugar;
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
            input = Regex.Replace(input, @"\{\ {1,5}\{", "{{");
            input = Regex.Replace(input, @"\} {1,5}\}", "}}");
            input = Regex.Replace(input, @"\<\ {1,5}\%", "<%");
            input = Regex.Replace(input, @"\% {1,5}\>", "%>");
            input = Regex.Replace(input,  "\"{{", "\"\"{{");
            input = Regex.Replace(input, "}}\"", "}}\"\"");
            StringBuilder sb = new StringBuilder();
            sb.Append("string result = ");
            sb.Append("@\"");
            sb.Append(input);
            sb.Append("\";");
            return sb.ToString();
        } 
    }
}
