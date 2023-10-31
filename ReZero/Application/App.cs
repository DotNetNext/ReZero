using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    public class App
    {
       
        public  static ApplicationServiceProvider? ServiceProvider { get;internal set; }
        internal static ISqlSugarClient Db { get => App.ServiceProvider!.GetService<DatabaseReZeroContext>().SugarClient; }
    }
}
