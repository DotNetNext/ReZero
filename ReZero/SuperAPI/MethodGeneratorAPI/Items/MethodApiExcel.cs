using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ReZero.SuperAPI 
{
    public partial class MethodApi
    {  
        public byte[] ExportEntities(long [] tableIds)
        { 
            List<DataTable> datatables = new List<DataTable>();
            var db = App.Db;
            var datas=db.Queryable<ZeroEntityInfo>().WhereIF(tableIds.Any(), it=> tableIds.Contains(it.Id)).Includes(it => it.ZeroEntityColumnInfos).ToList();
            foreach (var item in datas)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("列名");
                dt.Columns.Add("列描述");
                dt.Columns.Add("列类型");
                dt.Columns.Add("实体类型");
                dt.Columns.Add("主键");
                dt.Columns.Add("自增");
                dt.Columns.Add("可空");
                dt.Columns.Add("长度");
                dt.Columns.Add("精度");
                dt.Columns.Add("默认值");
                dt.Columns.Add("表名"); ;
                dt.Columns.Add("表描述");
                foreach (var it in item.ZeroEntityColumnInfos!)
                {
                    var dr = dt.NewRow();
                    dr["列名"] = it.DbColumnName;
                    dr["列描述"] = it.Description;
                    dr["列类型"] = it.DataType;
                    dr["实体类型"] = it.PropertyType;
                    dr["表名"] = item.DbTableName;
                    dr["表描述"] = item.Description;

                    dr["主键"] = it.IsPrimarykey ? "yes" : "";
                    dr["自增"] = it.IsIdentity ? "yes" : "";
                    dr["可空"] = it.IsNullable ? "yes" : "";
                    dr["长度"] = it.Length;
                    dr["精度"] = it.DecimalDigits; 

                    dt.Rows.Add(dr);
                }
                dt.TableName = item.DbTableName;
                datatables.Add(dt);
            }
            return ReZero.Excel.DataTableToExcel.ExportExcel(datatables.ToArray(),$"{DateTime.Now.ToString("实体文档.xlsx")}");
        }
    }
}
