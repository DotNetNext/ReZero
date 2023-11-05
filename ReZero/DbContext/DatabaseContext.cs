using SqlSugar;
using System;

namespace ReZero
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
            SetConfigureExternalServices(connectionConfig);

            SetAop(connectionConfig);

            // Create a new SqlSugar client instance using the provided connection configuration.
            SugarClient = new SqlSugarClient(connectionConfig, db => 
            {
                db.QueryFilter.AddTableFilter<IDeleted>(it=>it.IsDeleted==false);
            }); 
        }

        private static void SetAop(ConnectionConfig connectionConfig)
        {
            connectionConfig.ConfigureExternalServices.EntityService = (x, p) =>
            {
                p.DbColumnName = UtilMethods.ToUnderLine(p.DbColumnName, true);
            };
            connectionConfig.ConfigureExternalServices.EntityNameService = (x, p) =>
            {
                p.DbTableName = UtilMethods.ToUnderLine(p.DbTableName, true);
            };
        }

        private static void SetConfigureExternalServices(ConnectionConfig connectionConfig)
        {
            connectionConfig.ConfigureExternalServices = connectionConfig.ConfigureExternalServices ?? new ConfigureExternalServices();
        }
    }
}