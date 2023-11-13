using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{ 
    public class MenuBuilder
    {
        public static string GenerateMenu(List<ZeroInterfaceCategory> treeList)
        {
            StringBuilder htmlBuilder = new StringBuilder();
            int i = 0;
            foreach (var tree in treeList)
            { 
                var active = i == 0 ? "active" : "";
                if (tree.SubInterfaceCategories != null && tree.SubInterfaceCategories.Count > 0)
                {
                    htmlBuilder.AppendLine("<li class=\" "+ active + " nav-item nav-item-has-subnav\">");
                    htmlBuilder.AppendLine($"  <a href=\"{tree.Url}\"><i class=\"mdi mdi-menu\"></i> {tree.Name}</a>");
                    htmlBuilder.AppendLine("  <ul class=\"nav nav-subnav\">");
                    GenerateSubMenu(tree.SubInterfaceCategories, htmlBuilder);
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

        private static void GenerateSubMenu(List<ZeroInterfaceCategory> subTreeList, StringBuilder htmlBuilder)
        {
            foreach (var subTree in subTreeList)
            {
                if (subTree.SubInterfaceCategories != null && subTree.SubInterfaceCategories.Count > 0)
                {
                    htmlBuilder.AppendLine("    <li class=\"nav-item nav-item-has-subnav\">");
                    htmlBuilder.AppendLine($"      <a href=\"{subTree.Url}\">{subTree.Name}</a>");
                    htmlBuilder.AppendLine("      <ul class=\"nav nav-subnav\">");
                    GenerateSubMenu(subTree.SubInterfaceCategories, htmlBuilder);
                    htmlBuilder.AppendLine("      </ul>");
                    htmlBuilder.AppendLine("    </li>");
                }
                else
                {
                    htmlBuilder.AppendLine("    <li class=\"nav-item-no-subnav\">");
                    htmlBuilder.AppendLine($"      <a href=\"{subTree.Url}\">{subTree.Name}</a>");
                    htmlBuilder.AppendLine("    </li>");
                }
            }
        }
    }
}
