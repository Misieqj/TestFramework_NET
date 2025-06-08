using Microsoft.Playwright;

namespace TestFramework_NET.Test_Playwright.Helpers
{
    internal static class RecordVideoHelper
    {
        //=> https://playwright.dev/dotnet/docs/videos

        internal static async Task<string> StopRecordVideoAsync(this IPage page)
        {
            if (page.Video == null)
                return string.Empty;

            string videoPath = await page.Video.PathAsync();
            var path = Path.Combine(TestContext.Parameters.Get("DirResults") ?? string.Empty,
                $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:ddHHmmss}{Path.GetExtension(videoPath)}");
            File.Move(videoPath, path);

            return path;
        }

        internal static async Task DeleteRecordedVideoAsync(this IPage page)
        {
            if (page.Video == null)
                return;

            string videoPath = await page.Video.PathAsync();
            if (File.Exists(videoPath))
                File.Delete(videoPath);
        }
    }
}
