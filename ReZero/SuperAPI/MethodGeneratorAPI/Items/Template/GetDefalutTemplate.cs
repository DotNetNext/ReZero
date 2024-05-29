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
           return "using System;\r\nusing System.Collections.Generic;\r\nusing System.Linq;\r\nusing SqlSugar;\r\nnamespace Models\r\n{\r\n    /// <summary>\r\n    /// {{(Model.Description+\"\").Replace(\"\\r\",\"\").Replace(\"\\n\",\"\")}}\r\n    ///</summary>\r\n    [SugarTable(\"{{Model.TableName}}\")]\r\n    public class {{Model.ClassName}}\r\n    {\r\n        \r\n<% foreach (var item in Model.PropertyGens)\r\n   {\r\n    var isPrimaryKey = item.IsPrimaryKey ? \",IsPrimaryKey = true\" : \"\";\r\n    var isIdentity = item.IsIdentity ? \",IsIdentity = true\" : \"\";\r\n    var isNull=(item.IsNullable&&item.Type!=\"string\"&&item.IsSpecialType==false&&item.Type!=\"byte[]\")?\"?\":\"\";\r\n    var isIgnore=(item.IsIgnore?\",IsIgnore = true\":\"\");\r\n    var isJson=(item.CodeType.StartsWith(\"json\")?\",IsJson= true\":\"\");\r\n\r\n    var newPropertyName=item.PropertyName; //这里可以用C#处理 实体属性的显式格式\r\n    //想和数据库一样就用 newPropertyName=item.DbColumnName\r\n    if(System.Text.RegularExpressions.Regex.IsMatch(newPropertyName.Substring(0,1), \"[0-9]\"))\r\n    {\r\n        newPropertyName=\"_\"+newPropertyName;//处理属性名开头为数字情况\r\n    }\r\n    if(newPropertyName==Model.ClassName)\r\n    {\r\n        newPropertyName=\"_\"+newPropertyName;//处理属性名不能等于类名\r\n    }\r\n\r\n\r\n    var desc=(item.Description+\"\").Replace(\"\\r\",\"\").Replace(\"\\n\",\"\");//处理换行\r\n\r\n    if(isIgnore!=\"\")\r\n    {\r\n       isPrimaryKey =isIdentity =isNull=\"\";\r\n    }\r\n     \r\n%>\r\n        /// <summary>\r\n        /// {{desc}}\r\n<%      if(item.DefaultValue!=null)\r\n        {   %>\r\n        /// 默认值:{{item.DefaultValue}}\r\n<%      }  %>\r\n        ///</summary>\r\n        [SugarColumn(ColumnName=\"{{item.DbColumnName}} {{isPrimaryKey}} {{isIdentity}}  {{isIgnore}} {{isJson}}]\r\n        public {{item.Type}}{{isNull}} {{newPropertyName}} { get; set; }\r\n<%         \r\n   }\r\n    }\r\n} %>\r\n";
        } 
    }
}
