using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    public static class Extensions
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

        /// <summary>
        /// Converts the first character of a string to uppercase and the rest to lowercase.
        /// </summary>
        /// <param name="input">The string to convert.</param>
        /// <returns>The converted string.</returns>
        public static string FirstCharToUpper(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return char.ToUpper(input[0]) + input.Substring(1).ToLower();
        }
    }
}
