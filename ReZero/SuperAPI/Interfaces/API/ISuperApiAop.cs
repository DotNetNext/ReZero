using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReZero.SuperAPI 
{
    public interface ISuperApiAop 
    {
        Task OnExecutingAsync(InterfaceContext context);
        Task OnExecutedAsync(InterfaceContext context);
        Task OnErrorAsync(InterfaceContext context);
    }
}
