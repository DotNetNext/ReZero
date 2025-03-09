using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using ReZero.Configuration; 
namespace ReZero.SuperAPI
{
    /// <summary>
    /// Represents configuration options for the ReZero SuperAPI.
    /// </summary>
    public class SuperAPIOptions
    { 
        public static SuperAPIOptions GetOptions( string fileName = "appsettings.json")
        {
            string key = "ReZero";
            ReZeroJson configuration=ApiConfiguration.GetJsonValue<ReZeroJson>(key,fileName);
            SuperAPIOptions superAPIOptions = new SuperAPIOptions();
            superAPIOptions.IsEnableSuperAPI = true;
            superAPIOptions.DatabaseOptions = new DatabaseOptions()
            {
                ConnectionConfig = new SuperAPIConnectionConfig()
                {
                    ConnectionString = configuration.BasicDatabase?.ConnectionString,
                    DbType = configuration?.BasicDatabase?.DbType ?? SqlSugar.DbType.Sqlite
                }
            };
            superAPIOptions.UiOptions = new UiOptions()
            {
                ShowNativeApiDocument = configuration?.Ui?.ShowNativeApiDocument ?? true
            };
            superAPIOptions.InterfaceOptions = new InterfaceOptions()
            {
                  Jwt=configuration?.Jwt,
                  CorsOptions = configuration?.Cors?? new ReZeroCors()
            };
            if (!string.IsNullOrEmpty(configuration?.Ui?.DefaultIndexSource)) 
            {
                superAPIOptions.UiOptions.DefaultIndexSource = configuration.Ui.DefaultIndexSource;
            } 
            return superAPIOptions;
        }

        public void EnableSuperApi()
        {
            SuperAPIOptions options = new SuperAPIOptions();
            IsEnableSuperAPI = true;
            this.DatabaseOptions = options.DatabaseOptions;
            this.InterfaceOptions = options.InterfaceOptions;
            this.DependencyInjectionOptions = options.DependencyInjectionOptions;
            this.UiOptions = options.UiOptions;
        }
        public void EnableSuperApi(SuperAPIOptions options)
        {
            IsEnableSuperAPI = true;
            this.DatabaseOptions = options.DatabaseOptions;
            this.InterfaceOptions = options.InterfaceOptions;
            this.DependencyInjectionOptions = options.DependencyInjectionOptions;
            this.UiOptions = options.UiOptions; 
        }

        /// <summary>
        /// Enable super api
        /// </summary>
        internal bool IsEnableSuperAPI = false;

        /// <summary>
        /// Gets or sets the database configuration options.
        /// </summary>
        public DatabaseOptions? DatabaseOptions { get; set; }


        public InterfaceOptions InterfaceOptions { get; set; } = new InterfaceOptions();

        /// <summary>
        /// Gets or sets the options for the DependencyInjection.
        /// </summary>
        public DependencyInjectionOptions DependencyInjectionOptions { get; set; } = new DependencyInjectionOptions();

        /// <summary>
        /// Gets or sets the UI configuration options.
        /// </summary>
        public UiOptions UiOptions { get; set; } = new UiOptions(); 

        
    }
    public class DependencyInjectionOptions
    {
        public Assembly[]? Assemblies { get; set; }

        public bool InitDependencyInjection => Assemblies?.Any() ?? false;

        public DependencyInjectionOptions(params Assembly[] assemblies)
        {
            if (!InitDependencyInjection)
            {
                this.Assemblies = assemblies;
            }
        }
    }
    public class InterfaceOptions
    {
        public string? AuthorizationLocalStorageName { get; set; } = "RezeroLocalStorage";
        public string PageNumberPropName { set; get; } = "PageNumber";
        public string PageSizePropName { set; get; } = "PageSize";
        public DefaultSuperApiAop SuperApiAop { get; set; } = new DefaultSuperApiAop();
        public Func<InterfaceContext, bool>? NoAuthorizationFunc { get; set; }

        public Func<object, object>? MergeDataToStandardDtoFunc { get; set; }
        public  ReZeroJwt?  Jwt { get; set; }
        public ReZeroCors CorsOptions { get; set; } = new ReZeroCors();

        public JsonSerializerSettings? JsonSerializerSettings { get; set; }
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
        internal Func<CallBackUserInfo> GetCurrentUserCallback { get; set; } = () => new CallBackUserInfo { UserId = "1", UserName = "Admin" };
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
        /// <summary>
        /// Show system api document
        /// </summary>
        public bool ShowSystemApiDocument { get; set; } = false;
        /// <summary>
        /// Show native api document
        /// </summary>
        public bool ShowNativeApiDocument { get; set; } = true;
        /// <summary>
        /// Enable Login page Configuration on the UI
        /// </summary>
        internal bool EnableLoginPage { get; set; }
    }


    public class AopOptions
    {
        public Func<HttpContext, Task>? DynamicApiBeforeInvokeAsync { get; set; }
        public Func<HttpContext, Task>? DynamicApiAfterInvokeAsync { get; set; }

        public Func<HttpContext, Task>? SystemApiBeforeInvokeAsync { get; set; }
        public Func<HttpContext, Task>? SystemApiAfterInvokeAsync { get; set; }
    }
}