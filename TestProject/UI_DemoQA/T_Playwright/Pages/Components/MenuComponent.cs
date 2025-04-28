using Microsoft.Playwright;

namespace TestFramework_NET.TestProject.UI_DemoQA.T_Playwright.Pages.Components
{
    internal class MenuComponent(IPage _page)
    {
        internal static string Elements => "Elements";
        internal static string Forms => "Forms";
        internal static string Forms_PracticeForm => "Practice Form";
        internal static string AlertsFrameWindows => "Alerts, Frame & Windows";
        internal static string Widgets => "Widgets";
        internal static string Interactions => "Interactions";
        internal static string BookStoreApplication => "Book Store Application";

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
