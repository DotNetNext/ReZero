using System.Net.Http;
using System.Threading.Tasks;

namespace ReZero.SuperAPI
{
    /// <summary>
    /// Default UI usage, not applicable for Vue front-end and back-end separation
    /// </summary>
    public interface IUiManager
    {
        /// <summary>
        /// Retrieves the HTML content asynchronously.
        /// </summary>
        /// <param name="fileContent">The content of the file.</param>
        /// <param name="filePath">The path of the file.</param>
        /// <param name="content">The HTTP context.</param>
        /// <returns>The HTML content as a string.</returns>
        Task<string> GetHtmlAsync(string fileContent, string filePath, Microsoft.AspNetCore.Http.HttpContext content);

        /// <summary>
        /// Checks if the file content represents a master page.
        /// </summary>
        /// <param name="fileContent">The content of the file.</param>
        /// <returns>True if the file content represents a master page, otherwise false.</returns>
        bool IsMasterPage(string fileContent);
    }
}