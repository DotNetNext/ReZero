using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
namespace ReZero.DependencyInjection
{
    public class DependencyInjectionOptions
    {
        public Assembly[]? Assemblies { get; set; }
        public  bool InitDependencyInjection => Assemblies?.Any() == true;
        public DependencyInjectionOptions(params Assembly[] assemblies) 
        {
            if (this.InitDependencyInjection == false)
            {
                this.Assemblies = assemblies;
            }
        } 
    }
}
