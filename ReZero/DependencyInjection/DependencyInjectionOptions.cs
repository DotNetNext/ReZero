using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
namespace ReZero.DependencyInjection
{
    public class DependencyInjectionOptions
    {
        public Assembly[]? Assembly { get; set; }
        public  bool InitDependencyInjection => Assembly?.Any() == true;
        public DependencyInjectionOptions(params Assembly[] assembly) 
        {
            if (this.InitDependencyInjection == false)
            {
                this.Assembly = assembly;
            }
        } 
    }
}
