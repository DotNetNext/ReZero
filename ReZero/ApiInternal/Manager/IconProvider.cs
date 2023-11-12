using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    internal class IconProvider
    {
        internal const int Id1 = 1;
        internal const int Id2 = 2;
        internal const int Id3 = 3;
        internal const int Id4 = 4;

        private const string IconName1 = "mdi mdi-home";
        private const string IconName2 = "mdi polymer";
        private const string IconName3 = "mdi mdi-book-open";
        private const string IconName4 = "mdi-arrange-send-backward";

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
