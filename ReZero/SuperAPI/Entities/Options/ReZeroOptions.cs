using System;
using System.Threading.Tasks;

namespace ReZero.SuperAPI
{
    /// <summary>
    /// Represents configuration options for the ReZero SuperAPI.
    /// </summary>
    public class SuperAPIOptions
    {
        //public void EnableSuperApi(Action<SuperAPIOptions> func) 
        //{
        //    IsEnableSuperAPI = true;
        //    func(this);
        //}
        public void EnableSuperApi()
        {
            SuperAPIOptions options = new SuperAPIOptions();
            IsEnableSuperAPI = true;
            this.DatabaseOptions = options.DatabaseOptions;
            this.InterfaceOptions = options.InterfaceOptions;
            this.UiOptions = options.UiOptions;
        }
        public void EnableSuperApi(SuperAPIOptions  options)
        {
            IsEnableSuperAPI = true;
            this.DatabaseOptions = options.DatabaseOptions;
             this.InterfaceOptions = options.InterfaceOptions;
             this.UiOptions = options.UiOptions;
        }

        /// <summary>
        /// Enable super api
        /// </summary>
        internal bool IsEnableSuperAPI = false;

        /// <summary>
        /// Gets or sets the database configuration options.
        /// </summary>
        public DatabaseOptions DatabaseOptions { get; set; } = new DatabaseOptions();


        public InterfaceOptions InterfaceOptions { get; set; } = new InterfaceOptions();

        /// <summary>
        /// Gets or sets the UI configuration options.
        /// </summary>
        public UiOptions UiOptions { get; set; } = new UiOptions();
    }

    public class InterfaceOptions 
    {
        public DefaultSuperApiAop ISuperApiAop { get; set; } = new DefaultSuperApiAop();
    }

    /// <summary>
    /// Represents configuration options for the database settings in ReZero.
    /// </summary>
    public class DatabaseOptions
    {
        /// <summary>
        /// Gets or sets whether to initialize configuration tables. Default is true.
        /// </summary>
        public bool InitializeTables { get; set; } = true;

        /// <summary>
        /// Gets or sets the initialization connection string information. Default is SQLite.
        /// </summary>
        public SuperAPIConnectionConfig ConnectionConfig { get; set; } = new SuperAPIConnectionConfig()
        {
            DbType = SqlSugar.DbType.Sqlite,
            ConnectionString = "datasource=ReZero.db"
        };

        /// <summary>
        /// Callback function to retrieve the current user information.
        /// </summary>
        public Func<CallBackUserInfo> GetCurrentUserCallback { get; set; } = () => new CallBackUserInfo { UserId = "1", UserName = "Admin" };
    }

    /// <summary>
    /// Represents configuration options for the user interface settings in ReZero.
    /// </summary>
    public class UiOptions
    {
        /// <summary>
        /// Gets or sets the language for the UI.
        /// </summary>
        public Language UiLanguage { get; set; }

        /// <summary>
        /// Gets or sets the folder name for the default UI. Default is "default_ui".
        /// </summary>
        public string? DefaultUiFolderName { get; set; } = "default_ui";

        /// <summary>
        /// Gets or sets the source path for the default index. Default is "/swagger".
        /// </summary>
        public string? DefaultIndexSource { get; set; } = "/swagger";

        /// <summary>
        /// Gets or sets the path for NuGet packages. Default is the default user NuGet packages path.
        /// </summary>
        public string? NugetPackagesPath { get; set; } = @"C:\Users\Administrator\.nuget\packages";
    }
}