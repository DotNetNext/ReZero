using ReZero.SuperAPI; 
namespace SuperAPITest.Controllers
{ 
    /// <summary>
    /// 动态接口
    /// </summary>
    [Api(200100, GroupName = "分组2")]
    public class MyApiController
    { 
        [ApiMethod("我是A方法")]
        public int A(int num,int num2) 
        {
            return num+num2;
        }

        [ApiMethod("我是B方法")]
        public string B(byte[] file)
        {
            return "文件长度"+ file.Length;
        } 

        [ApiMethod("我是C方法", HttpMethod = HttpType.Post)]
        public Object C(ClassA classA)
        {
            return classA;
        }

        [ApiMethod("我是D方法", HttpMethod = HttpType.Post)]
        public async Task<ClassA> D(ClassA classA)
        {
             await Task.Delay(1);
            return classA;
        }

        [ApiMethod("我是E方法")]
        public async Task<int?> E(int? num)
        {
            await Task.Delay(1);
            return num;
        }
    }

    public class ClassA 
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
