using ReZero.DependencyInjection;
using ReZero.SuperAPI;
using SqlSugar;

namespace SuperAPITest.Controllers
{
    /// <summary>
    /// 动态接口
    /// </summary>
    [Api(200100, GroupName = "分组2")]
    public class UnitOfWorkController
    {
        //属性注入
        [DI]
        public ISqlSugarClient? db { get; set; }

        //工作单元，可以用自带的也可以重新写
        [UnitOfWork]
        public bool QueryTest() 
        {
            db!.Ado.ExecuteCommand("select 1 as id");
            return true;
        }
    }
}
