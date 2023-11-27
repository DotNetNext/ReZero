using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    public partial class IconInitializerProvider
    {  
        internal List<ZeroIconData> GetDatas()
        {
            List<ZeroIconData> datas = new List<ZeroIconData>();

            datas.Add(GetNewItem(it =>
            {
                it.Id = Id1;
                it.IconName = IconName1;
            }));

            datas.Add(GetNewItem(it =>
            {
                it.Id = Id2;
                it.IconName = IconName2;
            }));

            datas.Add(GetNewItem(it =>
            {
                it.Id = Id3;
                it.IconName = IconName3;
            }));

            datas.Add(GetNewItem(it =>
            {
                it.Id = Id4;
                it.IconName = IconName4;
            }));

            return datas;
        }
        private static ZeroIconData GetNewItem(Action<ZeroIconData> action)
        {
            var result = new ZeroIconData()
            {
                IsInitialized = true,
            };
            action(result);
            return result;
        }
    }
}
