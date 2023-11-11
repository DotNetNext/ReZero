using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ReZero
{
    internal class DefaultUiManager
    {
        private readonly string originalFileContent;
        private readonly string originalFilePath;
        private readonly string masterPagePlaceholder = "@@master_page.html";
        private readonly string masterPageFolder = "template";
        private readonly string masterPageFileName = "master_page.html";
        private readonly string layoutContentPlaceholder = "@@lyear-layout-content";

        public DefaultUiManager(string fileContent, string filePath)
        {
            this.originalFileContent = fileContent;
            this.originalFilePath = filePath;
        }

        public async Task<string> GetHtmlAsync()
        {
            var modifiedContent = originalFileContent.Replace(masterPagePlaceholder, "");
            var masterPagePath = Path.Combine(Path.GetDirectoryName(originalFilePath), masterPageFolder, masterPageFileName);
            var masterPageHtml = await File.ReadAllTextAsync(masterPagePath);
            modifiedContent = masterPageHtml.Replace(layoutContentPlaceholder, modifiedContent);
            return modifiedContent;
        }

        public bool IsMasterPage(string fileContent)
        {
            return fileContent.Contains(masterPagePlaceholder);
        }
    }
}