using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.Configuration
{

    public class ReZeroCors
    {
        public bool Enable { get; set; }
        public string? PolicyName { get; set; }
        public string[]? Origins { get; set; }
        public string[]? Headers { get; set; }
        public string[]? Methods { get; set; }
        public bool AllowCredentials { get; set; } 
    }
}
