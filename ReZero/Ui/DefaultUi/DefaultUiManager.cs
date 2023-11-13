using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks; 

namespace ReZero
{
    /// <summary>
    /// 默认UI使用，如果是Vue前后分离不会使用该类
    /// </summary>
    public class DefaultUiManager : IUiManager
    { 
        private readonly string masterPagePlaceholder = "@@master_page.html";
        private readonly string masterPageFolder = "template";
        private readonly string masterPageFileName = "master_page.html";
        private readonly string layoutContentPlaceholder = "@@lyear-layout-content";
        private readonly string masterMenuPlaceholder = "@@left-menu";

        public DefaultUiManager()
        { 
        }

        public async Task<string> GetHtmlAsync(string fileContent, string filePath)
        {  
            var modifiedContent = fileContent.Replace(masterPagePlaceholder, "");
            var masterPagePath = Path.Combine(Path.GetDirectoryName(filePath), masterPageFolder, masterPageFileName);
            var masterPageHtml = await File.ReadAllTextAsync(masterPagePath);
            var menuList =await App.Db.Queryable<ZeroInterfaceCategory>().ToTreeAsync(it => it.SubInterfaceCategories, it => it.ParentId, 0, it => it.Id);
            
            var menuHtml = await GetMenuHtml(menuList);
            masterPageHtml=masterPageHtml.Replace(masterMenuPlaceholder, menuHtml);
            modifiedContent = masterPageHtml.Replace(layoutContentPlaceholder, modifiedContent);
            return modifiedContent;
        }

        public async Task<string> GetMenuHtml(List<ZeroInterfaceCategory> categories) 
        {
            return await Task.FromResult(MenuBuilder.GenerateMenu(categories));
        }

        public bool IsMasterPage(string fileContent)
        {
            return fileContent.Contains(masterPagePlaceholder);
        }
    }
}