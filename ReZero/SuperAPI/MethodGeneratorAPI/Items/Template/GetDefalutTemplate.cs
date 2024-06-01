using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    public partial class MethodApi
    {
        public string GetDefalutTemplate(TemplateType type)
        {
            string result = string.Empty; 
            switch (type)
            {
                case TemplateType.Entity:
                    result =ClassNameDefalutTemplateTemplate();
                    break; 
                default:
                    throw new ArgumentException("Invalid template type.");
            }

            return result;
        }

        public string ClassNameDefalutTemplateTemplate()
        {
           return "using System;\r\nusing System.Collections.Generic;\r\nusing System.Linq;\r\nusing SqlSugar;\r\nnamespace Models\r\n{\r\n    /// <summary>\r\n    /// {{(Model.Description+\"\").Replace(\"\\r\",\"\").Replace(\"\\n\",\"\")}}\r\n    ///</summary>\r\n    [SugarTable(\"{{Model.TableName}}\")]\r\n    public class {{Model.ClassName}}\r\n    {\r\n        \r\n    <% foreach (var item in Model.PropertyGens) {  \r\n       \r\n              var isPrimaryKey = item.IsPrimaryKey ? \",IsPrimaryKey = true\" : \"\";\r\n              var isIdentity = item.IsIdentity ? \",IsIdentity = true\" : \"\"; \r\n              var isIgnore=(item.IsIgnore?\",IsIgnore = true\":\"\");\r\n              var isJson=item.IsJson?\",IsJson= true\":\"\" ; \r\n              var stringValue=item.PropertyType==\"string\"?\"= null!;\":\"\";//C#低版本改模版\r\n    %> \r\n        /// <summary>\r\n        /// 备  注:{{item.Description}}\r\n        /// 默认值:{{item.DefaultValue}}\r\n        ///</summary>\r\n        [SugarColumn(ColumnName=\"{{item.DbColumnName}}\" {{isPrimaryKey+isIdentity+isIgnore+isJson}}) ]\r\n        public {{item.PropertyType}} {{item.PropertyName}}  { get; set;  } {{stringValue}}\r\n    <%} %>\r\n\r\n    }\r\n    \r\n}";
        } 
    }
}
