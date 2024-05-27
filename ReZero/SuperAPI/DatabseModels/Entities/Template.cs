using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    public class ZeroTemplate : DbBase
    {
        public int TypeId { get; set; }
        public string? Title { get; set; } 
        public string ? TemplateContent { get; set; }
    }
}
