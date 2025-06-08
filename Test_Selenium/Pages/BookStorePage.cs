using OpenQA.Selenium;
using TestFramework_NET.Common;
using TestFramework_NET.Test_Selenium.Extensions;

namespace TestFramework_NET.Test_Selenium.Pages
{
    internal class BookStorePage(IWebDriver _driver)
    {
        private IWebElement Table => _driver.WaitAndFindElement(By.XPath("//div[contains(@class,'ReactTable')]"));
        private IEnumerable<IWebElement> TableRows => Table.FindElements(By.XPath("//div[@class='rt-tbody']//div[@role='row']"));

        internal string GetTableRowText(int index = 0)
        {
            var firstRow = TableRows.ElementAtOrDefault(index);
            if (firstRow != null)
            {
                var result = firstRow.Text;
                QLogger.PrintHeader("Text from row:");
                QLogger.Print(result);
                return result;
            }

            return string.Empty;
        }
    }
}
