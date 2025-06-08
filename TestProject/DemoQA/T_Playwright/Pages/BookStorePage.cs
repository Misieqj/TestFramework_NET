using Microsoft.Playwright;
using TestFramework_NET.Common;

namespace TestFramework_NET.TestProject.DemoQA.T_Playwright.Pages
{
    internal class BookStorePage(IPage _page)
    {
        private ILocator Table => _page.Locator("//div[contains(@class,'ReactTable')]");
        private ILocator TableRows => Table.Locator("//div[@class='rt-tbody']//div[@role='row']");

        internal async Task<string> GetTableRowTextAsync(int index = 0)
        {
            if (await TableRows.CountAsync() == 0)
            {
                throw new Exception("No rows found in the table.");
            }
            else if (index < 0 || index >= await TableRows.CountAsync())
            {
                throw new Exception("Index is out of range. Please provide a valid index.");
            }
            var firstRow = TableRows.Nth(index);
            var result = await firstRow.TextContentAsync() ?? string.Empty;
            QLogger.PrintHeader("Text from row:");
            QLogger.Print(result);
                
            return result;
        }
    }
}
