using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    internal class BuiltInApi
    {
        ZeroInterfaceList zeroInterfaceList = new ZeroInterfaceList();
        public void Init(ReZeroOptions options) 
        {
            App.Db.Insertable(zeroInterfaceList).ExecuteCommand();
        }

        public  void AddProject() 
        {
            ZeroInterfaceList zeroInterface = new ZeroInterfaceList()
            {
                Id = 0,
                Name="获取项目列表",
                ActionType = ActionType.Query_Common,
                
            };
        }
        public static void AddDataBase()
        {

        }
    }
}
