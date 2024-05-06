using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    /// <summary>
    /// Initialize system data
    /// </summary>
    public class DataInitializerService
    {
        List<ZeroInterfaceList> zeroInterfaceList = new List<ZeroInterfaceList>() { };
        List<ZeroInterfaceCategory> zeroInterfaceCategory = new List<ZeroInterfaceCategory>() { };
        public void Initialize(SuperAPIOptions options)
        {
            var db = App.PreStartupDb;
            App.PreStartupDb!.QueryFilter.ClearAndBackup();
            InitUser(options);
            InitInterfaceCategory(db);
            InitEntityInfo(db);
            InitInterfaceList(db);
            InitIcon();
            InitDatabase(db);
            db!.Updateable<ZeroInterfaceList>()
                .SetColumns(it => it.IsAttributeMethod == false)
                .Where(it => it.IsAttributeMethod==null)
                .ExecuteCommand();
            App.PreStartupDb!.QueryFilter.Restore();
        }

        /// <summary>
        /// Initializes the database.
        /// </summary>
        /// <param name="db">The database client.</param>
        private void InitDatabase(ISqlSugarClient? db)
        {
            db!.Storageable(new ZeroDatabaseInfo()
            {
                Connection = db.CurrentConnectionConfig.ConnectionString,
                DbType = db.CurrentConnectionConfig.DbType,
                IsInitialized = true,
                Name = TextHandler.GetCommonText("Rezero", "Rezero database"),
                Creator = DataBaseInitializerProvider.UserName,
                Id = DataBaseInitializerProvider.Id

            }).ExecuteCommand();
        }

        /// <summary>
        /// Initializes the entity information.
        /// </summary>
        /// <param name="db">The database client.</param>
        private void InitEntityInfo(ISqlSugarClient? db)
        {
            var entity = new EntityInfoInitializerProvider();
            var datas = entity.GetDatas(); 
            db!.UpdateNav(datas, new UpdateNavRootOptions() { IsInsertRoot = true }).Include(x => x.ZeroEntityColumnInfos).ExecuteCommand();
        }

        /// <summary>
        /// Initializes the icon.
        /// </summary>
        private static void InitIcon()
        {
            var icon = new IconInitializerProvider();
        }

        /// <summary>
        /// Initializes the interface list.
        /// </summary>
        /// <param name="db">The database client.</param>
        private void InitInterfaceList(ISqlSugarClient? db)
        {
            db!.Deleteable<ZeroInterfaceList>().Where(it => it.IsInitialized).ExecuteCommand();
            var interfaceListProvider = new InterfaceListInitializerProvider(zeroInterfaceList);
            interfaceListProvider.Set();
            db!.Storageable(zeroInterfaceList).ExecuteCommand();
        }

        /// <summary>
        /// Initializes the interface category.
        /// </summary>
        /// <param name="db">The database client.</param>
        private void InitInterfaceCategory(ISqlSugarClient? db)
        {
            db!.Deleteable<ZeroInterfaceCategory>().Where(it => it.IsInitialized).ExecuteCommand();
            var categoryProvider = new InterfaceCategoryInitializerProvider(zeroInterfaceCategory);
            categoryProvider.Set();
            db!.Storageable(zeroInterfaceCategory).ExecuteCommand();
        }

        /// <summary>
        /// Initializes the user.
        /// </summary>
        /// <param name="options">The SuperAPI options.</param>
        private static void InitUser(SuperAPIOptions options)
        {
            UserInitializerProvider userInitializerProvider = new UserInitializerProvider();
            userInitializerProvider.Initialize(options);
        }
    }
}
