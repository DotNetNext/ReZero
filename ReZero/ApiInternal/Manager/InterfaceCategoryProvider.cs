using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    internal partial class InterfaceCategoryProvider
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
                it.Name = "项目管理";
                it.ParentId = 0;
            }));
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id200002;
                it.Name = "项目分类";
                it.ParentId = Id2;
            }));
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id200003;
                it.Name = "数据库管理";
                it.ParentId = Id2;
            }));
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id200004;
                it.Name = "实体表管理";
                it.ParentId = Id2;
            }));
        }

      

        private void SetInterfaceManager()
        {
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id1;
                it.Name = "接口管理";
                it.ParentId = 0;
            }));
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id100001;
                it.Name = "接口分类";
                it.ParentId = Id1;
            }));
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id100002;
                it.Name = "接口列表";
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
