using System;
using System.Collections.Generic;
using System.Text; 

namespace ReZero
{
    public class InternalApiManager
    {
        List<ZeroInterfaceList> zeroInterfaceList = new List<ZeroInterfaceList>() { };
        List<ZeroInterfaceCategory>  zeroInterfaceCategory = new List<ZeroInterfaceCategory>() { };
        public void Initialize(ReZeroOptions options)
        {
            var db = App.PreStartupDb;

            var categoryProvider = new InterfaceCategoryProvider(zeroInterfaceCategory);
            categoryProvider.Set();
            if (!db!.Queryable<ZeroInterfaceCategory>().Any())
                db!.Insertable(zeroInterfaceCategory).ExecuteCommand();


            var interfaceListProvider = new InterfaceListProvider(zeroInterfaceList);
            interfaceListProvider.Set(); 
            if (!db!.Queryable<ZeroInterfaceList>().Any())
                db!.Insertable(zeroInterfaceList).ExecuteCommand();
        } 
    }
}
