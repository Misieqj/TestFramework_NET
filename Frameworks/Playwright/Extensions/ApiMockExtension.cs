using Microsoft.Playwright;

namespace TestFramework_NET.Frameworks.Playwright.Extensions
{
    internal static class ApiMockExtension
    {
        // => https://playwright.dev/dotnet/docs/mock

        internal static async Task BlockResponseAsync(this IBrowserContext context, string partUrl)
        {
            await context.RouteAsync($"**{partUrl}**", async route =>
            {
                await route.FulfillAsync(new() { Status = 401 });
            });
        }
    }
}
