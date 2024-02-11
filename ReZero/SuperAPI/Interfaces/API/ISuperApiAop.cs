using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReZero.SuperAPI 
{
    public interface ISuperApiAop 
    {
        Task BeginActionAsync(DynamicInterfaceContext context);
        Task CommitActionAsync(DynamicInterfaceContext context);
        Task ErrorActionAsync(DynamicInterfaceContext context);
    }
}
