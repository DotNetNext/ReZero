using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
namespace ReZero.SuperAPI
{
    public class Encryption
    {
        /// <summary>
        /// Encrypt the input string using MD5 hashing algorithm.
        /// </summary>
        /// <param name="input">The string to encrypt</param>
        /// <returns>The encrypted string in hexadecimal format</returns>
        public static string Encrypt(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString().ToLower();
            }
        }
    }
}
 