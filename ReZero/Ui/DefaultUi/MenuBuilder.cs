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
        public static string GenerateMenu(List<ZeroInterfaceCategory> treeList, ZeroInterfaceCategory current)
        {
            StringBuilder htmlBuilder = new StringBuilder();
            int i = 0;
            foreach (var tree in treeList)
            {
                var isOpen = current.Id.ToString().StartsWith(tree.Id.ToString());
                var active = isOpen ? " active " : null;
                if (tree.SubInterfaceCategories != null && tree.SubInterfaceCategories.Count > 0)
                {
                    if (active != null)
                        active = $" {active} open ";
                    htmlBuilder.AppendLine("<li class=\" " + active + " nav-item nav-item-has-subnav\">");
                    htmlBuilder.AppendLine($"  <a href=\"{tree.Url}\"><i class=\"mdi mdi-menu\"></i> {tree.Name}</a>");
                    htmlBuilder.AppendLine("  <ul class=\"nav nav-subnav\">");
                    GenerateSubMenu(tree.SubInterfaceCategories, htmlBuilder, current);
                    htmlBuilder.AppendLine("  </ul>");
                    htmlBuilder.AppendLine("</li>");
                }
                else
                {
                    htmlBuilder.AppendLine("<li class=\"  " + active + " nav-item-no-subnav\">");
                    htmlBuilder.AppendLine($"  <a href=\"{tree.Url}\"><i class=\"mdi mdi-menu\"></i> {tree.Name}</a>");
                    htmlBuilder.AppendLine("</li>");
                }
                ++i;
            }

            return htmlBuilder.ToString();
        }

        private static void GenerateSubMenu(List<ZeroInterfaceCategory> subTreeList, StringBuilder htmlBuilder, ZeroInterfaceCategory current)
        {

            foreach (var subTree in subTreeList)
            {
                var isOpen = current.Id.ToString().StartsWith(subTree.Id.ToString());
                var active = isOpen ? " active " : "";

                if (subTree.SubInterfaceCategories != null && subTree.SubInterfaceCategories.Count > 0)
                {
                    htmlBuilder.AppendLine("    <li class=\" " + active + "nav-item nav-item-has-subnav\">");
                    htmlBuilder.AppendLine($"      <a href=\"{subTree.Url}\">{subTree.Name}</a>");
                    htmlBuilder.AppendLine("      <ul class=\"nav nav-subnav\">");
                    GenerateSubMenu(subTree.SubInterfaceCategories, htmlBuilder, current);
                    htmlBuilder.AppendLine("      </ul>");
                    htmlBuilder.AppendLine("    </li>");
                }
                else
                {
                    isOpen = current.Id.ToString().Equals(subTree.Id.ToString());
                    active = isOpen ? " active " : "";
                    htmlBuilder.AppendLine("    <li class=\" " + active + " nav-item-no-subnav\">");
                    htmlBuilder.AppendLine($"      <a href=\"{subTree.Url}\">{subTree.Name}</a>");
                    htmlBuilder.AppendLine("    </li>");
                }
            }
        }
    }
}
