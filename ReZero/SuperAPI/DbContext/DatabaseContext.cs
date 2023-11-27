using SqlSugar;
using System;

namespace ReZero.SuperAPI
{
    /// <summary>
    /// Represents a database context for handling database operations using SqlSugar.
    /// </summary>
    public class DatabaseContext
    {
        /// <summary>
        /// Gets the SqlSugar client instance for performing database operations.
        /// </summary>
        public ISqlSugarClient SugarClient { get; private set; }

        /// <summary>
        /// Initializes a new instance of the DatabaseContext class with the provided database connection configuration.
        /// </summary>
        /// <param name="connectionConfig">Database connection configuration.</param>
        public DatabaseContext(ConnectionConfig connectionConfig)
        {
            InitializeExternalServices(connectionConfig);

            ConfigureExternalServices(connectionConfig);

            // Create a new SqlSugar client instance using the provided connection configuration.
            SugarClient = new SqlSugarClient(connectionConfig, db =>
            {
                db.QueryFilter.AddTableFilter<IDeleted>(it => it.IsDeleted == false);
                db.Aop.OnLogExecuting = (s, p) =>
                Console.WriteLine( UtilMethods.GetNativeSql(s, p) );
            });


        }

        /// <summary>
        /// Configures external services for the provided database connection configuration.
        /// </summary>
        /// <param name="connectionConfig">Database connection configuration.</param>
        private static void ConfigureExternalServices(ConnectionConfig connectionConfig)
        {
            connectionConfig.ConfigureExternalServices.EntityService = (x, p) =>
            {
                // Convert the database column name to snake case.
                p.DbColumnName = UtilMethods.ToUnderLine(p.DbColumnName);
            };
            connectionConfig.ConfigureExternalServices.EntityNameService = (x, p) =>
            {
                // Convert the database table name to snake case.
                p.DbTableName = UtilMethods.ToUnderLine(p.DbTableName);
            };
        }

        /// <summary>
        /// Initializes external services for the provided database connection configuration.
        /// </summary>
        /// <param name="connectionConfig">Database connection configuration.</param>
        private static void InitializeExternalServices(ConnectionConfig connectionConfig)
        {
            // Adds comments to explain the purpose of the method.
            // Sets the ConfigureExternalServices property of the provided connection configuration to a new instance of ConfigureExternalServices if it is null.
            connectionConfig.ConfigureExternalServices = connectionConfig.ConfigureExternalServices ?? new ConfigureExternalServices();
        }
    }
}