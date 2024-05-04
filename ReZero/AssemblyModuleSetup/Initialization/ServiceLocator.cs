using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero
{
    internal class ServiceLocator
    {
        public static ServiceProvider? Provider { get;   set; }
        public static IServiceCollection? Services { get;  set; }
    }
}
