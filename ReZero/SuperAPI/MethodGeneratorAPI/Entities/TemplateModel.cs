using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    public class TemplateModel<T> where T:class
    {
        public T Model { get; set; } = null!;
    }
}
