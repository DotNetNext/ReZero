using Microsoft.AspNetCore.Builder;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Diagnostics;
namespace ReZero.Configuration
{
    public class ApiConfiguration
    {
        /// <summary>  
        /// 获取当前DLL文件的完整路径。  
        /// </summary>  
        /// <returns>DLL文件的完整路径。</returns>  
        public static string GetCurrentDllFullPath()
        {
            var assembly = Assembly.GetExecutingAssembly();
            return assembly.Location;
        }

        // 获取当前执行程序（EXE）的完整路径  
        public static string GetCurrentExeFullPath()
        {
            return Process.GetCurrentProcess().MainModule.FileName;
        }

        // 获取当前执行程序（EXE）的目录  
        public static string GetCurrentExeDirectory()
        {
            return Path.GetDirectoryName(GetCurrentExeFullPath());
        }

        /// <summary>  
        /// 从JSON文件中读取并反序列化指定键的值到泛型类型T。  
        /// </summary>  
        /// <typeparam name="T">要反序列化的目标类型。</typeparam>  
        /// <param name="key">JSON对象中的键。</param>  
        /// <param name="fileName">JSON文件的名称，默认为"appsettings.json"。如果文件位于DLL相同目录，则只需文件名；否则，需要提供完整路径。</param>  
        /// <returns>反序列化后的对象。</returns>  
        public static T GetJsonValue<T>(string key, string fileName = "appsettings.json")
        {
           
            string fullPath = Path.Combine(GetCurrentExeDirectory(), fileName); 
            if (!File.Exists(fullPath))
            {
                // 获取DLL的目录路径  
                string dllPath = Path.GetDirectoryName(GetCurrentDllFullPath());
                fullPath =Path.Combine(dllPath, fileName);
            }

            // 读取JSON文件内容  
            string jsonContent = File.ReadAllText(fullPath, Encoding.UTF8);
            if (typeof(T).IsValueType || typeof(T) == typeof(string))
            {
                return (T)UtilMethods.ChangeType2(jsonContent, typeof(T));
            }
            try
            {
                // 解析JSON内容为JObject  
                JObject jsonObject = JObject.Parse(jsonContent);

                // 根据提供的键获取对应的JToken  
                JToken? token = jsonObject.SelectToken(key!);

                if (token != null)
                {
                    // 将JToken反序列化为泛型类型T  
                    return token.ToObject<T>();
                }
                else
                {
                    throw new ArgumentException($"GetJsonValue<{typeof(T).Name}>() error。The specified key '{key}' was not found in the JSON file.");
                }
            }
            catch (JsonReaderException ex)
            {
                throw new InvalidOperationException($"GetJsonValue<{typeof(T).Name}>() error。Error parsing JSON file at path: {fullPath}", ex);
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException($"GetJsonValue<{typeof(T).Name}>() error。The JSON file was not found at path: {fullPath}", ex);
            }
        }
    }
}  