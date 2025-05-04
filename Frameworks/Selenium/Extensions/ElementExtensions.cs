using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace TestFramework_NET.Frameworks.Selenium.Extensions
{
    internal static class ElementExtensions
    {
        /// <summary>
        /// Select a random option from a dropdown list.
        /// </summary>
        internal static void ScrollAndClick(this IWebElement element, int xScroll = 150)
        {
            new Actions(WebDriverManager.GetDriver())
                .MoveToElement(element)
                .ScrollByAmount(0, xScroll)
                .Perform();
            element.Click();
        }

        /// <summary>
        /// Send keys to an element after scrolling it into view.
        /// </summary>
        internal static void ScrollAndSendKeys(this IWebElement element, string text, int xScroll = 150)
        {
            new Actions(WebDriverManager.GetDriver())
                .MoveToElement(element)
                .ScrollByAmount(0, xScroll)
                .Perform();
            element.SendKeys(text);
        }

        /// <summary>
        /// Make the element visible by setting its display style to 'block'.
        /// </summary>
        internal static void MakeVisible(this IWebElement element)
        {
            var js = (IJavaScriptExecutor)WebDriverManager.GetDriver();
            js.ExecuteScript("arguments[0].style.display = 'block';", element);
        }
    }
}
