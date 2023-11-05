using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero
{
    public class UserService
    {
        public void Initialize(ReZeroOptions options)
        {
            InitUser();
        }

        private void InitUser()
        {
            App.Db.Insertable(new Zero_UserInfo()
            {
                Id = 1,
                IsMasterAdmin = true,
                Password = "admin",
                Username = Encryption.Encrypt("123456"),
                SortId = -1,
                CreatorId=1,
                Creator= "admin",
                EasyDescription= "default password 123456"
            }).ExecuteCommand();
        }
    }
}
