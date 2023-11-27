using System.Threading.Tasks;

namespace ReZero
{
    public interface IDataService
    {
        Task<object> ExecuteAction(DataModel dataModel);
    }
}