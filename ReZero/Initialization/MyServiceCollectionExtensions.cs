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
        /// Configures ReZero services within the specified IServiceCollection.
        /// </summary>
        /// <param name="services">The IServiceCollection to which ReZero services are added.</param>
        /// <param name="options">Optional ReZero options.</param>
        /// <returns>The updated IServiceCollection.</returns>
        public static IServiceCollection AddReZeroServices(this IServiceCollection services, ReZeroOptions? options = null)
        {
            options = InitializeOptions(options);

            AddTransientServices(services, options);

            InitializeDataBase(options);

            InitializeUser(options);

            InitializeReZeroApi(options);

            // Return the updated IServiceCollection.
            return services;
        }

        /// <summary>
        /// Initializes ReZero options. If options are not provided, creates a new instance of ReZeroOptions.
        /// </summary>
        /// <param name="options">Optional ReZero options.</param>
        /// <returns>Initialized ReZero options.</returns>
        private static ReZeroOptions InitializeOptions(ReZeroOptions? options)
        {
            options = options ?? new ReZeroOptions();
            return options;
        }

        /// <summary>
        /// Adds transient services to the IServiceCollection.
        /// </summary>
        /// <param name="services">The IServiceCollection to which services are added.</param>
        /// <param name="options">ReZero options.</param>
        private static void AddTransientServices(IServiceCollection services, ReZeroOptions options)
        {
            // Add transient services to the IServiceCollection.
            services.AddTransient<IDynamicApi, DynamicApi>();
            services.AddTransient<IReZeroApi, ReZeroApi>();
            services.AddTransient<IStartupFilter, RequestSetOptionsStartupFilter>();

            // Create an instance of ORM with the specified connection configuration and add it as a transient service.
            services.AddTransient<DatabaseContext>(it => new DatabaseContext(options.ConnectionConfig));
        }

        /// <summary>
        /// Initializes user-related functionality based on ReZero options.
        /// </summary>
        /// <param name="options">ReZero options.</param>
        private static void InitializeUser(ReZeroOptions options)
        {
            new UserInitializerService().Initialize(options);
        }

        /// <summary>
        /// Initializes built-in ReZero API based on ReZero options.
        /// </summary>
        /// <param name="options">ReZero options.</param>
        private static void InitializeReZeroApi(ReZeroOptions options)
        {
            new InternalApiManager().Initialize(options);
        }

        /// <summary>
        /// Initializes the database based on ReZero options.
        /// </summary>
        /// <param name="options">ReZero options.</param>
        private static void InitializeDataBase(ReZeroOptions options)
        {
            if (options.InitTable == false)
            {
                return;
            }
            var types = PubMethod.GetTypesDerivedFromDbBase(typeof(DbBase));
            var db = new DatabaseContext(options.ConnectionConfig).SugarClient;
            db.DbMaintenance.CreateDatabase();
            db.CodeFirst.InitTables(types?.ToArray());
        }
    }
}