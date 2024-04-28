using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ReZero.TextTemplate 
{
    public class IfDirective : IDirective
    {
        public string Execute(string input, object data, ITemplateEngine templateEngine)
        {
            var regex = new Regex(@"\<div v-if=""([^""]*)"">");
            return regex.Replace(input, match =>
            {
                var condition = match.Groups[1].Value;
                var conditionResult = EvaluateCondition(condition, data);
                return conditionResult ? match.Value.Replace(match.Groups[0].Value, "") : "";
            });
        }

        private bool EvaluateCondition(string condition, object data)
        {
            var parts = condition.Split(new[] { "==", "!=", "<", ">", "<=", ">=" }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2)
            {
                // If there's only one part, consider it as valid if it's not empty
                return !string.IsNullOrEmpty(condition);
            }

            if (parts.Length != 2)
            {
                throw new ArgumentException("Invalid condition format");
            }

            var left = parts[0].Trim();
            var right = parts[1].Trim();

            var value = GetDataValue(left, data);
            var expectedValue = GetDataValue(right, data);

            return value?.Equals(expectedValue) ?? false;
        }

        private object GetDataValue(string key, object data)
        {
            var property = data.GetType().GetProperty(key);
            return property?.GetValue(data);
        }
    }
}
