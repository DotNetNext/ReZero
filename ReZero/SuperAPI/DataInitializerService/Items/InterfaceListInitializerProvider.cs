using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    internal partial class InterfaceListInitializerProvider
    {
        List<ZeroInterfaceList> zeroInterfaceList = new List<ZeroInterfaceList>() { };
        public InterfaceListInitializerProvider(List<ZeroInterfaceList> zeroInterfaceList)
        {
            this.zeroInterfaceList = zeroInterfaceList;
        }
      
        internal void Set()
        {
            AddInit_ZeroInterfaceList();
            AddInit_InterfaceCategory();
            AddInit_DatabaseInfo();
            AddInit_CodeList();
            AddInit_EntityInfo();
            AddInit_EntityColumnInfo();
        } 
    }
}
