using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    /// <summary>
    /// Represents configuration options for ReZero.
    /// </summary>
    public class ReZeroOptions
    {
        /// <summary>
        /// Gets or sets whether to initialize configuration tables (default: true).
        /// </summary>
        public bool InitTable { get; set; } = true;

        public Language Language { get; set; }

        /// <summary>
        /// WwwRootPath\ReZeroDirName\DefaultUiFolderName
        /// </summary>
        public string? WwwRootPath { get; set; } = "wwwroot";
        /// <summary>
        ///  WwwRootPath\ReZeroDirName\DefaultUiFolderName
        /// </summary>
        public string? ReZeroDirName { get; set; } = "rezero";
        /// <summary>
        ///  WwwRootPath\ReZeroDirName\DefaultUiFolderName
        /// </summary>
        public string? DefaultUiFolderName { get; set; } = "default_ui";

        /// <summary>
        /// Gets or sets the initialization connection string information (default: SQLite).
        /// </summary>
        public ConnectionConfig ConnectionConfig { get; set; } = new ConnectionConfig()
        {
            DbType = DbType.Sqlite,                  // Default to SQLite database type
            IsAutoCloseConnection = true,            // Automatically close the connection after operation
            ConnectionString = "datasource=ReZero.db"           // Default database file name: ReZero.db
        };
    }
}