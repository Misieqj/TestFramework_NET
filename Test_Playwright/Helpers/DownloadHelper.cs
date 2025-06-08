using Microsoft.Playwright;

namespace TestFramework_NET.Test_Playwright.Helpers
{
    internal static class DownloadHelper
    {
        //=> https://playwright.dev/dotnet/docs/api/class-page#page-wait-for-download
        //=> https://playwright.dev/dotnet/docs/api/class-download

        internal static readonly string DownloadTempDir;

        static DownloadHelper()
        {
            DownloadTempDir = CreateTempDownloadDir();
        }

        internal static async Task DownloadFile(IPage page, ILocator downloadBtn)
        {
            var download = await page.RunAndWaitForDownloadAsync(
                async () => await downloadBtn.ClickAsync()
                );
            var filePath = Path.Combine(DownloadTempDir, download.SuggestedFilename);
            await download.SaveAsAsync(filePath);
        }

        internal static void DeleteTempDownloadDir()
        {
            if (Directory.Exists(DownloadTempDir))
                Directory.Delete(DownloadTempDir, true);
        }

        private static string CreateTempDownloadDir()
        {
            var tempDir = Path.Combine(
                Path.GetTempPath(),
                $"Downloads_{DateTime.Now:yyMMddHHmmss}");
            Directory.CreateDirectory(DownloadTempDir);

            return tempDir;
        }
    }
}
