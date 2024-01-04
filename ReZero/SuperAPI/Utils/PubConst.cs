using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    public class PubConst
    {
        public const string Ui_TreeChild = "TreeChild";
        public const string Ui_TreeUrlFormatId = "{Id}";

        public const string Orm_TableDefaultPreName = "t"; 
        public const string Orm_DataBaseNameDTO = "DataBaseName";
        public const string Orm_InterfaceCategroyNameDTO = "InterfaceCategroyName";
        public readonly static string Orm_TableDefaultMasterTableShortName = Orm_TableDefaultPreName + 0;
        public const string Orm_ApiParameterJsonArray = "json array";

        public const string Namespace_ResultService = "ReZero.SuperAPI.Items.";
          
        public readonly static Random Common_Random = new Random();

        public const string CacheKey_Type = "ReZero_Type_{0}";
    }
}
