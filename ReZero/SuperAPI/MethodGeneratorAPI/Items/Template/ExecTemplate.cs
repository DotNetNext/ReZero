using ReZero.Excel;
using ReZero.TextTemplate;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;

namespace ReZero.SuperAPI
{
    public partial class MethodApi
    {
        #region ExecTemplateByTableIds
        public bool ExecTemplateByTableIds(long databaseId, long[] tableIds, long templateId,string url)
        {
            List<ExcelData> datatables = new List<ExcelData>();
            var db = App.Db;
            List<ZeroEntityInfo> datas = GetZeroEntities(databaseId, tableIds, db);
            var template = App.Db.Queryable<ZeroTemplate>().First(it => it.Id == templateId);
            foreach (var item in datas)
            {
                CreateFile(databaseId, template, item, url);
            }
            return true;
        }
        private void CreateFile(long databaseId, ZeroTemplate template, ZeroEntityInfo item,string url)
        {
            TemplateEntitiesGen templateEntitiesGen = GetClassString(databaseId, template, item);
            url = GetUrl(url, templateEntitiesGen);
        }

        private TemplateEntitiesGen GetClassString(long databaseId, ZeroTemplate template, ZeroEntityInfo item)
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
            foreach (var zeroEntityColumn in item.ZeroEntityColumnInfos!)
            {
                AddProperty(propertyGens, columnInfos, zeroEntityColumn);
            }
            var classString = ExecTemplate(TemplateType.Entity, new SerializeService().SerializeObject(templateEntitiesGen), template.TemplateContent!);
            return templateEntitiesGen;
        }
        private static void AddProperty(List<TemplatePropertyGen> propertyGens, List<SqlSugar.DbColumnInfo> columnInfos, ZeroEntityColumnInfo zeroEntityColumn)
        {
            var dbColumn = columnInfos.FirstOrDefault(it => it.DbColumnName!.EqualsCase(zeroEntityColumn.DbColumnName!));
            TemplatePropertyGen templatePropertyGen = new TemplatePropertyGen()
            {
                DbColumnName = zeroEntityColumn.DbColumnName,
                DbType = zeroEntityColumn.DataType,
                DecimalDigits = zeroEntityColumn.DecimalDigits,
                DefaultValue = "",
                Description = zeroEntityColumn.Description,
                IsIdentity = zeroEntityColumn.IsIdentity,
                IsNullable = zeroEntityColumn.IsNullable,
                IsPrimaryKey = zeroEntityColumn.IsPrimarykey,
                PropertyName = zeroEntityColumn.PropertyName,
                PropertyType = EntityGeneratorManager.GetTypeByNativeTypes(zeroEntityColumn.PropertyType).Name + (zeroEntityColumn.IsNullable ? "?" : string.Empty),
                IsJson = zeroEntityColumn.IsJson,
                IsIgnore = zeroEntityColumn.PropertyType == NativeType.IsIgnore
            };
            if (dbColumn != null)
            {
                templatePropertyGen.DbType = string.IsNullOrEmpty(dbColumn.OracleDataType) ? dbColumn.DataType : dbColumn.OracleDataType;
                templatePropertyGen.DecimalDigits = dbColumn.DecimalDigits;
                templatePropertyGen.Length = dbColumn.Length;
                templatePropertyGen.IsNullable = dbColumn.IsNullable;
            }
            propertyGens.Add(templatePropertyGen);
        }

        private static string GetUrl(string url, TemplateEntitiesGen templateEntitiesGen)
        {
            url = url.Replace("{0}", templateEntitiesGen.ClassName).Replace("{1}", templateEntitiesGen.TableName);
            return url;
        } 
        private static List<ZeroEntityInfo> GetZeroEntities(long databaseId, long[] tableIds, ISqlSugarClient db)
        {
            return db.Queryable<ZeroEntityInfo>()
                .OrderBy(it => it.DbTableName)
                .Where(it => it.DataBaseId == databaseId)
                .WhereIF(tableIds.Any(), it => tableIds.Contains(it.Id))
                .Includes(it => it.ZeroEntityColumnInfos).ToList();
        }
        #endregion 

        #region ExecTemplate
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
            var templateModel = new TemplateModel<TemplateEntitiesGen> { Model = model };
            var temp = new TextTemplateManager().RenderTemplate(template, templateModel);
            return temp.ToString();
        } 
        #endregion
    }
}
