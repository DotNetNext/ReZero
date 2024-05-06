using ReZero.DependencyInjection;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace ReZero.SuperAPI
{
    public class UnitOfWork : Attribute, IUnitOfWork
    { 
        public ISqlSugarClient?  db { get; set; }
        public virtual void BeginTran()
        {
            db!.AsTenant().BeginTran();
        }
        public virtual void CommitTran()
        {
            db!.AsTenant().CommitTran(); ;
        }
        public virtual void RollbackTran()
        {
            db!.AsTenant().RollbackTran(); ;
        } 
    }
}
