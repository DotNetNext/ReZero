using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReZero.SuperAPI
{
    public partial class InterfaceCategoryInitializerProvider
    {
        List<ZeroInterfaceCategory> zeroInterfaceCategory = new List<ZeroInterfaceCategory>() { };
        public InterfaceCategoryInitializerProvider(List<ZeroInterfaceCategory> zeroInterfaceCategory)
        {
            this.zeroInterfaceCategory = zeroInterfaceCategory;
        }

        internal void Set()
        {
            SetIndexAndRoot();
            SetInterfaceManager();
            SetProjectManager();
        }

        private void SetIndexAndRoot()
        {
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id;
                it.Name = TextHandler.GetInterfaceCategoryText(Id);
                it.ParentId = Id-1;
            }));

            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id1;
                it.Name = TextHandler.GetInterfaceCategoryText(Id1);
                it.ParentId = Id;
                it.Url = "/rezero/index.html";
                it.Icon = "mdi mdi-home";
            }));
        }

        private void SetProjectManager()
        {
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id300;
                it.Name = TextHandler.GetInterfaceCategoryText(Id300);
                it.ParentId = Id;
                //it.Url= "/rezero/interface_manager.html";
            }));
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id300002;
                it.Name = TextHandler.GetInterfaceCategoryText(Id300002);
                it.ParentId = Id300;
                it.Url= "/rezero/interface_categroy.html";
            }));
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id300003;
                it.Name = TextHandler.GetInterfaceCategoryText(Id300003);
                it.ParentId = Id300;
                it.Url= "/rezero/database_manager.html";
            }));
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id300001;
                it.Name = TextHandler.GetInterfaceCategoryText(Id300001);
                it.ParentId = Id300;
                it.Url="/rezero/entity_manager.html";
            }));
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id300006;
                it.Name = TextHandler.GetInterfaceCategoryText(Id300006);
                it.ParentId = Id300;
                it.Url = "/rezero/interface_manager.html";
            }));
        }

        private void SetInterfaceManager()
        {
            //Dyanamic interface
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id200;
                it.Name = TextHandler.GetInterfaceCategoryText(Id200);
                it.ParentId = Id;

            }));
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id200100;
                it.Name = TextHandler.GetInterfaceCategoryText(Id200100);
                it.ParentId = Id200;
                it.Url = "/rezero/dynamic_interface.html?InterfaceCategoryId=" + Id200100;
            }));
            zeroInterfaceCategory.Last().IsInitialized = false;

            //internal interface
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id100;
                it.Name = TextHandler.GetInterfaceCategoryText(Id100);
                it.ParentId = Id;
            }));
            //zeroInterfaceCategory.Add(GetNewItem(it =>
            //{
            //    it.Id = Id100001;
            //    it.Name = TextHandler.GetInterfaceCategoryText(Id100001);
            //    it.ParentId = Id100;
            //    it.Url = "/rezero/internal_interface.html";
            //}));
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id100002;
                it.Name = TextHandler.GetInterfaceCategoryText(Id100002);
                it.ParentId = Id100;
                it.Url = "/rezero/internal_interface.html?InterfaceCategoryId=" + Id100002;
            }));
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id100003;
                it.Name = TextHandler.GetInterfaceCategoryText(Id100003);
                it.ParentId = Id100;
                it.Url = "/rezero/internal_interface.html?InterfaceCategoryId=" + Id100003;
            }));
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id100004;
                it.Name = TextHandler.GetInterfaceCategoryText(Id100004);
                it.ParentId = Id100;
                it.Url = "/rezero/internal_interface.html?InterfaceCategoryId=" + Id100004;
            }));
        }

        private static ZeroInterfaceCategory GetNewItem(Action<ZeroInterfaceCategory> action)
        {
            var result = new ZeroInterfaceCategory()
            {
                IsInitialized = true,
                IsDeleted=false
            };
            action(result);
            return result;
        }
    }

}
