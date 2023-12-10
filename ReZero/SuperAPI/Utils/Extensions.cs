using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    public static  class Extensions
    {
        public static bool EqualsCase(this string a, string b) 
        {
            return a?.ToLower() == b?.ToLower();
        }
    }
}
