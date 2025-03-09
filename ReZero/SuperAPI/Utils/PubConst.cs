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
        public readonly static string Common_BlankSpace = " ";
        public readonly static string Common_R = "\r";
        public readonly static string Common_N = "\n";
        public readonly static string Common_Project = "{project}";
        public readonly static string Common_ProjectRegex= @"\{project\}";
        public readonly static string Common_Format0 = "{0}";
        public readonly static string Common_Format1 = "{1}";
        public readonly static string Common_Q = "?";
        public readonly static string Common_ArrayKey = "[]";
        public readonly static string Common_RegexWKey = "_____asfalflsgayfaggugglgfgyydfyiypqombbgjoosbds____";

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
        public const string DataSource_ApplicationJson = "application/json; charset=utf-8";
        public const string DataSource_ActionTypeGroupName_InsertOrUpdateCN = "插入或更新";
        public const string DataSource_ActionTypeGroupName_InsertOrUpdateEN = "Insert or update";

        public const string Jwt_TokenUrl = "/api/rezero/token";
        public const string Jwt_GetJwtInfo = "/api/rezero/getuserinfo";
        public const string Jwt_PageUrl = "/rezero/authorization.html";

        public const string ErrorCode_001 = "[001]";

        public const int Setting_EntityType = 1;
        public const int Setting_ImportUnunderlineType = 1;
        public const int Setting_EnableLoginType = 2;


        public const string InitApi_SystemCommon = "SystemCommon";
        public const string InitApi_RootUrl = "/PrivateReZeroRoute/100003/";
        public const string InitApi_SystemSaveConfig = "/PrivateReZeroRoute/100003/SaveLoginConfig";
        public const string InitApi_SystemGetInitConfig = "/PrivateReZeroRoute/100003/GetLoginConfig";
    }
}
