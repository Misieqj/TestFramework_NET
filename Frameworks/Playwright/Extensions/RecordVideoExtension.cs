using Microsoft.Playwright;
using TestFramework_NET.Common;

namespace TestFramework_NET.Frameworks.Playwright.Extensions
{
    internal static class RecordVideoExtension
    {
        // => https://playwright.dev/dotnet/docs/videos
        // Set RecordVideoDir in context
        // Add in TearDown to save only on failure

        internal static async Task StopRecordVideoAsync(this IPage page)
        {
            #pragma warning disable CS8602 // Dereference of a possibly null reference.
            string videoPath = await page.Video.PathAsync();
            var newPath = Path.Combine("videos", $"{TestContext.CurrentContext.Test.Name}.webm");
            File.Move(videoPath, newPath);
            QLogger.PrintAttachmentInfo(newPath, "Test record path");
        }
    }
}
