using System.Threading.Tasks;

namespace ReZero.SuperAPI
{
    public interface IDataService
    {
        Task<object> ExecuteAction(DataModel dataModel);
    }
}