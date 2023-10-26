using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ReZero
{
    public interface IApi
    {
        bool IsApi(string url); 
        Task WriteAsync(HttpContext context);
    }
}