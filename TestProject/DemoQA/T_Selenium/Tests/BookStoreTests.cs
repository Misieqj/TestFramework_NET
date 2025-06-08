using FluentAssertions;
using OpenQA.Selenium.Chrome;
using TestFramework_NET.Common;
using TestFramework_NET.Common.Helpers;
using TestFramework_NET.Common.Models;
using TestFramework_NET.Frameworks.Selenium;
using TestFramework_NET.TestProject.DemoQA.T_Selenium.Pages;
using TestFramework_NET.TestProject.DemoQA.T_Selenium.Pages.Components;

namespace TestFramework_NET.TestProject.DemoQA.T_Selenium.Tests
{
    [TestFixture]
    public class BookStoreTests
    {
        private readonly string _settingsFilePath = "TestProject\\DemoQA\\settings.json";
        private ChromeDriver _driver;

        [SetUp]
        public void Setup()
        {
            SettingsModel settings = JsonHelper.ObjectFromFile<SettingsModel>(_settingsFilePath);
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
                .ClickMenuPosition(MenuComponent.BookStoreApplication);
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
