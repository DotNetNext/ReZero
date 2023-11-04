using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    public class  UserService
    {
        public void Init(ReZeroOptions options) 
        {
            InitUser();
        }

        private void InitUser()
        {
            App.Db.Insertable(new Zero_UserInfo()
            {

            }).ExecuteCommand();
        }
    }
}
