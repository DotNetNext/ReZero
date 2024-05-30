using ReZero.TextTemplate;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    public partial class MethodApi
    {
        public string ExecTemplate(TemplateType type, string templateJson,string resultJson) 
        {
            string result = string.Empty;
            switch (type)
            {
                case TemplateType.Entity:
                    result = ExecTemplateByEntity(templateJson,resultJson);
                    break;
                default:
                    throw new ArgumentException("Invalid template type.");
            }

            return result; 
        }

        private string ExecTemplateByEntity(string templateJson, string resultJson)
        {
            TemplateModel<TemplateEntitiesGen> model = new SerializeService().DeserializeObject<TemplateModel<TemplateEntitiesGen>>(resultJson);
            var temp = new TextTemplateManager().RenderTemplate(templateJson, model);
            return temp.ToString();
        }
    }
}
