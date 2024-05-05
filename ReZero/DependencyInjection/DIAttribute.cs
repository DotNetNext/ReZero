using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.DependencyInjection
{
    // 自定义的DI属性  
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DIAttribute : Attribute
    {
    }
}
