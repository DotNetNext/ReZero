using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ReZero.Excel
{

    /// <summary>
    /// Represents the Excel data.
    /// </summary>
    public class ExcelData
    {
        /// <summary>
        /// Gets or sets the description of the table.
        /// </summary>
        public string? TableDescrpition { get; set; }

        /// <summary>
        /// Gets or sets the DataTable.
        /// </summary>
        public DataTable? DataTable { get; set; }
    }
}
