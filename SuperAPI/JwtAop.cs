using ReZero.SuperAPI;
using System.Diagnostics;

namespace SuperAPITest
{
    public class JwtAop : DefaultSuperApiAop
    {
        public async override Task OnExecutingAsync(InterfaceContext aopContext)
        {
            //// 尝试验证JWT  
            //var authenticateResult = await aopContext.HttpContext.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
            //if (!authenticateResult.Succeeded)
            //{
            //    // JWT验证失败，返回401 Unauthorized或其他适当的响应  
            //    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //    await context.Response.WriteAsync("Unauthorized");
            //    return;
            //}
            await base.OnExecutingAsync(aopContext);
        }
        public async override Task OnExecutedAsync(InterfaceContext aopContext)
        {
            await base.OnExecutedAsync(aopContext);
        }
        public async override Task OnErrorAsync(InterfaceContext aopContext)
        {
            await base.OnErrorAsync(aopContext);
        }
    }
}
