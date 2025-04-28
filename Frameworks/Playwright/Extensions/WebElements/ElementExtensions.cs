using Microsoft.Playwright;

namespace TestFramework_NET.Frameworks.Playwright.Extensions.WebElements
{
    internal static class ElementExtensions
    {
        internal static async Task WaitTillValueChangeAsync(this ILocator locator, string oldValue)
        {
            // Method that will wait for value change
            await locator.ClickAsync();
        }
    }
}
