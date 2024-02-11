using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReZero.SuperAPI
{
    public class DefaultSuperApiAop : ISuperApiAop
    {
        public Task BeginActionAsync(DynamicInterfaceContext context)
        {
            return Task.FromResult(0);
        }

        public Task CommitActionAsync(DynamicInterfaceContext context)
        {
            return Task.FromResult(0);
        }

        public Task ErrorActionAsync(DynamicInterfaceContext context)
        {
            return Task.FromResult(0);
        }
    }
}
