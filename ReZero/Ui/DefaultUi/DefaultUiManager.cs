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
        private readonly string mastreNavNamePlaceholder = "@@nav-title";

        public DefaultUiManager()
        {
        }

        /// <summary>
        /// Replaces the master page placeholder with the actual master page HTML content and replaces the layout content placeholder with the modified file content.
        /// </summary>
        /// <param name="fileContent">The content of the file to modify.</param>
        /// <param name="filePath">The path of the file to modify.</param>
        /// <returns>The modified file content.</returns>
        public async Task<string> GetHtmlAsync(string fileContent, string filePath, string url)
        {
            var modifiedContent = fileContent.Replace(masterPagePlaceholder, "");
            var masterPagePath = Path.Combine(Path.GetDirectoryName(filePath), masterPageFolder, masterPageFileName);
            var masterPageHtml = await File.ReadAllTextAsync(masterPagePath);
            var menuList = await App.Db.Queryable<ZeroInterfaceCategory>().ToTreeAsync(it => it.SubInterfaceCategories, it => it.ParentId, 0, it => it.Id);
            var currentMenu = await App.Db.Queryable<ZeroInterfaceCategory>().Where(it=>it.Url!.ToLower()!.Contains(url)).FirstAsync();
            if (currentMenu != null) 
            {
                var parentMenu=await App.Db.Queryable<ZeroInterfaceCategory>().Where(it => it.Id == currentMenu.ParentId).FirstAsync();
                var navTitle = parentMenu.Name + "->" + currentMenu.Name;
                masterPageHtml = masterPageHtml.Replace(mastreNavNamePlaceholder, navTitle);
            }
            var menuHtml = await GetMenuHtml(menuList, filePath,url);
            masterPageHtml = masterPageHtml.Replace(masterMenuPlaceholder, menuHtml);
            modifiedContent = masterPageHtml.Replace(layoutContentPlaceholder, modifiedContent);
            return modifiedContent;
        }

        /// <summary>
        /// Generates the HTML code for the menu based on the given list of interface categories.
        /// </summary>
        /// <param name="categories">The list of interface categories.</param>
        /// <returns>The HTML code for the menu.</returns>
        public async Task<string> GetMenuHtml(List<ZeroInterfaceCategory> categories,string filePath,string url)
        {
            return await Task.FromResult(MenuBuilder.GenerateMenu(categories,url));
        }

        /// <summary>
        /// Determines whether the given file content contains the master page placeholder.
        /// </summary>
        /// <param name="fileContent">The content of the file to check.</param>
        /// <returns>True if the file content contains the master page placeholder, otherwise false.</returns>
        public bool IsMasterPage(string fileContent)
        {
            return fileContent.Contains(masterPagePlaceholder);
        }
    }
}