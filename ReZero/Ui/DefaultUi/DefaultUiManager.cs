using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks; 

namespace ReZero
{
    /// <summary>
    /// 默认后台管理并非读写离，如果是VUE等UI框架不会走这个逻辑  
    /// </summary>
    public class DefaultUiManager : IUiManager
    { 
        private readonly string masterPagePlaceholder = "@@master_page.html";
        private readonly string masterPageFolder = "template";
        private readonly string masterPageFileName = "master_page.html";
        private readonly string layoutContentPlaceholder = "@@lyear-layout-content";

        public DefaultUiManager()
        { 
        }

        public async Task<string> GetHtmlAsync(string fileContent, string filePath)
        {  
            var modifiedContent = fileContent.Replace(masterPagePlaceholder, "");
            var masterPagePath = Path.Combine(Path.GetDirectoryName(filePath), masterPageFolder, masterPageFileName);
            var masterPageHtml = await File.ReadAllTextAsync(masterPagePath);
            modifiedContent = masterPageHtml.Replace(layoutContentPlaceholder, modifiedContent);
            return modifiedContent;
        }

        public async Task<string> GetMenuHtml(List<ZeroInterfaceCategory> categories) 
        {
            return MenuBuilder.GenerateMenu(categories);
        }

        public bool IsMasterPage(string fileContent)
        {
            return fileContent.Contains(masterPagePlaceholder);
        }
    }
}