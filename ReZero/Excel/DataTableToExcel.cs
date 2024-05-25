using System.Data;
using System.IO;
using System;
using ClosedXML.Excel;
using System.Linq;
using ReZero.SuperAPI;

namespace ReZero.Excel
{
    public class DataTableToExcel
    {
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="dts"></param>
        /// <param name="name"></param>
        /// <param name="widths"></param>
        /// <returns></returns>
        public static byte[] ExportExcel(EecelData[] dts, string name, int[]? widths = null,string? navName=null)
        {
            XLWorkbook wb = new XLWorkbook();

            // 添加导航工作表
            var navigationSheet = wb.Worksheets.Add(TextHandler.GetCommonText("导航","Navigation"));
            navigationSheet.Cell(1, 1).Value = TextHandler.GetCommonText(navName ?? "Sheet名称","Sheet Name");
            navigationSheet.Cell(1, 2).Value = TextHandler.GetCommonText("备注","Description"); // 可以添加其他信息，例如描述

            int index = 0;
            int navRowIndex = 2; // 导航工作表的行索引
            foreach (var data in dts)
            {
                var dt = data.DataTable!;
                index++;
                for (int i = 1; i < 15; i++)
                {
                    // 删除Ignore列
                    if (dt.Columns.Contains("Column" + i))
                    {
                        dt.Columns.Remove("Column" + i);
                    }
                }
                var newdt = new DataTable();
                foreach (DataColumn item in dt.Columns)
                {
                    newdt.Columns.Add(item.ColumnName);
                }
                foreach (DataRow item in dt.Rows)
                {
                    DataRow dr = newdt.NewRow();
                    foreach (DataColumn c in dt.Columns)
                    {
                        var value = item[c.ColumnName] + "";
                        dr[c.ColumnName] = value;
                    }
                    newdt.Rows.Add(dr);
                }
                string sheetName;
                try
                {
                    sheetName = dt.TableName;
                    wb.Worksheets.Add(newdt, sheetName);
                }
                catch
                {
                    if (dt.TableName.Length < 28)
                    {
                        sheetName = "_" + dt.TableName;
                        wb.Worksheets.Add(newdt, sheetName);
                    }
                    else
                    {
                        sheetName = dt.TableName.Substring(0, 25) + DateTime.Now.ToString("...") + index;
                        wb.Worksheets.Add(newdt, sheetName);
                    }
                }

                var worksheet = wb.Worksheets.Last();
                foreach (var item in worksheet.Tables)
                {
                    item.Theme = XLTableTheme.None;
                }
                // 处理列
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    worksheet.Cell(1, i + 1).Value = dt.Columns[i].ColumnName;
                }
                // 处理列宽
                var colsWidth = dt.Columns.Cast<DataColumn>().Select(it => 20).ToArray();
                if (widths != null)
                {
                    colsWidth = widths;
                }
                for (int j = 1; j <= colsWidth.Length; j++)
                {
                    worksheet.Columns(j, j).Width = colsWidth[j - 1];
                }

                // 在导航工作表中添加链接
                var navCell = navigationSheet.Cell(navRowIndex, 1);
                navCell.Value = sheetName; 
                navCell.SetHyperlink(new XLHyperlink($"'{sheetName}'!A1"));
                navCell.Style.Font.FontColor = XLColor.Blue;
                navCell.Style.Font.Underline = XLFontUnderlineValues.Single;
                navigationSheet.Cell(navRowIndex, 2).Value = data.TableDescrpition; 
                navRowIndex++;
            }

            var minWidth = 50;
            if (navigationSheet.Column(1).Width < minWidth)
            {
                navigationSheet.Column(1).Width = minWidth;
            }
            if (navigationSheet.Column(2).Width < minWidth)
            {
                navigationSheet.Column(2).Width = minWidth;
            }  

            // 缓存到内存流，然后返回
            byte[] bytes = null!;
            using (MemoryStream stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                bytes = stream.ToArray();
            }
            return bytes;
        }

     
    }
    public class EecelData
    {
        public string? TableDescrpition { get; set; }
        public DataTable? DataTable { get; set; }
    }
}