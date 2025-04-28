using Microsoft.Playwright;

namespace TestFramework_NET.Frameworks.Playwright.Extensions.WebElements
{
    internal static class SelectExtensions
    {
        internal static async Task SelectByRandomValueAsync(this ILocator locator)
        {
            var options = locator.Locator("option");
            var randomIndex = new Random().Next(0, await options.CountAsync());
            await options.Nth(randomIndex).ScrollIntoViewIfNeededAsync();
            await options.Nth(randomIndex).ClickAsync();
        }
    }
}
