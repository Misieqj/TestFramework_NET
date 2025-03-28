using Microsoft.Playwright;
using TestFramework_NET.Utilities;

namespace TestFramework_NET.Frameworks.Playwright.Extensions
{
    internal static class ScreenshotExtension
    {
        internal static async Task DoScreenshotAsync(this IPage page)
        {
            if (bool.Parse(TestContext.Parameters.Get("Screenshot") ?? "true"))
            {
                var pathDefine = Path.Combine(
                    DateTime.Now.ToString("ddHHmmss"),
                    TestContext.CurrentContext.Test.Name + ".png");
                await page.ScreenshotAsync(new()
                {
                    Path = pathDefine,
                    FullPage = true,
                });

                QLogger.Print(pathDefine);
                QLogger.PrintAttachmentInfo(pathDefine, "Screenshot path");
            }
        }
    }
}
