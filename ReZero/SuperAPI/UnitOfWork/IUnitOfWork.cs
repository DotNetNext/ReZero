using SqlSugar;

namespace ReZero.SuperAPI
{
    public interface IUnitOfWork
    {
        ISqlSugarClient? db { get; set; }

        void BeginTran(); 
        void CommitTran();
        void RollbackTran();
    }
}