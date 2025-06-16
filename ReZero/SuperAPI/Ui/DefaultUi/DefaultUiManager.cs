using Microsoft.AspNetCore.Http;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using DocumentFormat.OpenXml.Drawing.Diagrams;
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
        private readonly string authorizationLocalStorageName = "@@authorizationLocalStorageName";
        private readonly string version = "@@version";
        private readonly string pageNumber = "__pageNumber";
        private readonly string pageSize = "__pageSize";
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
            menuList = FilterMenuList(menuList);
            var currentMenu = await App.Db.Queryable<ZeroInterfaceCategory>().Where(it => it.Url!.ToLower() == url).FirstAsync();
            if (currentMenu == null)
            {
                if (url.Contains("utorials.html"))
                {
                    currentMenu = await App.Db.Queryable<ZeroInterfaceCategory>().FirstAsync(it=>it.Id== InterfaceCategoryInitializerProvider.Id300008);
                }
                else
                {
                    currentMenu = await App.Db.Queryable<ZeroInterfaceCategory>().FirstAsync();
                }
            }
            var parentMenu = await App.Db.Queryable<ZeroInterfaceCategory>().Where(it => it.Id == currentMenu.ParentId).FirstAsync();
            var menuHtml = await GetMenuHtml(menuList, filePath, currentMenu);

            //authorization
            masterPageHtml = GetAuthorizationHtml(content,masterPageHtml);

            //Samll page
            masterPageHtml = GetSmallPageHtml(content, masterPageHtml);

            //Nav title
            masterPageHtml = ReplaceNavTitle(masterPageHtml, currentMenu, parentMenu);

            //Page html
            modifiedContent = await ReplacePageContext(filePath, modifiedContent);

            modifiedContent = ReplceIndexSrc(modifiedContent, currentMenu);

            //Body context
            masterPageHtml = ReplaceBodyContext(modifiedContent, masterPageHtml, menuHtml);

            //token
            masterPageHtml = masterPageHtml.Replace(authorizationLocalStorageName, SuperAPIModule._apiOptions?.InterfaceOptions?.AuthorizationLocalStorageName);

            masterPageHtml = masterPageHtml.Replace(pageNumber, SuperAPIModule._apiOptions?.InterfaceOptions?.PageNumberPropName);

            masterPageHtml = masterPageHtml.Replace(pageSize, SuperAPIModule._apiOptions?.InterfaceOptions?.PageSizePropName);

            //version
            masterPageHtml = masterPageHtml.Replace(version, $"{Assembly.GetExecutingAssembly().GetName().Version}");
            return masterPageHtml;
        }

        private static List<ZeroInterfaceCategory> FilterMenuList(List<ZeroInterfaceCategory> menuList)
        {
            var db = App.Db;
            var options = SuperAPIModule._apiOptions;
            var sysSetting = db.Queryable<ZeroSysSetting>().Where(it => it.TypeId == PubConst.Setting_EnableLicenseType).First();
            if (sysSetting == null)
                sysSetting = new ZeroSysSetting();
            var isEnable = options?.InterfaceOptions?.License?.Enable == true;
            double time = 0;
            if (sysSetting.StringValue is { } key && options?.InterfaceOptions?.License?.LicenseValidateFunc is { } func)
            {
                time = (func(key).Date - DateTime.Now.Date).TotalDays;
            }
            object expirationTime = time;
            if (time <= 0 && isEnable)
            {
                menuList = menuList.Where(it => it.Id == InterfaceCategoryInitializerProvider.SystemSettingId).ToList();
                var firstObj = menuList.FirstOrDefault();
                if (firstObj != null)
                {
                    firstObj.SubInterfaceCategories = firstObj.SubInterfaceCategories.Where(it => it.Id == InterfaceCategoryInitializerProvider.Id300010).ToList();
                }
            }

            return menuList;
        }

        private string GetAuthorizationHtml(HttpContext content, string masterPageHtml)
        {
            if (!string.IsNullOrEmpty((content.Request.Query["token"] + "").ToString())&& content.Request.Query["token"].ToString()!="null")
            {
                masterPageHtml = masterPageHtml
                    .Replace("localStorage.getItem('@@authorizationLocalStorageName')",$"'{content.Request.Query["token"]}'");

            }
            masterPageHtml = masterPageHtml.Replace(authorizationLocalStorageName, SuperAPIModule._apiOptions?.InterfaceOptions?.AuthorizationLocalStorageName);
            //var db = App.Db;
            //var loginSetting=db.Queryable<ZeroSysSetting>().First(it => it.TypeId == PubConst.Setting_EnableLoginType);
            //if (loginSetting?.BoolValue == true) 
            //{
                masterPageHtml=masterPageHtml.Replace("tools.checkAuthorization();", "var isloginPage=true;\r\n        tools.checkAuthorization();");
            //}
            return masterPageHtml;
        }


        public Task<string> GetCustomPageHtmlAsync(string fileContent, string filePath, Microsoft.AspNetCore.Http.HttpContext content) 
        {
            fileContent = fileContent.Replace(authorizationLocalStorageName, SuperAPIModule._apiOptions?.InterfaceOptions?.AuthorizationLocalStorageName);
            return Task.FromResult(fileContent);
        }

        private static string GetSmallPageHtml(HttpContext content, string masterPageHtml)
        {
            if ((content.Request.Query["model"] + "").ToString().ToLower() == "small")
            {
                masterPageHtml = masterPageHtml
                    .Replace("<body data-theme=\"default\">", "<body data-theme=\"default\" class=\"lyear-layout-sidebar-close\">")
                    .Replace("dropdown dropdown-profile", "dropdown dropdown-profile hide")
                    .Replace("lyear-aside-toggler", "lyear-aside-toggler hide")
                    .Replace("@@nav-title", "<i class=\"mdi mdi-soccer\"></i> Rezero云API");
                  
            }

            return masterPageHtml;
        }

        private string ReplceIndexSrc(string modifiedContent, ZeroInterfaceCategory? currentMenu)
        {
            
            if (
                    SuperAPIModule._apiOptions!.UiOptions!.DefaultIndexSource!=null&&
                    !SuperAPIModule._apiOptions!.UiOptions!.DefaultIndexSource!.StartsWith("/")&&
                    !SuperAPIModule._apiOptions!.UiOptions!.DefaultIndexSource.Contains(":")) 
                {
                   SuperAPIModule._apiOptions!.UiOptions!.DefaultIndexSource = "/" + SuperAPIModule._apiOptions!.UiOptions!.DefaultIndexSource;
                }
                
            modifiedContent = modifiedContent.Replace(index_url, SuperAPIModule._apiOptions!.UiOptions!.DefaultIndexSource);
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