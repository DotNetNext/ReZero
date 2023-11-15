using KdbndpTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace ReZero 
{ 
    public class MenuBuilder
    {
        public static string GenerateMenu(List<ZeroInterfaceCategory> treeList,string filePath)
        {
            StringBuilder htmlBuilder = new StringBuilder();
            int i = 0;
            foreach (var tree in treeList)
            {
                var urls = GetAllUrls(tree);
                var isOpen = urls.Any(it => filePath?.ToLower()?.Contains(it?.ToLower()) == true);
                var active = isOpen ? " active " : null; 
                if (tree.SubInterfaceCategories != null && tree.SubInterfaceCategories.Count > 0)
                {
                    if (active != null)
                        active = $" {active} open ";
                    htmlBuilder.AppendLine("<li class=\" "+ active + " nav-item nav-item-has-subnav\">");
                    htmlBuilder.AppendLine($"  <a href=\"{tree.Url}\"><i class=\"mdi mdi-menu\"></i> {tree.Name}</a>");
                    htmlBuilder.AppendLine("  <ul class=\"nav nav-subnav\">");
                    GenerateSubMenu(tree.SubInterfaceCategories, htmlBuilder,  filePath);
                    htmlBuilder.AppendLine("  </ul>");
                    htmlBuilder.AppendLine("</li>");
                }
                else
                {
                    htmlBuilder.AppendLine("<li class=\"  "+ active + " nav-item-no-subnav\">");
                    htmlBuilder.AppendLine($"  <a href=\"{tree.Url}\"><i class=\"mdi mdi-menu\"></i> {tree.Name}</a>");
                    htmlBuilder.AppendLine("</li>");
                }
                ++i;
            }

            return htmlBuilder.ToString();
        }

        private static List<string?> GetAllUrls(ZeroInterfaceCategory category)
        {
            List<string?> urls = new List<string?>();

            if (category != null)
            {
                // Add the URL of the current category
                urls.Add(category?.Url);

                // Recursively traverse subcategories and add their URLs
                if (category?.SubInterfaceCategories != null)
                {
                    foreach (var subCategory in category.SubInterfaceCategories)
                    {
                        urls.AddRange(GetAllUrls(subCategory));
                    }
                }
            } 
            return urls;
        }

        private static void GenerateSubMenu(List<ZeroInterfaceCategory> subTreeList, StringBuilder htmlBuilder,string filePath)
        {

            foreach (var subTree in subTreeList)
            {
                var urls = GetAllUrls(subTree);
                var isOpen = urls.Any(it => filePath?.ToLower()?.Contains(it?.ToLower()) == true);
                var active = isOpen ? " active " : "";

                if (subTree.SubInterfaceCategories != null && subTree.SubInterfaceCategories.Count > 0)
                {
                    htmlBuilder.AppendLine("    <li class=\" "+active+"nav-item nav-item-has-subnav\">");
                    htmlBuilder.AppendLine($"      <a href=\"{subTree.Url}\">{subTree.Name}</a>");
                    htmlBuilder.AppendLine("      <ul class=\"nav nav-subnav\">");
                    GenerateSubMenu(subTree.SubInterfaceCategories, htmlBuilder, filePath);
                    htmlBuilder.AppendLine("      </ul>");
                    htmlBuilder.AppendLine("    </li>");
                }
                else
                {
                    htmlBuilder.AppendLine("    <li class=\" "+active+" nav-item-no-subnav\">");
                    htmlBuilder.AppendLine($"      <a href=\"{subTree.Url}\">{subTree.Name}</a>");
                    htmlBuilder.AppendLine("    </li>");
                }
            }
        }
    }
}
