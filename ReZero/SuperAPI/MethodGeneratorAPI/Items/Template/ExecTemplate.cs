using DocumentFormat.OpenXml.Spreadsheet;
using ReZero.Common;
using ReZero.Excel;
using ReZero.TextTemplate;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
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
        /// <summary>
        /// Execute template generation for multiple tables.
        /// </summary>
        /// <param name="databaseId">The ID of the database.</param>
        /// <param name="tableIds">The IDs of the tables.</param>
        /// <param name="templateId">The ID of the template.</param>
        /// <param name="url">The URL of the output file.</param>
        /// <returns>The directory path of the generated files.</returns>
        public string ExecTemplateByTableIds(long databaseId, long[] tableIds, long templateId, string url,string viewName)
        {
            if (IsView(viewName))
            {
                return ExecTemplateByView(databaseId, viewName, templateId, url);
            }
            List<ExcelData> datatables = new List<ExcelData>();
            var db = App.Db;
            List<ZeroEntityInfo> datas = GetZeroEntities(databaseId, tableIds, db);
            var template = App.Db.Queryable<ZeroTemplate>().First(it => it.Id == templateId);
            var outUrl = string.Empty;
            foreach (var item in datas)
            {
                outUrl = CreateFile(databaseId, template, item, url);
            }
            var result = Directory.GetParent(outUrl).FullName;
            return result;
        }

        private string ExecTemplateByView(long databaseId, string viewName, long templateId, string url)
        {
            var db = App.Db;
            var template = App.Db.Queryable<ZeroTemplate>().First(it => it.Id == templateId);
            var item = new ZeroEntityInfo();
            var viewDb = App.GetDbById(databaseId);
            var dt=viewDb!.Queryable<object>().AS(viewName).Take(1).Select("*").ToDataTable();
            item.ClassName = viewName;
            item.DbTableName = viewName;
            item.Description = string.Empty;
            item.ZeroEntityColumnInfos = new List<ZeroEntityColumnInfo>();
            foreach (DataColumn dataColumn in dt.Columns)
            {
                item.ZeroEntityColumnInfos.Add(new ZeroEntityColumnInfo()
                {
                     PropertyName=dataColumn.ColumnName,
                     DbColumnName=dataColumn.ColumnName,
                     DataType=dataColumn.DataType.Name,
                     PropertyType=EntityGeneratorManager.GetNativeTypeByType(dataColumn.DataType)
                });
            }
            var outUrl = CreateFile(databaseId, template, item, url);
            var result = Directory.GetParent(outUrl).FullName;
            return result;
        }

        private static bool IsView(string viewName)
        {
            return !string.IsNullOrEmpty(viewName);
        }

        /// <summary>
        /// Create a file based on the template and entity information.
        /// </summary>
        /// <param name="databaseId">The ID of the database.</param>
        /// <param name="template">The template to use.</param>
        /// <param name="item">The entity information.</param>
        /// <param name="url">The URL of the output file.</param>
        /// <returns>The URL of the created file.</returns>
        private string CreateFile(long databaseId, ZeroTemplate template, ZeroEntityInfo item, string url)
        {
            var classString = GetClassString(databaseId, template, item, out TemplateEntitiesGen templateEntitiesGen);
            url = GetUrl(url, templateEntitiesGen);
            if (url.Contains(PubConst.Common_Project))
            {
                var baseDir = AppContext.BaseDirectory;
                var findDir = DirectoryHelper.FindParentDirectoryWithSlnFile(baseDir);
                if (!string.IsNullOrEmpty(findDir))
                {
                    url = Regex.Replace(url, PubConst.Common_ProjectRegex, string.Empty, RegexOptions.IgnoreCase).TrimStart('/').TrimStart('\\');
                    url = Path.Combine(findDir, url);
                }
                else
                {
                    throw new Exception(TextHandler.GetCommonText("没有找到 项目sln文件,可以使完整物理路径", "No project sln file found that can make the full physical path"));
                }
            }
            FileSugar.CreateFileReplace(url, classString, Encoding.UTF8);
            return url;
        }

        /// <summary>
        /// Get the class string based on the template and entity information.
        /// </summary>
        /// <param name="databaseId">The ID of the database.</param>
        /// <param name="template">The template to use.</param>
        /// <param name="item">The entity information.</param>
        /// <param name="templateEntitiesGen">The generated template entities.</param>
        /// <returns>The class string.</returns>
        private string GetClassString(long databaseId, ZeroTemplate template, ZeroEntityInfo item, out TemplateEntitiesGen templateEntitiesGen)
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

        /// <summary>
        /// Add a property to the property list based on the entity column information.
        /// </summary>
        /// <param name="propertyGens">The list of property generators.</param>
        /// <param name="columnInfos">The list of column information.</param>
        /// <param name="zeroEntityColumn">The entity column information.</param>
        private static void AddProperty(List<TemplatePropertyGen> propertyGens, List<SqlSugar.DbColumnInfo> columnInfos, ZeroEntityColumnInfo zeroEntityColumn)
        {
            var dbColumn = columnInfos.FirstOrDefault(it => it.DbColumnName!.EqualsCase(zeroEntityColumn.DbColumnName!));
            TemplatePropertyGen templatePropertyGen = new TemplatePropertyGen()
            {
                DbColumnName = zeroEntityColumn.DbColumnName,
                DbType = zeroEntityColumn.DataType,
                DecimalDigits = zeroEntityColumn.DecimalDigits,
                DefaultValue = string.Empty,
                Description = zeroEntityColumn.Description?.Replace(Environment.NewLine,PubConst.Common_BlankSpace)?.Replace(PubConst.Common_N, PubConst.Common_BlankSpace)?.Replace(PubConst.Common_R, PubConst.Common_BlankSpace),
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

        /// <summary>
        /// Process the property based on the database column information.
        /// </summary>
        /// <param name="dbColumn">The database column information.</param>
        /// <param name="templatePropertyGen">The template property generator.</param>
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

        /// <summary>
        /// Process the default value of the property.
        /// </summary>
        /// <param name="zeroEntityColumn">The entity column information.</param>
        /// <param name="templatePropertyGen">The template property generator.</param>
        private static void ProcessingPropertyDefault(ZeroEntityColumnInfo zeroEntityColumn, TemplatePropertyGen templatePropertyGen)
        {
            templatePropertyGen.PropertyType = EntityGeneratorManager.GetNativeTypeName(templatePropertyGen.PropertyType!);
            templatePropertyGen.PropertyType = templatePropertyGen.PropertyType + (zeroEntityColumn.IsNullable ? PubConst.Common_Q : string.Empty);
        }

        /// <summary>
        /// Get the URL based on the template entities information.
        /// </summary>
        /// <param name="url">The URL template.</param>
        /// <param name="templateEntitiesGen">The generated template entities.</param>
        /// <returns>The URL with replaced placeholders.</returns>
        private static string GetUrl(string url, TemplateEntitiesGen templateEntitiesGen)
        {
            url = url.Replace(PubConst.Common_Format0, templateEntitiesGen.ClassName?.Trim()).Replace(PubConst.Common_Format1, templateEntitiesGen.TableName);
            return url;
        }

        /// <summary>
        /// Get the entity information for the specified database and table IDs.
        /// </summary>
        /// <param name="databaseId">The ID of the database.</param>
        /// <param name="tableIds">The IDs of the tables.</param>
        /// <param name="db">The SQL Sugar client.</param>
        /// <returns>The list of entity information.</returns>
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
        /// <summary>
        /// Execute template generation based on the specified type, data, and template.
        /// </summary>
        /// <param name="type">The type of the template.</param>
        /// <param name="data">The data to use for template generation.</param>
        /// <param name="template">The template to use.</param>
        /// <returns>The generated template result.</returns>
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

        /// <summary>
        /// Execute template generation based on the entity type.
        /// </summary>
        /// <param name="data">The data to use for template generation.</param>
        /// <param name="template">The template to use.</param>
        /// <returns>The generated template result.</returns>
        private string ExecTemplateByEntity(string data, string template)
        {
            var model = new SerializeService().DeserializeObject<TemplateEntitiesGen>(data);
            var templateModel = new TemplateModel<TemplateEntitiesGen> { Model = model };
            var temp = new TextTemplateManager().RenderTemplate(template, templateModel);
            return temp.ToString();
        }
        #endregion

        #region Helper

        internal  string ExecTemplateByViewWithoutCreatingFiles(long databaseId, string viewName,bool isView, long templateId)
        {
            var db = App.Db;
            var template = App.Db.Queryable<ZeroTemplate>().First(it => it.Id == templateId);
            var item = new ZeroEntityInfo();
            var viewDb = App.GetDbById(databaseId);
            string className = string.Empty;
            if (isView == false)
            {
                var data = db.Queryable<ZeroEntityInfo>()!.InSingle(viewName);
                viewName = data!.DbTableName!;
                className = data!.ClassName!;
            }
            var selectValue = "*";
            if (viewDb!.CurrentConnectionConfig.DbType == SqlSugar.DbType.Oracle) 
            {
                var sqlBuilder = viewDb!.Queryable<object>().SqlBuilder;
                selectValue = $"{sqlBuilder.GetTranslationColumnName(viewName)}.{selectValue}";
            }
            var dt = viewDb!.Queryable<object>().AS(viewName).Take(1).Select(selectValue).ToDataTable();
            item.ClassName = className==string.Empty? viewName: className;
            item.DbTableName = viewName;
            item.Description = string.Empty;
            item.ZeroEntityColumnInfos = new List<ZeroEntityColumnInfo>();
            foreach (DataColumn dataColumn in dt.Columns)
            {
                item.ZeroEntityColumnInfos.Add(new ZeroEntityColumnInfo()
                {
                    PropertyName = dataColumn.ColumnName,
                    DbColumnName = dataColumn.ColumnName,
                    DataType = dataColumn.DataType.Name,
                    PropertyType = EntityGeneratorManager.GetNativeTypeByType(dataColumn.DataType)
                });
            }
            var entityInfo = db.Queryable<ZeroEntityInfo>()
                .Includes(it=>it.ZeroEntityColumnInfos).Where(it => it.DbTableName == item.DbTableName)
             .First();
            if (!isView&&entityInfo != null)
            {
                item = entityInfo;
            }
            var classString = GetClassString(databaseId, template, item, out _);
            return classString;
        }

        #endregion
    }
}
