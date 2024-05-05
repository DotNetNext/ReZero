using ReZero.SuperAPI; 
namespace SuperAPITest.Controllers
{
 
    [Api(21000, Description ="哈哈")]
    public class MyApiController
    { 
        [ApiMethod()]
        public int A(int i) 
        {
            return 1;
        }
    }
}
