using Microsoft.Playwright;

namespace TestFramework_NET.Test_Playwright.Pages.Components
{
    internal class MenuComponent(IPage _page)
    {
        private ILocator MenuItem(string name)
            => _page.Locator($"//*[contains(text(), '{name}')]");
        private ILocator SubmenuItem(string name)
            => _page.Locator($"//ul/li/span[text()='{name}']");

        internal async Task ClickMenuPositionAsync(string name)
            => await MenuItem(name).ClickAsync();

        internal async Task ClickSubmenuPositionAsync(string name)
            => await SubmenuItem(name).ClickAsync();
    }
}
