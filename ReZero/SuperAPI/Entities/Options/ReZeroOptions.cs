using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    /// <summary>
    /// Represents configuration options for ReZero.
    /// </summary>
    public class SuperAPIOptions
    {
        /// <summary>
        /// Gets or sets whether to initialize configuration tables (default: true).
        /// </summary>
        public bool InitTable { get; set; } = true;

        public Language Language { get; set; } 
        
        /// <summary>
        ///  WwwRootPath\ReZeroDirName\DefaultUiFolderName
        /// </summary>
        public string? DefaultUiFolderName { get; set; } = "default_ui";

        /// <summary>
        /// index src
        /// </summary>
        public string? IndexSrc { get; set; } = "/swagger";

        /// <summary>
        /// Gets or sets the initialization connection string information (default: SQLite).
        /// </summary>
        public ReZeroConnectionConfig ConnectionConfig { get; set; } = new ReZeroConnectionConfig()
        {
            DbType = DbType.Sqlite,                
            ConnectionString = "datasource=ReZero.db"       
        };

        /// <summary>
        /// Callback function to get the current user information.
        /// </summary>
        public Func<CallBackUserInfo> GetCurrentUserCallback { get; set; } = () => new CallBackUserInfo { UserId="1", UserName="Admin" };
    } 
}