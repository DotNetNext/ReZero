using ReZero.Excel;
using ReZero.TextTemplate;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReZero.SuperAPI
{
    public partial class MethodApi
    {
        public string ExecTemplateByTableIds(long databaseId, long[] tableIds,long tempId) 
        {
            List<ExcelData> datatables = new List<ExcelData>();
            var db = App.Db;
            var datas = db.Queryable<ZeroEntityInfo>()
                .OrderBy(it => it.DbTableName)
                .Where(it => it.DataBaseId == databaseId)
                .WhereIF(tableIds.Any(), it => tableIds.Contains(it.Id))
                .Includes(it => it.ZeroEntityColumnInfos).ToList();
            List<TemplateEntitiesGen> gens = new List<TemplateEntitiesGen>();
            var template= App.Db.Queryable<ZeroTemplate>().First(it => it.Id == tempId);
            foreach (var item in datas)
            {
                var propertyGens = new List<TemplatePropertyGen>();
                TemplateEntitiesGen templateEntitiesGen = new TemplateEntitiesGen()
                {
                    ClassName = item.ClassName,
                    Description = item.Description,
                    TableName = item.DbTableName,
                    PropertyGens = propertyGens
                }; 
                var columnInfos = App.GetDbById(databaseId)!.DbMaintenance.GetColumnInfosByTableName(item.DbTableName, false);
                foreach (var columnInfo in columnInfos)
                {
                    var zeroEntityColumn = item.ZeroEntityColumnInfos.FirstOrDefault(it=>it.DbColumnName!.EqualsCase( columnInfo.DbColumnName));
                    TemplatePropertyGen templatePropertyGen = new TemplatePropertyGen()
                    {
                         DbColumnName=columnInfo.DbColumnName,
                         DbType= string.IsNullOrEmpty(columnInfo.OracleDataType)? columnInfo.DataType: columnInfo.OracleDataType,
                         DecimalDigits=columnInfo.DecimalDigits,
                         DefaultValue=columnInfo.DefaultValue,
                         Description=columnInfo.ColumnDescription,
                         IsIdentity=columnInfo.IsIdentity,
                         IsNullable=columnInfo.IsNullable,
                         IsPrimaryKey=columnInfo.IsPrimarykey, 
                         PropertyName= zeroEntityColumn.PropertyName,
                         PropertyType=EntityGeneratorManager.GetTypeByNativeTypes(zeroEntityColumn.PropertyType).Name+(zeroEntityColumn.IsNullable?"?":string.Empty),
                         IsJson=zeroEntityColumn.IsJson,
                         IsIgnore=zeroEntityColumn.PropertyType==NativeType.IsIgnore
                    }; 
                }
            }
            return ExecTemplate(TemplateType.Entity, new SerializeService().SerializeObject(gens), template.TemplateContent!);
        }

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
            var model = new SerializeService().DeserializeObject<TemplateEntitiesGen>(data);
            var templateModel=new  TemplateModel<TemplateEntitiesGen> { Model = model };
            var temp = new TextTemplateManager().RenderTemplate(template, templateModel);
            return temp.ToString();
        }
    }
}
