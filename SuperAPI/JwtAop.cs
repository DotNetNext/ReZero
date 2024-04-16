using ReZero.SuperAPI;

namespace SuperAPITest
{
    public class JwtAop : DefaultSuperApiAop
    {
        public override Task OnExecutingAsync(InterfaceContext context)
        { 
            return base.OnExecutingAsync(context);
        }
        public override Task OnExecutedAsync(InterfaceContext context)
        {
            return base.OnExecutedAsync(context);
        }
        public override Task OnErrorAsync(InterfaceContext context)
        {
            return base.OnErrorAsync(context);
        }
    }
}
