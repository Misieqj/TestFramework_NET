using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace TestFramework_NET.Test_Selenium.Helpers
{
    internal class DownloadDirHelper
    {
        internal static readonly string DownloadTempDir;

        static DownloadDirHelper()
        {
            DownloadTempDir = CreateTempDownloadDir();
        }

        internal static void AddToChromeOptions(ChromeOptions options)
        {
            options.AddUserProfilePreference("download.default_directory", DownloadTempDir);
            options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddUserProfilePreference("safebrowsing.enabled", true);
            options.AddUserProfilePreference("safebrowsing.disable_download_protection", true);
        }

        internal static void AddToFirefoxOptions(FirefoxOptions options)
        {
            options.SetPreference("browser.download.folderList", 2); // 2 = custom folder
            options.SetPreference("browser.download.dir", DownloadTempDir);
            options.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/pdf,application/octet-stream"); // MIME types
            options.SetPreference("pdfjs.disabled", true);

        }

        internal static void AddToEdgeOptions(EdgeOptions options)
        {
            options.AddUserProfilePreference("download.default_directory", DownloadTempDir);
            options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddUserProfilePreference("profile.default_content_settings.popups", 0);
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
