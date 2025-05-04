using Microsoft.Playwright;

namespace TestFramework_NET.Frameworks.Playwright.Helpers
{
    internal static class ScreenshotHelper
    {
        internal static async Task<string> DoScreenshotAsync(this IPage page)
        {
            var path = Path.Combine(TestContext.Parameters.Get("DirResults") ?? string.Empty,
                $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:ddHHmmss}.png");
            await page.ScreenshotAsync(new()
            {
                Path = path,
                FullPage = true,
            });

            return path;
        }
    }
}
