using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    public class ActionTypeFormElementModel
    {
        public string? Name { get; set; }
        public string? Text { get; set; }
        public ElementType? ElementType { get; set; }
        public string? Value { get; set; }
        public bool IsRequired { get; set; }
        public List<ActionTypeFormElementModel>? Items { get; set; }

    }
}
