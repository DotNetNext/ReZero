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
            SetInterfaceManager();
            SetProjectManager();
        }

        private void SetProjectManager()
        {
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id2;
                it.Name = TextHandler.GetInterfaceCategoryText(Id2);
                it.ParentId = 0;
            }));
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id200002;
                it.Name = TextHandler.GetInterfaceCategoryText(Id200002);
                it.ParentId = Id2;
            }));
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id200003;
                it.Name = TextHandler.GetInterfaceCategoryText(Id200003);
                it.ParentId = Id2;
            }));
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id200004;
                it.Name = TextHandler.GetInterfaceCategoryText(Id200004);
                it.ParentId = Id2;
            }));
        }

      

        private void SetInterfaceManager()
        {
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id1;
                it.Name = TextHandler.GetInterfaceCategoryText(Id1);
                it.ParentId = 0;
            }));
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id100001;
                it.Name = TextHandler.GetInterfaceCategoryText(Id100001);
                it.ParentId = Id1;
            }));
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id100002;
                it.Name = TextHandler.GetInterfaceCategoryText(Id100002);
                it.ParentId = Id1;
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
