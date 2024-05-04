using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyInjectionAttribute : Attribute
    {
    }
}
