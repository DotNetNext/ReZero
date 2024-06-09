using ReZero.SuperAPI;
using System.Security.Policy;
namespace SuperAPITest.Controllers
{ 
    /// <summary>
    /// 动态接口
    /// </summary>
    [Api(200100, GroupName = "分组1",Url= "/api/MyApiController")]
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

        [ApiMethod("我是C方法", HttpMethod = HttpType.Get)]
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
