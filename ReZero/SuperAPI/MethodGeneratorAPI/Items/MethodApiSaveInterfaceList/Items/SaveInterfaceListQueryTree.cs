using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ReZero.SuperAPI 
{
    public class SaveInterfaceListQueryTree : BaseSaveInterfaceList, ISaveInterfaceList
    {
        public object SaveInterfaceList(SaveInterfaceListModel saveInterfaceListModel)
        {
            ZeroInterfaceList zeroInterfaceList = new ZeroInterfaceList();
            base.SetCommonProperties(zeroInterfaceList, saveInterfaceListModel);
            this.SetProperties(zeroInterfaceList, saveInterfaceListModel);
            var commonQuery = new SaveInterfaceListQueryCommon();
            commonQuery.zeroEntityInfo =App.Db.Queryable<ZeroEntityInfo>().Includes(it=>it.ZeroEntityColumnInfos).InSingle(zeroInterfaceList.DataModel!.TableId) ;
            commonQuery.SetWhere(saveInterfaceListModel, zeroInterfaceList);
            return base.SaveData(zeroInterfaceList);
        }
        private void SetProperties(ZeroInterfaceList zeroInterfaceList, SaveInterfaceListModel saveInterfaceListModel)
        {
            zeroInterfaceList.DataModel!.TreeParameter=new DataModelTreeParameter()
            {
                ChildPropertyName="Children",
                CodePropertyName  = saveInterfaceListModel.TreeCode,
                ParentCodePropertyName=saveInterfaceListModel.TreeParentCode,
                RootValue =saveInterfaceListModel.TreeRootParentValue
            };  
            zeroInterfaceList.DataModel!.DefaultParameters = new List<DataModelDefaultParameter>()
            {
                new DataModelDefaultParameter(){
                    FieldOperator=FieldOperatorType.Equal,
                    Name="RootPk",
                    ValueType=typeof(string).Name, 
                    Description=TextHandler.GetCommonText("根目录主键", "Root primary key")
                }
            };
        }
    }
}
