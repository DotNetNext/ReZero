using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.TextTemplate
{
    public class TextTemplateModel<T> where T:class
    {
        public T Model { get; set; } = null!;
    }
}
