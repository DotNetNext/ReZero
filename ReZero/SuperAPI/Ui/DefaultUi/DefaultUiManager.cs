using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks; 

namespace ReZero.SuperAPI
{
    /// <summary>
    /// 默认UI使用，如果是Vue前后分离不会使用该类
    /// </summary>
    public class DefaultUiManager : IUiManager
    {
        private readonly string masterPagePlaceholder = "@@master_page.html";
        private readonly string index_url = "@@index_url";
        private readonly string masterPageFolder = "template";
        private readonly string masterPageFileName = "master_page.html";
        private readonly string layoutContentPlaceholder = "@@lyear-layout-content";
        private readonly string masterMenuPlaceholder = "@@left-menu";
        private readonly string mastreNavNamePlaceholder = "@@nav-title";
        private readonly string pageControlPlaceholder = "@@page_control.html";
        private readonly string pageControlName = "page_control.html";
        public DefaultUiManager()
        {
        }

        /// <summary>
        /// Replaces the master page placeholder with the actual master page HTML content and replaces the layout content placeholder with the modified file content.
        /// </summary>
        /// <param name="fileContent">The content of the file to modify.</param>
        /// <param name="filePath">The path of the file to modify.</param>
        /// <returns>The modified file content.</returns>
        public async Task<string> GetHtmlAsync(string fileContent, string filePath, Microsoft.AspNetCore.Http.HttpContext content)
        {

            var url = (content.Request.Path + "" + content.Request.QueryString).ToLower();
            var modifiedContent = fileContent.Replace(masterPagePlaceholder, "");
            var masterPagePath = Path.Combine(Path.GetDirectoryName(filePath), masterPageFolder, masterPageFileName);
            var masterPageHtml = await File.ReadAllTextAsync(masterPagePath);

            //menu html
            var menuList = await App.Db.Queryable<ZeroInterfaceCategory>().ToTreeAsync(it => it.SubInterfaceCategories, it => it.ParentId, 0, it => it.Id);
            var currentMenu = await App.Db.Queryable<ZeroInterfaceCategory>().Where(it => it.Url!.ToLower() == url).FirstAsync();
            if (currentMenu == null)
            {
                currentMenu = await App.Db.Queryable<ZeroInterfaceCategory>().FirstAsync();
            }
            var parentMenu = await App.Db.Queryable<ZeroInterfaceCategory>().Where(it => it.Id == currentMenu.ParentId).FirstAsync();
            var menuHtml = await GetMenuHtml(menuList, filePath, currentMenu);

            //Nav title
            masterPageHtml = ReplaceNavTitle(masterPageHtml, currentMenu, parentMenu);

            //Page html
            modifiedContent = await ReplacePageContext(filePath, modifiedContent);

            modifiedContent = ReplceIndexSrc(modifiedContent, currentMenu);

            //Body context
            masterPageHtml = ReplaceBodyContext(modifiedContent, masterPageHtml, menuHtml);

            return masterPageHtml;
        }
        

        private string ReplceIndexSrc(string modifiedContent, ZeroInterfaceCategory? currentMenu)
        {
            if (currentMenu!.Id == InterfaceCategoryInitializerProvider.Id1)
            {
                modifiedContent = modifiedContent.Replace(index_url, SuperAPIModule._apiOptions!.UiOptions!.DefaultIndexSource);
            } 
            return modifiedContent;
        }

        private async Task<string> ReplacePageContext(string filePath,string html)
        {
            if (html?.Contains(pageControlPlaceholder)==true)
            {
                var path = Path.Combine(Path.GetDirectoryName(filePath), masterPageFolder, pageControlName);
                var pageHtml = await File.ReadAllTextAsync(path);
                html= html.Replace(pageControlPlaceholder, pageHtml);
            }
            return html;
        }

        private string ReplaceBodyContext(string modifiedContent, string masterPageHtml, string menuHtml)
        {
            masterPageHtml = masterPageHtml.Replace(masterMenuPlaceholder, menuHtml);
            masterPageHtml = masterPageHtml.Replace(layoutContentPlaceholder, modifiedContent);
            return masterPageHtml;
        }

        private string ReplaceNavTitle(string masterPageHtml, ZeroInterfaceCategory currentMenu, ZeroInterfaceCategory parentMenu)
        {
            var navTitle = parentMenu?.Name + "->" + currentMenu.Name;
            if (parentMenu == null) 
            {
                navTitle=TextHandler.GetCommonText("详情页","Detail");
            }
            masterPageHtml = masterPageHtml.Replace(mastreNavNamePlaceholder, navTitle);
            return masterPageHtml;
        }

        /// <summary>
        /// Generates the HTML code for the menu based on the given list of interface categories.
        /// </summary>
        /// <param name="categories">The list of interface categories.</param>
        /// <returns>The HTML code for the menu.</returns>
        public async Task<string> GetMenuHtml(List<ZeroInterfaceCategory> categories,string filePath, ZeroInterfaceCategory  current)
        {
            var result= await Task.FromResult(MenuBuilder.GenerateMenu(categories, current));
            return result;
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