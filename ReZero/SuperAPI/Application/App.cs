using SqlSugar;
using System;

namespace ReZero.SuperAPI
{
    /// <summary>
    /// Represents the application's main entry point and provides access to essential services and resources.
    /// </summary>
    public class App
    {
        /// <summary>
        /// Gets or sets the application's service provider, allowing access to registered services.
        /// </summary>
        public static ApplicationServiceProvider? ServiceProvider { get; internal set; }
        /// <summary>
        /// Represents a database connection object used before service startup.
        /// </summary>
        internal static ISqlSugarClient? PreStartupDb { get; set; }

        /// <summary>
        /// Gets the instance of the SqlSugar client for database operations.
        /// </summary>
        /// <remarks>
        /// This property provides convenient access to the configured SqlSugar client for database operations.
        /// </remarks>
        internal static ISqlSugarClient Db { get => ServiceProvider!.GetService<DatabaseContext>().SugarClient; }


        internal static SqlSugarClient? GetDbById(long dbId)
        {
            var rootDb = App.Db;
            var zeroDatabaseInfo = rootDb.Queryable<ZeroDatabaseInfo>().Where(it => it.Id == dbId).First();
            SqlSugarClient? db = null;
            if (zeroDatabaseInfo != null)
            {
                db = new SqlSugarClient(new ConnectionConfig()
                {
                    ConnectionString = zeroDatabaseInfo.Connection,
                    DbType = zeroDatabaseInfo.DbType,
                    IsAutoCloseConnection = true,
                    InitKeyType = InitKeyType.Attribute
                });
            } 
            return db;
        }
        internal static Language Language
        {
            get
            {
                return SuperAPIModule._apiOptions!.Language;
            }
        }
    }
}