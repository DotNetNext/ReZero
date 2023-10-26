using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    
   
    public class RequestSetOptionsStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return builder =>
            { 
                App.ServiceProvider = new ApplicationServiceProvider(builder);
                var func= new ZeroApiMiddleware(builder).HandleApiRequests();
                builder.Use(func);
                next(builder);
            };
        }
    }
}
