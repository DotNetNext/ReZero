using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero
{
    public class InternalApiManager
    {
        List<ZeroInterfaceList> zeroInterfaceList = new List<ZeroInterfaceList>();
        public void Initialize(ReZeroOptions options)
        {
            var db = App.PreStartupDb;
            if (!db!.Queryable<ZeroInterfaceList>().Any())
                db!.Insertable(zeroInterfaceList).ExecuteCommand();
        }

        public void AddProject()
        {

        }
        public static void AddDataBase()
        {

        }
    }
}
