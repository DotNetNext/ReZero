using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReZero.SuperAPI
{
    public class DefaultSuperApiAop : ISuperApiAop
    {
        public virtual Task OnExecutingAsync(InterfaceContext context)
        {
            return Task.FromResult(0);
        }

        public virtual Task OnExecutedAsync(InterfaceContext context)
        {
            return Task.FromResult(0);
        }

        public virtual Task OnErrorAsync(InterfaceContext context)
        {
            return Task.FromResult(0);
        }
    }
}
