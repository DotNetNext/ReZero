﻿using KdbndpTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace ReZero.SuperAPI
{
    public class MenuBuilder
    {
        /// <summary>
        /// Generate the menu based on the provided tree list and current category.
        /// </summary>
        /// <param name="treeList">The list of interface categories.</param>
        /// <param name="current">The current interface category.</param>
        /// <returns>The generated menu HTML.</returns>
        public static string GenerateMenu(List<ZeroInterfaceCategory> treeList, ZeroInterfaceCategory current)
        {
            StringBuilder htmlBuilder = new StringBuilder();
            int i = 0;
            foreach (var tree in treeList.OrderBy(it=>it.SortId))
            {
                var isOpen = IsOpen(current, tree);
                var active = isOpen ? " active " : null; 
                var isHidden = SuperAPIModule._apiOptions?.InterfaceOptions?.Jwt?.Enable==true&&tree.Id>200;
                if (tree.SubInterfaceCategories != null && tree.SubInterfaceCategories.Count > 0)
                {
                    if (active != null)
                        active = $" {active} open "; 
                    if (isHidden) 
                        active = $"hide manager {active}"; 
                    htmlBuilder.AppendLine("<li class=\" " + active + " nav-item nav-item-has-subnav\">");
                    htmlBuilder.AppendLine($"  <a href=\"{tree.Url}\"><i class=\""+(!string.IsNullOrEmpty(tree.Icon) ?tree.Icon: "mdi mdi-menu" )+ $"\"></i> {tree.Name}</a>");
                    htmlBuilder.AppendLine("  <ul class=\"nav nav-subnav\">");
                    GenerateSubMenu(tree.SubInterfaceCategories, htmlBuilder, current);
                    htmlBuilder.AppendLine("  </ul>");
                    htmlBuilder.AppendLine("</li>");
                }
                else
                {
                    if (isHidden)
                        active = $"hide manager {active}";
                    htmlBuilder.AppendLine("<li class=\"  " + active + " nav-item-no-subnav\">");
                    htmlBuilder.AppendLine($"  <a href=\"{tree.Url}\"><i class=\""+(!string.IsNullOrEmpty(tree.Icon) ? tree.Icon : "mdi mdi-menu")+$"\"></i> {tree.Name}</a>");
                    htmlBuilder.AppendLine("</li>");
                }
                ++i;
            }

            return htmlBuilder.ToString();
        }

        private static void GenerateSubMenu(List<ZeroInterfaceCategory> subTreeList, StringBuilder htmlBuilder, ZeroInterfaceCategory current)
        {

            foreach (var subTree in subTreeList.OrderBy(it=>it.SortId))
            {
                var isOpen = IsOpen(current, subTree);
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

        private static bool IsOpen(ZeroInterfaceCategory current, ZeroInterfaceCategory subTree)
        {
            return current.ParentId.ToString().Equals(subTree.Id.ToString()) || current.Id == subTree.Id;
        }
    }
}
