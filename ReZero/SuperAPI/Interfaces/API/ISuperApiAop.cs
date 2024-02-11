using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReZero.SuperAPI 
{
    public interface ISuperApiAop 
    {
        Task OnExecutingAsync(DynamicInterfaceContext context);
        Task OnExecutedAsync(DynamicInterfaceContext context);
        Task OnErrorAsync(DynamicInterfaceContext context);
    }
}
