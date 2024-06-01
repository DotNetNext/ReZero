using ReZero.Common;
using ReZero.Excel;
using ReZero.TextTemplate;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ReZero.SuperAPI
{
    public partial class MethodApi
    {
        #region ExecTemplateByTableIds
        public string ExecTemplateByTableIds(long databaseId, long[] tableIds, long templateId,string url)
        {
            List<ExcelData> datatables = new List<ExcelData>();
            var db = App.Db;
            List<ZeroEntityInfo> datas = GetZeroEntities(databaseId, tableIds, db);
            var template = App.Db.Queryable<ZeroTemplate>().First(it => it.Id == templateId);
            var outUrl = string.Empty;
            foreach (var item in datas)
            {
                outUrl=CreateFile(databaseId, template, item, url);
            }
            var result = Directory.GetParent(outUrl).FullName;
            return result;
        }
        private string CreateFile(long databaseId, ZeroTemplate template, ZeroEntityInfo item,string url)
        { 
            var  classString = GetClassString(databaseId, template, item,out TemplateEntitiesGen templateEntitiesGen);
            url = GetUrl(url,templateEntitiesGen);
            if (url.Contains(PubConst.Common_Project)) 
            { 
                var baseDir = AppContext.BaseDirectory;
                var findDir = DirectoryHelper.FindParentDirectoryWithSlnFile(baseDir);
                if (!string.IsNullOrEmpty(findDir))
                {
                    url= Regex.Replace(url,PubConst.Common_ProjectRegex,string.Empty,RegexOptions.IgnoreCase).TrimStart('/').TrimStart('\\');
                    url = Path.Combine(findDir,url);
                }
                else 
                {
                    throw new Exception(TextHandler.GetCommonText("没有找到 项目sln文件,可以使完整物理路径", "No project sln file found that can make the full physical path"));
                }
            }
            FileSugar.CreateFileReplace(url, classString,Encoding.UTF8);
            return url;
        }

        private string GetClassString(long databaseId, ZeroTemplate template, ZeroEntityInfo item,out TemplateEntitiesGen templateEntitiesGen)
        {
            var propertyGens = new List<TemplatePropertyGen>();
            templateEntitiesGen = new TemplateEntitiesGen()
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
            return classString;
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
                Description = zeroEntityColumn.Description?.Replace(PubConst.Common_N, PubConst.Common_BlankSpace)?.Replace(PubConst.Common_R, PubConst.Common_BlankSpace),
                IsIdentity = zeroEntityColumn.IsIdentity,
                IsNullable = zeroEntityColumn.IsNullable,
                IsPrimaryKey = zeroEntityColumn.IsPrimarykey,
                PropertyName = zeroEntityColumn.PropertyName,
                PropertyType = EntityGeneratorManager.GetTypeByNativeTypes(zeroEntityColumn.PropertyType).Name,
                IsJson = zeroEntityColumn.IsJson,
                IsIgnore = zeroEntityColumn.PropertyType == NativeType.IsIgnore
            };
            ProcessingPropertyDefault(zeroEntityColumn, templatePropertyGen);
            ProcessingPropertyByDbColumn(dbColumn, templatePropertyGen);
            propertyGens.Add(templatePropertyGen);
        }

        private static void ProcessingPropertyByDbColumn(SqlSugar.DbColumnInfo dbColumn, TemplatePropertyGen templatePropertyGen)
        {
            if (dbColumn != null)
            {
                templatePropertyGen.DbType = string.IsNullOrEmpty(dbColumn.OracleDataType) ? dbColumn.DataType : dbColumn.OracleDataType;
                templatePropertyGen.DecimalDigits = dbColumn.DecimalDigits;
                templatePropertyGen.Length = dbColumn.Length;
                templatePropertyGen.IsNullable = dbColumn.IsNullable;
            }
        } 
        private static void ProcessingPropertyDefault(ZeroEntityColumnInfo zeroEntityColumn, TemplatePropertyGen templatePropertyGen)
        {
            templatePropertyGen.PropertyType = EntityGeneratorManager.GetNativeTypeName(templatePropertyGen.PropertyType!);
            templatePropertyGen.PropertyType = templatePropertyGen.PropertyType + (zeroEntityColumn.IsNullable ? PubConst.Common_Q : string.Empty);
        }


        private static string GetUrl(string url, TemplateEntitiesGen templateEntitiesGen)
        {
            url = url.Replace(PubConst.Common_Format0, templateEntitiesGen.ClassName).Replace(PubConst.Common_Format1, templateEntitiesGen.TableName);
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
