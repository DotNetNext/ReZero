using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    [AttributeUsage(AttributeTargets.All )]
    public class ChineseTextAttribute : Attribute
    {
        public ChineseTextAttribute(string text) 
        {
            this.Text = text;
        }
        public string? Text { get; set; }
     
    }
    [AttributeUsage(AttributeTargets.All )]
    public class TextEN : Attribute
    {
        public TextEN(string text)
        {
            this.Text = text;
        }
        public string? Text { get; set; } 
    }
}
