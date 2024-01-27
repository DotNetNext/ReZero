using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;  
using System;
using System.Collections.Generic;
using System.IO;
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
                InitUi(options);
                InitZeroStaticFileMiddleware();
                InitializeDataBase(_apiOptions);
                InitializeData(_apiOptions);
                AddTransientServices(services, _apiOptions);
            }
        }

        private static void InitUi(ReZeroOptions options)
        {
            var path = Path.Combine(options.SuperApiOptions.UiOptions.NugetPackagesPath, "rezero", "1.0.4","wwwroot","rezero");
            if (Directory.Exists(path))
            {
                var destDir= Path.Combine(AppContext.BaseDirectory, "wwwroot", "rezero");
                PubMethod.CopyDirectory(path,destDir);
            }
            else 
            {
                throw new Exception(TextHandler.GetCommonText("没有找到路径:"+ path+"需要自已配置。右键DLL查看引用", "path not found :" + path + " needs to be configured by itself. Right-click DLL to view reference"));
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
            return options.SuperApiOptions != null;
        }

    }
}
