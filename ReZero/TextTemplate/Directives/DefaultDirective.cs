using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ReZero.TextTemplate 
{
    public class DefaultDirective : IDirective
    {
        public string Execute(string input, object data, ITemplateEngine templateEngine)
        {
            string pattern = @"<%([\s\S]*?)%>";
            MatchCollection matches = Regex.Matches(input, pattern);
            foreach (Match match in matches)
            {
                string expression = match.Groups[1].Value;
                string value = Evaluate(expression).ToString();
                input = input.Replace(match.Value, value);
            }
            return input;
        }

        private string Evaluate(string expression)
        {
            expression = $"\";\r\n{expression}\r\nresult+=@\"";
            return expression;
        }
    }
}
