using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero
{
    public class Encryption
    {
        public static string Encrypt(string input)
        {
            char[] inputArray = input.ToCharArray();
            for (int i = 0; i < inputArray.Length; i++)
            {
                // Shift each character by 3 positions in the Unicode table
                inputArray[i] = (char)(inputArray[i] + 3);
            }
            return new string(inputArray);
        }

        public static string Decrypt(string input)
        {
            char[] inputArray = input.ToCharArray();
            for (int i = 0; i < inputArray.Length; i++)
            {
                // Shift each character back by 3 positions in the Unicode table
                inputArray[i] = (char)(inputArray[i] - 3);
            }
            return new string(inputArray);
        }
    }
}
