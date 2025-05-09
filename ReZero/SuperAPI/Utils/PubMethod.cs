using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
namespace ReZero.SuperAPI
{
    /// <summary>
    /// Provides utility methods for common operations.
    /// </summary>
    internal class PubMethod
    {
        public static byte[] ConvertBase64ToBytes(string dataUri)
        {
            if (string.IsNullOrWhiteSpace(dataUri))
                throw new ArgumentException("Data URI cannot be null or empty.", nameof(dataUri));

            var match = Regex.Match(dataUri, @"^data:(image/\w+);base64,(.+)$", RegexOptions.IgnoreCase);
            if (!match.Success)
                throw new FormatException("Invalid data URI format.");

            return Convert.FromBase64String(match.Groups[2].Value);
        }
        /// <summary>
        /// Checks if the given URL has a valid format.
        /// </summary>
        /// <param name="url">The URL to check</param>
        /// <returns>True if the URL has a valid format, otherwise false</returns>
        public static bool IsValidUrlFormat(string url)
        {
            string pattern = @"^\/[a-zA-Z0-9_-]+\/[a-zA-Z0-9_-]+$";
            if (url.Contains("."))
                url = System.IO.Path.GetFileNameWithoutExtension(url);
            Regex regex = new Regex(pattern);

            return regex.IsMatch(url);
        }

        /// <summary>
        /// Get the types derived from the specified base type.
        /// </summary>
        /// <param name="baseType">The base type</param>
        /// <returns>A list of types derived from the base type</returns>
        public static List<Type> GetTypesDerivedFromDbBase(Type baseType)
        {
            Assembly assembly = baseType.Assembly;
            List<Type> derivedTypes = new List<Type>();
            Type[] types = assembly.GetTypes();
            foreach (Type type in types)
            {
                if (type.Name == nameof(SavePermissionInfoDetailModel)) 
                {
                    continue;
                }
                if (type.IsSubclassOf(baseType))
                {
                    derivedTypes.Add(type);
                }
            }
            return derivedTypes;
        }

        /// <summary>
        /// Checks if the given string is a valid property name.
        /// </summary>
        /// <param name="str">The string to check</param>
        /// <returns>True if the string is a valid property name, otherwise false</returns>
        public static bool CheckIsPropertyName(string str)
        {
            return Regex.IsMatch(str, @"^[\u4e00-\u9fa5a-zA-Z_]\w*$");
        }

        public static byte[] ConvertFromBase64(string base64String)
        { 
            int startIndex = base64String.IndexOf(',') + 1;
            string base64Data = base64String.Substring(startIndex);
             
            return Convert.FromBase64String(base64Data);
        }

        public static void CopyDirectory(string sourceDir, string destDir)
        {
            if (!Directory.Exists(destDir))
            {
                Directory.CreateDirectory(destDir);
            }

            string[] files = Directory.GetFiles(sourceDir);

            foreach (string file in files)
            {
                string destFile = Path.Combine(destDir, Path.GetFileName(file));
                File.Copy(file, destFile, true); // 设置为 true 表示覆盖已存在的文件
            }

            string[] dirs = Directory.GetDirectories(sourceDir);

            foreach (string dir in dirs)
            {
                string destSubDir = Path.Combine(destDir, Path.GetFileName(dir));
                CopyDirectory(dir, destSubDir);
            }
        }
    }
}
