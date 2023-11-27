using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ReZero
{
    /// <summary>
    /// Extension methods for configuring ReZero services in IServiceCollection.
    /// </summary>
    public static class ReZeroServiceCollectionExtensions
    {
        /// <summary>
        /// Holds the ReZeroOptions instance.
        /// </summary>
        public static SuperAPIOptions? _options = null;

   
        ///<summary>
        /// Adds Super API services to the IServiceCollection.
        /// </summary>
        /// <param name="services">The IServiceCollection to which services are added.</param>
        /// <param name="options">Optional ReZero options.</param>
        /// <returns>The updated IServiceCollection.</returns>
        public static IServiceCollection AddSuperAPIServices(this IServiceCollection services, SuperAPIOptions? options = null)
        {
            _options = options = InitializeOptions(options);

            InitZeroStaticFileMiddleware();

            AddTransientServices(services, options);

            InitializeDataBase(options);

            InitializeData(options);

            return services;
        }


        /// <summary>
        /// Adds ReZero services to the IServiceCollection.
        /// </summary>
        /// <param name="services">The IServiceCollection to which services are added.</param>
        /// <param name="options">Optional ReZero options.</param>
        /// <returns>The updated IServiceCollection.</returns>
        public static IServiceCollection AddReZeroServices(this IServiceCollection services, SuperAPIOptions? options = null)
        {
            throw new Exception("未来计划");
        }

        /// <summary>
        /// Initializes ZeroStaticFileMiddleware.
        /// </summary>
        private static void InitZeroStaticFileMiddleware()
        {
            _options!.DefaultUiFolderName = ZeroStaticFileMiddleware.DefaultUiFolderName;
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
            services.AddTransient<DatabaseContext>(it => new DatabaseContext(options.ConnectionConfig));
        }

        /// <summary>
        /// Initializes data based on ReZero options.
        /// </summary>
        /// <param name="options">ReZero options.</param>
        private static void InitializeData(SuperAPIOptions options)
        {
            new DataInitializerService().Initialize(options);
        }

        /// <summary>
        /// Initializes the database based on ReZero options.
        /// </summary>
        /// <param name="options">ReZero options.</param>
        private static void InitializeDataBase(SuperAPIOptions options)
        {
            if (options.InitTable == false)
            {
                return;
            }
            var types = PubMethod.GetTypesDerivedFromDbBase(typeof(DbBase));
            var db = new DatabaseContext(options.ConnectionConfig).SugarClient;
            App.PreStartupDb = db;
            db.DbMaintenance.CreateDatabase();
            db.CodeFirst.InitTables(types?.ToArray());
        }
    }
}