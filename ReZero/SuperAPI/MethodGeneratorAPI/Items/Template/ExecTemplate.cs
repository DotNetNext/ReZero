using ReZero.TextTemplate;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    public partial class MethodApi
    {
        public string ExecTemplate(TemplateType type, string data, string template) 
        {
            string result = string.Empty;
            switch (type)
            {
                case TemplateType.Entity:
                    result = ExecTemplateByEntity(data, template);
                    break;
                default:
                    throw new ArgumentException("Invalid template type.");
            }

            return result; 
        }

        private string ExecTemplateByEntity(string data, string template)
        {
            TemplateModel<TemplateEntitiesGen> model = new SerializeService().DeserializeObject<TemplateModel<TemplateEntitiesGen>>(data);
            var temp = new TextTemplateManager().RenderTemplate(template, model);
            return temp.ToString();
        }
    }
}
