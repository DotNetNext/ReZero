using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    public partial class InterfaceCategoryProvider
    {
        List<ZeroInterfaceCategory> zeroInterfaceCategory = new List<ZeroInterfaceCategory>() { };
        public InterfaceCategoryProvider(List<ZeroInterfaceCategory> zeroInterfaceCategory) 
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
                it.ParentId = null; 
            }));

            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id1;
                it.Name = TextHandler.GetInterfaceCategoryText(Id1);
                it.ParentId = Id;
                it.Url = "/rezero/index.html";
            }));
        }

        private void SetProjectManager()
        {
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id300;
                it.Name = TextHandler.GetInterfaceCategoryText(Id300);
                it.ParentId = Id;
            }));  
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id300002;
                it.Name = TextHandler.GetInterfaceCategoryText(Id300002);
                it.ParentId = Id300;
            }));
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id300003;
                it.Name = TextHandler.GetInterfaceCategoryText(Id300003);
                it.ParentId = Id300;
            }));
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id300004;
                it.Name = TextHandler.GetInterfaceCategoryText(Id300004);
                it.ParentId = Id300;
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
                it.Id = Id200001;
                it.Name = TextHandler.GetInterfaceCategoryText(Id200001);
                it.ParentId = Id200; 
                it.Url = "/rezero/dynamic_interface.html";
            }));
            //internal interface
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id100;
                it.Name = TextHandler.GetInterfaceCategoryText(Id100);
                it.ParentId = Id;
            }));
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id100001;
                it.Name = TextHandler.GetInterfaceCategoryText(Id100001);
                it.ParentId = Id100;
            }));
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id100002;
                it.Name = TextHandler.GetInterfaceCategoryText(Id100002);
                it.ParentId = Id100;
            }));
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id100003;
                it.Name = TextHandler.GetInterfaceCategoryText(Id100003);
                it.ParentId = Id100;
            }));
        } 

        private static ZeroInterfaceCategory GetNewItem(Action<ZeroInterfaceCategory> action)
        {
            var result= new ZeroInterfaceCategory()
            {
                  IsInitialized=true
            };
            action(result);
            return result;
        } 
    } 

}
