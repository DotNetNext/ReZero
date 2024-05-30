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

        private string ClassNameDefalutTemplateTemplate()
        {
           return "using System;\r\nusing System.Collections.Generic;\r\nusing System.Linq;\r\nusing SqlSugar;\r\nnamespace Models\r\n{\r\n    /// <summary>\r\n    /// {{(Model.Description+\"\").Replace(\"\\r\",\"\").Replace(\"\\n\",\"\")}}\r\n    ///</summary>\r\n    [SugarTable(\"{{Model.TableName}}\")]\r\n    public class {{Model.ClassName}}\r\n    {\r\n        \r\n    <% foreach (var item in Model.PropertyGens) { %> \r\n        /// <summary>\r\n        /// 备  注:{{item.Description}}\r\n        /// 默认值:{{item.DefaultValue}}\r\n        ///</summary>\r\n        [SugarColumn(ColumnName=\"{{item.DbColumnName}}]\r\n        public {{item.PropertyType}} {{item.PropertyName}} { get; set; } \r\n    <%} %>\r\n\r\n    }\r\n    \r\n}\r\n";
        } 
    }
}
