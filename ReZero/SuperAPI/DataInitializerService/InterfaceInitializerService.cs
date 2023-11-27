using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    public class DataInitializerService
    {
        List<ZeroInterfaceList> zeroInterfaceList = new List<ZeroInterfaceList>() { };
        List<ZeroInterfaceCategory> zeroInterfaceCategory = new List<ZeroInterfaceCategory>() { };
        public void Initialize(SuperAPIOptions options)
        {
            var db = App.PreStartupDb;

            InitUser(options);
            InitInterfaceCategory(db);
            InitEntityInfo(db);
            InitInterfaceList(db);
            InitIcon();
        }

        private void InitEntityInfo(ISqlSugarClient? db)
        {
            var entity =new  EntityInfoInitializerProvider();
            var datas = entity.GetDatas();
            db!.UpdateNav(datas,new UpdateNavRootOptions() { IsInsertRoot=true }).Include(x=>x.ZeroEntityColumnInfos).ExecuteCommand();
        }

        private static void InitIcon()
        {
            var icon = new IconInitializerProvider();
        }

        private void InitInterfaceList(ISqlSugarClient? db)
        {
            db!.Deleteable<ZeroInterfaceList>().Where(it => it.IsInitialized).ExecuteCommand();
            var interfaceListProvider = new InterfaceListInitializerProvider(zeroInterfaceList);
            interfaceListProvider.Set();
            db!.Storageable(zeroInterfaceList).ExecuteCommand();
        }

        private void InitInterfaceCategory(ISqlSugarClient? db)
        {
            db!.Deleteable<ZeroInterfaceCategory>().Where(it => it.IsInitialized).ExecuteCommand();
            var categoryProvider = new InterfaceCategoryInitializerProvider(zeroInterfaceCategory);
            categoryProvider.Set();
            db!.Storageable(zeroInterfaceCategory).ExecuteCommand();
        }

        private static void InitUser(SuperAPIOptions options)
        {
            UserInitializerProvider userInitializerProvider = new UserInitializerProvider();
            userInitializerProvider.Initialize(options);
        }
    }
}
