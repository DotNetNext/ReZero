using ReZero.DependencyInjection;
using ReZero.SuperAPI;
namespace SuperAPITest.Controllers
{
    [Api(200100,GroupName = "分组1")]
    public class MyApiWithIocController
    {
        //属性注入
        [DI]
        public MyService? MyService { get; set; }

        [ApiMethod("我是A方法")]
        public int A(int num, int num2)
        {
            return this.MyService!.CalculateSum(num, num2);
        }
    }
    public class MyService : IScopeContract
    {
        public int CalculateSum(int num, int num2)
        {
            return num2 + num;
        }
    }
}
