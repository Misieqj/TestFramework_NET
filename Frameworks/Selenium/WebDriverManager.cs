using OpenQA.Selenium;

namespace TestFramework_NET.Frameworks.Selenium
{
    internal static class WebDriverManager
    {
        public static IWebDriver? Driver { get; set; }
        
        public static IWebDriver GetDriver()
        {
            return Driver
                ?? throw new InvalidOperationException("WebDriver is not initialized. Please set WebDriverManager.Driver before use."); ;
        }
    }
}
