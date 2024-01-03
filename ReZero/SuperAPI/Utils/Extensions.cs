using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    public static  class Extensions
    {
        /// <summary>
        /// Determines whether two strings are equal, ignoring case.
        /// </summary>
        /// <param name="a">The first string to compare.</param>
        /// <param name="b">The second string to compare.</param>
        /// <returns>True if the strings are equal, ignoring case; otherwise, false.</returns>
        public static bool EqualsCase(this string a, string b)
        {
            return a?.ToLower() == b?.ToLower();
        }
    }
}
