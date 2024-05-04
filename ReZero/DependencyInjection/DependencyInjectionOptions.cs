using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
namespace ReZero.DependencyInjection
{
    public class DependencyInjectionOptions
    {
        public  Assembly[]? assembly;
        public  bool InitDependencyInjection => assembly?.Any() == true;
        public DependencyInjectionOptions(params Assembly[] assembly) 
        {
            if (this.InitDependencyInjection == false)
            {
                this.assembly = assembly;
            }
        } 
    }
}
