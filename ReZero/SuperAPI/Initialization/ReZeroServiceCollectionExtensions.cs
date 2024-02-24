using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ReZero.ModuleSetup.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace ReZero.SuperAPI
{
    public static partial class SuperAPIModule
    {
       
        public static SuperAPIOptions? _apiOptions = null;

   
        internal static void Init(IServiceCollection services, ReZeroOptions options)
        {
            if (IsInitSupperApi(options))
            {
                var apiOptions = options.SuperApiOptions;
                _apiOptions = InitializeOptions(apiOptions);
                //InitUi(options);
                InitZeroStaticFileMiddleware();
                InitializeDataBase(_apiOptions);
                InitializeData(_apiOptions);
                AddTransientServices(services, _apiOptions);
            }
        }

        private static void InitUi(ReZeroOptions options)
        { 
            Assembly assembly = Assembly.GetExecutingAssembly(); 
            Version version = assembly.GetName().Version;
            var path = Path.Combine(options.SuperApiOptions.UiOptions.NugetPackagesPath, "rezero", version+"", "wwwroot","rezero");
            if (Directory.Exists(path))
            {
                var destDir= Path.Combine(AppContext.BaseDirectory, "wwwroot", "rezero");
                PubMethod.CopyDirectory(path,destDir);
            }
            else if(!Directory.Exists(path))  
            {
                throw new Exception(TextHandler.GetCommonText("初始化UI失败，可以手动将NGUET包中的wwwroot文件夹手动复制到API,没找到路径"+ path, "Failed to initialize UI, you can manually copy the wwwroot folder in the NGUET package to the API manually. no found "+path));
            } 
        }


        /// <summary>
        /// Initializes ZeroStaticFileMiddleware.
        /// </summary>
        private static void InitZeroStaticFileMiddleware()
        {
            _apiOptions!.UiOptions!.DefaultUiFolderName = ZeroStaticFileMiddleware.DefaultUiFolderName;
        }


        /// <summary>
        /// Initializes the database based on ReZero options.
        /// </summary>
        /// <param name="options">ReZero options.</param>
        private static void InitializeDataBase(SuperAPIOptions options)
        {
            if (options.DatabaseOptions!.InitializeTables == false)
            {
                return;
            }
            var types = PubMethod.GetTypesDerivedFromDbBase(typeof(DbBase));
            var db = new DatabaseContext(options.DatabaseOptions!.ConnectionConfig).SugarClient;
            App.PreStartupDb = db;
            db.DbMaintenance.CreateDatabase();
            db.CodeFirst.InitTables(types?.ToArray());
        }


        /// <summary>
        /// Adds transient services to the IServiceCollection.
        /// </summary>
        /// <param name="services">The IServiceCollection to which services are added.</param>
        /// <param name="options">ReZero options.</param>
        private static void AddTransientServices(IServiceCollection services, SuperAPIOptions options)
        {
            // Add transient services to the IServiceCollection.
            services.AddTransient<IDynamicApi, DynamicApiManager>();
            services.AddTransient<InternalApi, InternalApi>();
            services.AddTransient<IStartupFilter, ZeroApiRequestSetOptionsStartupFilter>();

            // Create an instance of ORM with the specified connection configuration and add it as a transient service.
            services.AddTransient<DatabaseContext>(it => new DatabaseContext(options.DatabaseOptions!.ConnectionConfig));
        }

        /// <summary>
        /// Initializes ReZero options. If options are not provided, creates a new instance of ReZeroOptions.
        /// </summary>
        /// <param name="options">Optional ReZero options.</param>
        /// <returns>Initialized ReZero options.</returns>
        private static SuperAPIOptions InitializeOptions(SuperAPIOptions? options)
        {
            options = options ?? new SuperAPIOptions();
            return options;
        }


        /// <summary>
        /// Initializes data based on ReZero options.
        /// </summary>
        /// <param name="options">ReZero options.</param>
        private static void InitializeData(SuperAPIOptions options)
        {
            new DataInitializerService().Initialize(options);
        }
         
        private static bool IsInitSupperApi(ReZeroOptions options)
        {
            return options.SuperApiOptions.IsEnableSuperAPI;
        }

    }
}
