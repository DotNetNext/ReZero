using SqlSugar;
using System;

namespace ReZero
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
        internal static ISqlSugarClient? PreStartupDb { get;  set; }

        /// <summary>
        /// Gets the instance of the SqlSugar client for database operations.
        /// </summary>
        /// <remarks>
        /// This property provides convenient access to the configured SqlSugar client for database operations.
        /// </remarks>
        internal static ISqlSugarClient Db { get => App.ServiceProvider!.GetService<DatabaseContext>().SugarClient; }

        internal static string Language
        {
            get
            {
                return ReZeroServiceCollectionExtensions._options!.Language.ToString();
            }
        }
    }
}