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
                ClassName = "ClassName01",
                TableName = "TableName01",
                Description = "表备注",
                PropertyGens = new List<TemplatePropertyGen>()
                    {
                        new TemplatePropertyGen()
                        {
                            DbColumnName="Id",
                            PropertyName="PId",
                            PropertyType="int",
                            IsIdentity=true,
                            IsPrimaryKey=true,
                            IsNullable=false,
                            Description="序号"
                        },
                        new TemplatePropertyGen()
                        {
                            DbColumnName="Name",
                            PropertyName="PName",
                            PropertyType="string",
                            IsNullable=false,
                            Description="名称"
                        },
                         new TemplatePropertyGen()
                        {
                            DbColumnName="Price",
                            PropertyName="PPrice",
                            PropertyType="decimal?",
                            IsNullable=true,
                            Description="价格"
                        }
                    }
            };

            return templateEntitiesGen;
        } 
    }
}
