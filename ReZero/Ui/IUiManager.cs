using System.Threading.Tasks;

namespace ReZero 
{
    public interface IUiManager
    {
        Task<string> GetHtmlAsync(string fileContent, string filePath);
        bool IsMasterPage(string fileContent);
    }
}