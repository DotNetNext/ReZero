using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ReZero.TextTemplate 
{
    public class ForDirective : IDirective
    {
        public string Execute(string input, object data, ITemplateEngine templateEngine)
        {
            var regex = new Regex(@"\<\w+\s+v-for=""([^""]*)"">");
            return regex.Replace(input, match =>
            {
                var expression = match.Groups[1].Value;
                var parts = expression.Split(" in ");
                if (parts.Length != 2)
                {
                    throw new ArgumentException("Invalid v-for expression");
                }

                var itemName = parts[0].Trim();
                var collectionName = parts[1].Trim();

                var collection = GetDataValue(collectionName, data) as IEnumerable<object>;
                if (collection == null)
                {
                    throw new ArgumentException($"Invalid collection name: {collectionName}");
                }

                var output = new StringBuilder();
                foreach (var item in collection)
                {
                    if (item is string || item.GetType().IsValueType)
                    {
                        // For simple types, directly replace the placeholder with the value
                        var replacedTemplate = ReplacePlaceholder(input, itemName, item);
                        output.Append(replacedTemplate);
                    }
                    else
                    {
                        // For complex types, render the nested template recursively
                        var nestedData = new Dictionary<string, object> { { "condition", true }, { itemName, item } };
                        templateEngine.Render(input, nestedData, output);
                    }
                }

                return output.ToString();
            });
        }
        private string ReplacePlaceholder(string template, string placeholder, object value)
        {
            return template.Replace($"{{{{{placeholder}}}}}", value.ToString());
        }
        private object GetDataValue(string key, object data)
        {
            var property = data.GetType().GetProperty(key);
            return property?.GetValue(data);
        }
    }
}
