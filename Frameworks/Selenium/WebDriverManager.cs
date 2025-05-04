using OpenQA.Selenium;

namespace TestFramework_NET.Frameworks.Selenium
{
    internal static class WebDriverManager
    {
        internal static IWebDriver? Driver { get; set; }

        internal static IWebDriver GetDriver()
        {
            return Driver
                ?? throw new InvalidOperationException("WebDriver is not initialized. Please set WebDriverManager.Driver before use."); ;
        }
    }
}
