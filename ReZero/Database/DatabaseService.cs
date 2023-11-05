using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero
{
    public class UserInitializerService
    {
        private ReZeroOptions? _options;
        public void Initialize(ReZeroOptions options)
        {
            _options = options ?? new ReZeroOptions();
            InitUser();
        }

        private void InitUser()
        {
            var db = App.PreStartupDb;
            db!.Storageable(new ZeroUserInfo()
            {
                Id = 1,
                IsMasterAdmin = true,
                Password =Encryption.Encrypt("123456"),
                UserName = "admin",
                SortId = -1,
                CreatorId=1,
                Creator= "admin",
                EasyDescription= "default password 123456"
            }).ToStorage().AsInsertable.ExecuteCommand();
        }
    }
}
