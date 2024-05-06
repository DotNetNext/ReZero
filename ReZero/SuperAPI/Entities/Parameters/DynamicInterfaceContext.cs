using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    public class InterfaceContext
    {
        public InterfaceType InterfaceType { get; internal set; }
        public HttpContext? HttpContext { get; internal set; }
        public DataModel? DataModel { get; internal set; }
        public ZeroInterfaceList? InterfaceInfo { get; internal set; }
        public Exception? Exception { get; internal set; }
        public ServiceProvider? ServiceProvider { get; internal set; }

        public void AttachClaimToHttpContext(string claimKey, object claimValue)
        {
            if (DataModel != null&& !DataModel.ClaimList!.ContainsKey(claimKey))
            {
                DataModel!.ClaimList!.Add(claimKey, claimValue);
            }
        }
    }
    public enum InterfaceType 
    {
        DynamicApi,
        SystemApi,
    }
}
