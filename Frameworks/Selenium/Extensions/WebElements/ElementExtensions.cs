using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace TestFramework_NET.Frameworks.Selenium.Extensions.WebElements
{
    internal static class ElementExtensions
    {
        internal static void ScrollAndClick(this IWebElement element, int xScroll = 150)
        {
            new Actions(WebDriverManager.GetDriver())
                .MoveToElement(element)
                .ScrollByAmount(0, xScroll)
                .Perform();
            element.Click();
        }

        internal static void ScrollAndSendKeys(this IWebElement element, string text, int xScroll = 150)
        {
            new Actions(WebDriverManager.GetDriver())
                .MoveToElement(element)
                .ScrollByAmount(0, xScroll)
                .Perform();
            element.SendKeys(text);
        }
    }
}
