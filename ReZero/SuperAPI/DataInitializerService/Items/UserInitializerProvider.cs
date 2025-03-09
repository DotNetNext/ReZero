using System;
using System.Collections.Generic;
using System.Text; 

namespace ReZero.SuperAPI
{
    public partial class UserInitializerProvider
    {
        private SuperAPIOptions? _options;
        public void Initialize(SuperAPIOptions options)
        {
            _options = options ?? new SuperAPIOptions();
            InitUser();
        }

        private void InitUser()
        {
            var db = App.PreStartupDb;
            var defaltUser=db!.Queryable<ZeroUserInfo>().ClearFilter().Where(it => !it.IsInitialized)
                .Where(it => it.Id == 1).First();
            if (defaltUser != null) 
            {
                db.Deleteable<ZeroUserInfo>().Where(it => it.Id == 1).ExecuteCommand();
            }
            db!.Storageable(new ZeroUserInfo()
            {
                Id = 1,
                IsMasterAdmin = true,
                Password = Encryption.Encrypt("123456"),
                UserName = "admin",
                SortId = -1,
                IsInitialized=true,
                CreatorId = 1,
                Creator = "admin",
                EasyDescription = "default password 123456"
            }).ToStorage().AsInsertable.ExecuteCommand();
        }
    }
}
