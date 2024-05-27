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
            SetInterfaceDocument();
            SetInterfaceManager();
            //SetCodeBuilder();
            //SetDataDocument();
        }

        private void SetDataDocument()
        {
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = DataDocumentRootId;
                it.Name = TextHandler.GetInterfaceCategoryText(DataDocumentRootId);
                it.ParentId = Id;
                it.Icon = "mdi mdi-file-document-box";
                it.SortId = 500;
            }));
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = DataDocumentManagerId;
                it.Name = TextHandler.GetInterfaceCategoryText(DataDocumentManagerId);
                it.ParentId = DataDocumentRootId;
                it.Url = "/rezero/data_document.html";
            }));
        }
        private void SetCodeBuilder()
        {
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = CodeBuilderRootId;
                it.Name = TextHandler.GetInterfaceCategoryText(CodeBuilderRootId);
                it.ParentId = Id; 
                it.Icon = "mdi mdi-codepen";
                it.SortId = 499;
            }));
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = CodeBuilderManagerId;
                it.Name = TextHandler.GetInterfaceCategoryText(CodeBuilderManagerId);
                it.ParentId = CodeBuilderRootId;
                it.Url = "/rezero/code_builder.html";
            }));
        }

        private void SetIndexAndRoot()
        {
            if (SuperAPIModule._apiOptions!.UiOptions!.ShowNativeApiDocument == false) 
            {
                return;
            }
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id;
                it.Name = TextHandler.GetInterfaceCategoryText(Id);
                it.ParentId = Id-1;
                it.SortId = 0;
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

        private void SetInterfaceManager()
        {
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id300;
                it.Name = TextHandler.GetInterfaceCategoryText(Id300);
                it.ParentId = Id;
                it.SortId = 3;
                //it.Url= "/rezero/interface_manager.html";
            }));
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id300002;
                it.Name = TextHandler.GetInterfaceCategoryText(Id300002);
                it.ParentId = Id300;
                it.SortId = 0;
                it.Url= "/rezero/interface_categroy.html";
            }));
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id300003;
                it.Name = TextHandler.GetInterfaceCategoryText(Id300003);
                it.ParentId = Id300;
                it.SortId = 1;
                it.Url= "/rezero/database_manager.html";
            }));
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id300001;
                it.Name = TextHandler.GetInterfaceCategoryText(Id300001);
                it.ParentId = Id300;
                it.Url="/rezero/entity_manager.html";
                it.SortId = 2;
            }));
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id300006;
                it.Name = TextHandler.GetInterfaceCategoryText(Id300006);
                it.ParentId = Id300;
                it.Url = "/rezero/interface_manager.html";
                it.SortId = 3;
            }));
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id300007;
                it.Name = TextHandler.GetInterfaceCategoryText(Id300007);
                it.ParentId = Id300;
                it.Url = "/rezero/authorization.html";
                it.SortId = 4;
            }));
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = 300008;
                it.Name = TextHandler.GetInterfaceCategoryText(300008);
                it.ParentId = Id300;
                it.Url = "/rezero/template.html";
                it.SortId = 5;
            }));
        }

        private void SetInterfaceDocument()
        {
            //Dyanamic interface
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id200;
                it.Name = TextHandler.GetInterfaceCategoryText(Id200);
                it.ParentId = Id;
                it.Icon = "mdi mdi-palette";
                it.SortId = 1;

            }));
            zeroInterfaceCategory.Add(GetNewItem(it =>
            {
                it.Id = Id200100;
                it.Name = TextHandler.GetInterfaceCategoryText(Id200100);
                it.ParentId = Id200;
                it.Url = "/rezero/dynamic_interface.html?InterfaceCategoryId=" + Id200100;
            }));
            zeroInterfaceCategory.Last().IsInitialized = false;

            SystemDocment();
        }

        private void SystemDocment()
        {
            if (SuperAPIModule._apiOptions!.UiOptions!.ShowSystemApiDocument == true)
            {

                zeroInterfaceCategory.Add(GetNewItem(it =>
                {
                    it.Id = Id100;
                    it.Name = TextHandler.GetInterfaceCategoryText(Id100);
                    it.ParentId = Id;
                    it.SortId = 999;
                    it.Icon = "mdi mdi-file-outline";
                }));
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
