using ReZero.SuperAPI;
using ReZero.SuperAPI.ApiDynamic.Entities;
namespace SuperAPITest.Controllers
{
    [DynamicApi(21000)]
    public class MyApiController
    {
        [DynamicMethod()]
        public int A(int i) 
        {
            return 1;
        }
    }
}
