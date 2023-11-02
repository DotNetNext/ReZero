using System.Threading.Tasks;

namespace ReZero
{
    public interface IDataManager
    {
        Task<object> ExecuteAction(DataModel dataModel);
    }
}