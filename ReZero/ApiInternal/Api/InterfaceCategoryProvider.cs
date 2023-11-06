using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    internal class InterfaceCategoryProvider
    {
        List<ZeroInterfaceCategory> zeroInterfaceCategory = new List<ZeroInterfaceCategory>() { };
        public InterfaceCategoryProvider(List<ZeroInterfaceCategory> zeroInterfaceCategory) 
        {
            this.zeroInterfaceCategory = zeroInterfaceCategory;
        }

        internal void Set()
        {

        }
        private static ZeroInterfaceCategory GetNewItem()
        {
            return new ZeroInterfaceCategory()
            {
            };
        } 
    }
}
