using System;
using System.Collections.Generic;
using System.Text; 
using System.IO;

namespace ReZero.SuperAPI 
{
    public class DirectoryHelper
    {
        public static string FindParentDirectoryWithSlnFile(string startDirectory)
        {
            if (string.IsNullOrWhiteSpace(startDirectory) || !Directory.Exists(startDirectory))
            {
                throw new ArgumentException("Invalid start directory.", nameof(startDirectory));
            }

            return FindParentDirectoryWithSlnFileRecursive(startDirectory);
        }

        private static string FindParentDirectoryWithSlnFileRecursive(string currentDirectory)
        {
            // 检查当前目录中的文件  
            var files = Directory.GetFiles(currentDirectory, "*.sln");
            if (files.Length > 0)
            {
                // 找到.sln文件，返回当前目录  
                return currentDirectory;
            }

            // 如果没有找到，则检查上一级目录  
            var parentDirectory = Directory.GetParent(currentDirectory)?.FullName;
            if (parentDirectory == null)
            {
                // 如果没有上一级目录（即已经是根目录），则返回null  
                return null;
            }

            // 递归调用自身，检查上一级目录  
            return FindParentDirectoryWithSlnFileRecursive(parentDirectory);
        }
    } 
}
