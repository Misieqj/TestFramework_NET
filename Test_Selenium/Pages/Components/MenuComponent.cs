using OpenQA.Selenium;
using TestFramework_NET.Test_Selenium.Extensions;

namespace TestFramework_NET.Test_Selenium.Pages.Components
{
    internal class MenuComponent(IWebDriver _driver)
    {
        private IWebElement MenuItem(string name) => _driver.FindElement(By.XPath($"//*[contains(text(), '{name}')]"));
        private IWebElement SubmenuItem(string name) => _driver.FindElement(By.XPath($"//ul/li/span[text()='{name}']"));

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
