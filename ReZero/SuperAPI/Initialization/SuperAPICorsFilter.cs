using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Cors.Infrastructure;
using ReZero.Configuration;

namespace ReZero.SuperAPI
{
    public class SuperAPICorsFilter : IStartupFilter
    {
        public SuperAPICorsFilter(ReZeroCorsOptions options)
        {
            Options = options;
        }

        public ReZeroCorsOptions Options { get; }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
           return builder=>
           {
                builder.UseCors(Options.PolicyName);
                next(builder);
            };
        }
    }
}
