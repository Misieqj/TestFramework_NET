using OpenQA.Selenium;
using TestFramework_NET.Frameworks.Selenium.Extensions;

namespace TestFramework_NET.TestProject.UI_DemoQA.T_Selenium.Pages.Components
{
    internal class MenuComponent(IWebDriver _driver)
    {
        internal static string Elements => "Elements";
        internal static string Forms => "Forms";
        internal static string Forms_PracticeForm => "Practice Form";
        internal static string AlertsFrameWindows => "Alerts, Frame & Windows";
        internal static string Widgets => "Widgets";
        internal static string Interactions => "Interactions";
        internal static string BookStoreApplication => "Book Store Application";

        private IWebElement MenuItem(string name)
            => _driver.FindElement(By.XPath($"//*[contains(text(), '{name}')]"));
        private IWebElement SubmenuItem(string name)
            => _driver.FindElement(By.XPath($"//ul/li/span[text()='{name}']"));

        internal MenuComponent ClickMenuPosition(string name)
        {
            MenuItem(name).ScrollAndClick();

            return this;
        }

        internal MenuComponent ClickSubmenuPosition(string name)
        {
            SubmenuItem(name).ScrollAndClick();

            return this;
        }
    }
}
