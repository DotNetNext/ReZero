using SqlSugar;
using System;

namespace ReZero.SuperAPI
{
    /// <summary>
    /// Represents the application's main entry point and provides access to essential services and resources.
    /// </summary>
    internal class App
    {
        /// <summary>
        /// Gets or sets the application's service provider, allowing access to registered services.
        /// </summary>
        internal static ApplicationServiceProvider? ServiceProvider { get;  set; }
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

        /// <summary>
        /// Obtain the database operation object based on the database ID
        /// </summary>
        /// <param name="dbId"></param>
        /// <returns></returns>
        internal static SqlSugarClient? GetDbById(long dbId)
        {
            var rootDb = App.Db;
            var zeroDatabaseInfo = rootDb.Queryable<ZeroDatabaseInfo>().Where(it => it.Id == dbId).First();
            SqlSugarClient? db = null;
            if (zeroDatabaseInfo != null) 
                db = GetSqlSugarClientByDatabaseInfo(zeroDatabaseInfo); 
            return db;
        }

        /// <summary>
        /// Obtain the database operation object based on the table ID
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        internal static SqlSugarClient? GetDbTableId(long tableId)
        {
            var rootDb = App.Db;
            var dbId = rootDb.Queryable<ZeroEntityInfo>().Where(it => it.Id == tableId).First()?.DataBaseId;
            var zeroDatabaseInfo = rootDb.Queryable<ZeroDatabaseInfo>().Where(it => it.Id == dbId).First();
            SqlSugarClient? db = null;
            if (zeroDatabaseInfo != null) 
                db = GetSqlSugarClientByDatabaseInfo(zeroDatabaseInfo); 
            return db;
        }

        /// <summary>
        /// Obtain the database operation object based on the ZeroDatabaseInfo
        /// </summary>
        /// <param name="zeroDatabaseInfo"></param>
        /// <returns></returns>
        private static SqlSugarClient GetSqlSugarClientByDatabaseInfo(ZeroDatabaseInfo zeroDatabaseInfo)
        {
            return new SqlSugarClient(new ConnectionConfig()
            {
                ConfigId=int.MaxValue,
                ConnectionString = zeroDatabaseInfo.Connection,
                DbType = zeroDatabaseInfo.DbType,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute,
                MoreSettings = new ConnMoreSettings
                {
                    SqlServerCodeFirstNvarchar = true,
                    SqliteCodeFirstEnableDropColumn = true,
                    EnableCodeFirstUpdatePrecision = true,
                    IsAutoToUpper=false,
                    PgSqlIsAutoToLower=false,
                    PgSqlIsAutoToLowerCodeFirst=false,
                    EnableOracleIdentity=true
                }
            },
            db =>
            {
                db.Aop.OnLogExecuting = (s, p) =>
                {
                    Console.WriteLine(UtilMethods.GetNativeSql(s, p));
                };
            });
        }
        /// <summary>
        /// Gets the language used by the SuperAPI module.
        /// </summary>
        /// <returns>The language.</returns>
        internal static Language Language
        {
            get
            {
                return SuperAPIModule._apiOptions!.UiOptions!.UiLanguage;
            }
        } 
    }
}