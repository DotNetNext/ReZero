using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReZero.SuperAPI
{
    public class DefaultSuperApiAop : ISuperApiAop
    {
        public virtual Task OnExecutingAsync(DynamicInterfaceContext context)
        {
            return Task.FromResult(0);
        }

        public virtual Task OnExecutedAsync(DynamicInterfaceContext context)
        {
            return Task.FromResult(0);
        }

        public virtual Task OnErrorAsync(DynamicInterfaceContext context)
        {
            return Task.FromResult(0);
        }
    }
}
