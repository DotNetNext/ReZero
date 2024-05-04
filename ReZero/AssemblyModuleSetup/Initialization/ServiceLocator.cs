using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero
{
    internal class ServiceLocator
    { 
        public static IServiceCollection? Services { get;  set; }
    }
}
