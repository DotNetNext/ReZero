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
            if (db != null)
            {
                App.PreStartupDb!.QueryFilter.ClearAndBackup();
                InitUser(options);
                InitInterfaceCategory(db);
                InitEntityInfo(db);
                InitInterfaceList(db);
                InitIcon();
                InitDatabase(db);
                InitSetting(db);
                UpgradeCompatibility(db);
                InitTempate(db);
                App.PreStartupDb!.QueryFilter.Restore();
            }
        }

        private void InitTempate(ISqlSugarClient? db)
        { 
            var entityTemplate = db!.Queryable<ZeroTemplate>().Where(it=>it.IsDeleted==false).First(it => it.TypeId==TemplateType.Entity);
            if (entityTemplate == null)
            {
                db!.Insertable(new ZeroTemplate()
                {
                    Title=TextHandler.GetCommonText("SqlSugar实体类默认模版", "SqlSugar template"),
                    TemplateContent=new MethodApi().ClassNameDefalutTemplateTemplate(),
                    TemplateContentStyle="csharp",
                    Url="c:\\models\\{0}.cs",
                    Creator = DataBaseInitializerProvider.UserName,
                    Id =SqlSugar.SnowFlakeSingle.Instance.NextId(),
                    TypeId=TemplateType.Entity,
                    IsDeleted=false,
                }).ExecuteCommand();
            }
        }

        /// <summary>
        /// Initializes the setting.
        /// </summary>
        /// <param name="db">The database client.</param>
        private void InitSetting(ISqlSugarClient? db)
        {
            var entityType = PubConst.Setting_EntityType;
            var importUnunderlineType = PubConst.Setting_ImportUnunderlineType;
            var entityExport = db!.Queryable<ZeroSysSetting>().First(it => it.ChildTypeId == entityType && it.TypeId == importUnunderlineType);
            if (entityExport == null)
            {
                db!.Insertable(new ZeroSysSetting()
                {
                    BoolValue = false,
                    ChildTypeId = entityType,
                    EasyDescription = TextHandler.GetCommonText("实体-导入实体是不是去掉下划线", "Entity-Importing entity is not without underline"),
                    TypeId = importUnunderlineType,
                    Creator = DataBaseInitializerProvider.UserName,
                    Id = DataBaseInitializerProvider.Id
                }).ExecuteCommand();
            }
        }

        /// <summary>
        /// Upgrades compatibility.
        /// </summary>
        /// <param name="db">The database client.</param>
        private static void UpgradeCompatibility(ISqlSugarClient? db)
        {
            db!.Updateable<ZeroInterfaceList>()
                .SetColumns(it => it.IsAttributeMethod == false)
                .Where(it => it.IsAttributeMethod == null)
                .ExecuteCommand();
            var list = db!.Queryable<ZeroInterfaceList>()
                 .Where(it => it.IsInitialized == false)
                 .Where(it => it.DatabaseId == null).ToList();
            foreach (var item in list)
            {
                if (item?.DataModel?.TableId > 0)
                {
                    var entity = db.Queryable<ZeroEntityInfo>().InSingle(item?.DataModel?.TableId);
                    item!.DatabaseId = entity.DataBaseId;
                    db.Updateable(item).ExecuteCommand();
                }
            }
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
