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
            }));
        }

        private void SetProjectManager()
        {
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id20;
                it.Name = TextHandler.GetInterfaceCategoryText(Id20);
                it.ParentId = Id;
            })); 
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id200002;
                it.Name = TextHandler.GetInterfaceCategoryText(Id200002);
                it.ParentId = Id20;
            }));
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id200003;
                it.Name = TextHandler.GetInterfaceCategoryText(Id200003);
                it.ParentId = Id20;
            }));
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id200004;
                it.Name = TextHandler.GetInterfaceCategoryText(Id200004);
                it.ParentId = Id20;
            }));
        }

      

        private void SetInterfaceManager()
        {
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id10;
                it.Name = TextHandler.GetInterfaceCategoryText(Id10);
                it.ParentId = Id;
            }));
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id100001;
                it.Name = TextHandler.GetInterfaceCategoryText(Id100001);
                it.ParentId = Id10;
            }));
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id100002;
                it.Name = TextHandler.GetInterfaceCategoryText(Id100002);
                it.ParentId = Id10;
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
