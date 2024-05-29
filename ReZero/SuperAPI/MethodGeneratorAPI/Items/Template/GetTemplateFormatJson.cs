using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    public partial class MethodApi
    {
        public object GetTemplateFormatJson(TemplateType type)
        {
            object result = new object(); 
            switch (type)
            {
                case TemplateType.Entity:
                    result = GenerateClassNameTemplate();
                    break; 
                default:
                    throw new ArgumentException("Invalid template type.");
            }

            return result;
        }

        private object GenerateClassNameTemplate()
        {
            TemplateEntitiesGen templateEntitiesGen = new TemplateEntitiesGen()
            {
                ClassName = "ClassName",
                TableName = "TableName",
                Description = "表备注",
                PropertyGens = new List<TemplatePropertyGen>()
                    {
                        new TemplatePropertyGen()
                        {
                            DbColumnName="Id",
                            PropertyName="PId",
                            IsIdentity=true,
                            IsPrimaryKey=true,
                            IsNullable=false,
                            Description="序号"
                        },
                        new TemplatePropertyGen()
                        {
                            DbColumnName="Name",
                            PropertyName="PName",
                            IsNullable=false,
                            Description="名称"
                        }
                    }
            };

            return templateEntitiesGen;
        } 
    }
}
