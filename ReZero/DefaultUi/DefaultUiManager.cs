using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ReZero 
{
    /// <summary>
    /// The default UI manager does not use read-write separation for the sake of launching without installation.
    /// Custom UI implementations may not require this mechanism.
    /// </summary>
    internal class DefaultUiManager
    {
        private string fileContent;
        private string filePath;
        public DefaultUiManager(string fileContent,string filePath)
        {
            this.fileContent = fileContent;
            this.filePath = filePath;
        }

        public async Task<string> GetHtmlAsync() 
        {
            fileContent = fileContent.Replace("@@master_page.html", "");
            var path = Path.Combine(Path.GetDirectoryName(this.filePath), "template", "master_page.html");
            var masterPageHtml = await File.ReadAllTextAsync(path);
            fileContent = masterPageHtml.Replace("@@lyear-layout-content", fileContent);
            return fileContent;
        }


        public  bool IsMasterPage(string fileContent)
        {
            return fileContent.Contains("@@master_page.html");
        }
    }
}
