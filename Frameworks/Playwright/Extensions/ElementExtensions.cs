using Microsoft.Playwright;

namespace TestFramework_NET.Frameworks.Playwright.Extensions
{
    internal static class ElementExtensions
    {
        /// <summary>
        /// Select a random option from a dropdown list.
        /// </summary>
        internal static async Task SelectByRandomValueAsync(this ILocator locator)
        {
            var options = locator.Locator("option");
            var randomIndex = new Random().Next(0, await options.CountAsync());
            await options.Nth(randomIndex).ScrollIntoViewIfNeededAsync();
            await options.Nth(randomIndex).ClickAsync();
        }

        /// <summary>
        /// Make the element visible by setting its display style to 'block'.
        /// </summary>
        internal static async Task MakeVisibleAsync(this ILocator element)
            => await element.EvaluateAsync("el => el.style.display = 'block'");

        //=> Click and wait for response to complete.
    }
}
