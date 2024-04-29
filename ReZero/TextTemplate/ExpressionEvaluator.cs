using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Text;

namespace ReZero.TextTemplate
{
    public class ExpressionEvaluator
    {
        public object Evaluate(string expression)
        {
            return DynamicExpressionParser.ParseLambda(new ParsingConfig() { }, typeof(object), expression, new object[] { }).Compile().DynamicInvoke();
        }
    }
}
