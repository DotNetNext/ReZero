using ReZero.SuperAPI; 
namespace SuperAPITest.Controllers
{ 
    /// <summary>
    /// 动态接口
    /// </summary>
    [Api(200100)]
    public class MyApiController
    { 
        [ApiMethod("我是A方法")]
        public int A(int num) 
        {
            return num;
        }

        [ApiMethod("我是B方法")]
        public string B(byte[] file)
        {
            return "文件长度"+ file.Length;
        } 

        [ApiMethod("我是C方法")]
        public Object C(ClassA classA)
        {
            return classA;
        }
    }

    public class ClassA 
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
