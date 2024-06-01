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
                Description =TextHandler.GetCommonText( "表备注", "Table description"),
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
                            Description=TextHandler.GetCommonText("序号","No")
                        },
                        new TemplatePropertyGen()
                        {
                            DbColumnName="Name",
                            PropertyName="PName",
                            PropertyType="string",
                            IsNullable=false,
                            Description=TextHandler.GetCommonText( "名称","Name")
                        },
                         new TemplatePropertyGen()
                        {
                            DbColumnName="Price",
                            PropertyName="PPrice",
                            PropertyType="decimal?",
                            IsNullable=true,
                            Description=TextHandler.GetCommonText( "价格","Price")
                        }
                    }
            };

            return templateEntitiesGen;
        } 
    }
}
