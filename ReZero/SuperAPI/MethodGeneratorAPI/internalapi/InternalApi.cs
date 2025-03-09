using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;

namespace ReZero.SuperAPI 
{
    [Api(InterfaceCategoryInitializerProvider.Id100003)]
    internal class InternalInitApi
    {
        [ApiMethod(nameof(InternalInitApi.SaveConfig), GroupName = PubConst.InitApi_SystemCommon, Url =PubConst.InitApi_SystemSaveConfig)]
        public bool SaveConfig(string id) 
        {
            return true;
        }

        [ApiMethod(nameof(InternalInitApi.GetInitConfig), GroupName = PubConst.InitApi_SystemCommon, Url = PubConst.InitApi_SystemGetInitConfig)]
        public object GetInitConfig()
        {
            return new 
            { 
                Jwt = App.InitReZeroOptions?.SuperApiOptions.InterfaceOptions.Jwt
            };
        }
    }
}
