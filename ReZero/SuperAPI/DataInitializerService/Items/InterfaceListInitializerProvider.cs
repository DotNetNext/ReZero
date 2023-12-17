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
            GetZeroInterfaceList();
            GetInterfaceCategory();
            GetDatabaseList();
            GetCodeList();
            GetEntityList();
        } 
    }
}
