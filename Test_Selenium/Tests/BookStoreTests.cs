using FluentAssertions;
using OpenQA.Selenium.Chrome;
using TestFramework_NET.Common;
using TestFramework_NET.Common.Helpers;
using TestFramework_NET.Common.Models;
using TestFramework_NET.Data;
using TestFramework_NET.Test_Selenium.Pages;
using TestFramework_NET.Test_Selenium.Pages.Components;

namespace TestFramework_NET.Test_Selenium.Tests
{
    [TestFixture]
    public class BookStoreTests
    {
        private const string SettingsFilePath = "settings.json";
        private ChromeDriver _driver;

        [SetUp]
        public void Setup()
        {
            SettingsModel settings = JsonHelper.ObjectFromFile<SettingsModel>(SettingsFilePath);
            QLogger.PrintStartWithTcName();
            _driver = new ChromeDriver();
            _driver.Manage().Window.Size = new System.Drawing.Size(1680, 1024);
            _driver.Navigate().GoToUrl(settings.BaseUrl);
            WebDriverManager.Driver = _driver;
        }

        [Test]
        public void OpenBookStorePage()
        {
            // Arrange
            const string firstTitle = "Git Pocket Guide";

            // Act
            new MenuComponent(_driver)
                .ClickMenuPosition(MenuData.BookStoreApplication);
                //.ClickSubmenuPosition(MenuComponent.BookStoreApplication_BookStore);
            var bookStorePage = new BookStorePage(_driver).GetTableRowText();

            // Assert
            bookStorePage.Should().Contain(firstTitle);
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit(); // close browser
            _driver.Dispose(); // clean resources
            QLogger.PrintEnd();
        }
    }
}
