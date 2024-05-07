using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    public class PubConst
    {
        public const string Ui_TreeChild = "TreeChild";
        public const string Ui_TreeUrlFormatId = "{Id}";

        public const string Orm_SqlFalseString = " 1=2 ";
        public const string Orm_TableDefaultPreName = "t"; 
        public const string Orm_DataBaseNameDTO = "DataBaseName";
        public const string Orm_InterfaceCategroyNameDTO = "InterfaceCategroyName";
        public readonly static string Orm_TableDefaultMasterTableShortName = Orm_TableDefaultPreName + 0;
        public const string Orm_ApiParameterJsonArray = "json array";
        public const string Orm_SubqueryKey = "`SqlFunc`.`Key`.['010203']";
        public const string Orm_WhereValueTypeClaimKey = "Orm_WhereValueTypeClaimKey['16125']";
        public const string Orm_ClaimkeyName = "claimkey";

        public const string Namespace_ResultService = "ReZero.SuperAPI.Items.";
          
        public readonly static Random Common_Random = new Random();

        public const string CacheKey_Type = "ReZero_Type_{0}";

        public const string DataSource_ActionTypeGroupName_QueryCN = "查询";
        public const string DataSource_ActionTypeGroupName_QueryEN = "Query";
        public const string DataSource_ActionTypeGroupName_InsertCN = "插入";
        public const string DataSource_ActionTypeGroupName_InsertEN = "Insert";
        public const string DataSource_ActionTypeGroupName_UpdateCN = "更新";
        public const string DataSource_ActionTypeGroupName_UpdateEN = "Update";
        public const string DataSource_ActionTypeGroupName_DeleteCN = "删除";
        public const string DataSource_ActionTypeGroupName_DeleteEN = "Delete";
        public const string DataSource_ActionTypeGroupName_DDLCN = "库表维护";
        public const string DataSource_ActionTypeGroupName_DDLEN = "DLL";
        public const string DataSource_ActionTypeGroupName_MyMethodCN = "自定义方法";
        public const string DataSource_ActionTypeGroupName_MyMethodEN = "My method";

        public const string Jwt_TokenUrl = "/api/rezero/token";
        public const string Jwt_PageUrl = "/rezero/authorization.html";
    }
}
