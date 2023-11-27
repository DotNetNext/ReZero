using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
namespace ReZero.SuperAPI
{
    public class Encryption
    {
        /// <summary>
        /// Encrypt the input string by shifting each character by 3 positions in the Unicode table.
        /// </summary>
        /// <param name="input">The string to encrypt</param>
        /// <returns>The encrypted string</returns>
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
        /// <summary>
        /// Decrypt the input string by shifting each character back by 3 positions in the Unicode table.
        /// </summary>
        /// <param name="input">The string to decrypt</param>
        /// <returns>The decrypted string</returns>
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
 