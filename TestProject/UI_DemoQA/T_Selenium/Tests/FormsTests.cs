using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TestFramework_NET.Common.Models;
using TestFramework_NET.Common;
using TestFramework_NET.TestProject.UI_DemoQA.T_Selenium.Pages;
using TestFramework_NET.TestProject.UI_DemoQA.T_Selenium.Pages.Components;
using TestFramework_NET.Frameworks.Selenium;
using TestFramework_NET.TestProject.UI_DemoQA.Data.Models;
using TestFramework_NET.Common.Helpers;

namespace TestFramework_NET.TestProject.UI_DemoQA.T_Selenium.Tests
{
    [TestFixture]
    public class FormsTests
    {
        private readonly string _filePath = "TestProject\\UI_DemoQA\\settings.json";
        private IWebDriver _driver;
        //private ChromeDriver _driver; // if we want to set some special ChromeOptions()

        [SetUp]
        public void Setup()
        {
            SettingsModel settings = JsonHelper.LoadJson<SettingsModel>(_filePath);
            QLogger.PrintStartWithTcName();
            _driver = new ChromeDriver(); // we can also use FirefoxDriver or EdgeDriver
            _driver.Manage().Window.Size = new System.Drawing.Size(1280, 720);
            _driver.Navigate().GoToUrl(settings.BaseUrl);
            WebDriverManager.Driver = _driver;
        }

        [Test]
        public void CheckFormNecessaryData()
        {
            // Arrange
            StudentModel studentData = new()
            {
                FullName = "FirstName LastName",
                Gender = "Male",
                Mobile = "1234567890"
            };

            // Act
            new MenuComponent(_driver)
                .ClickMenuPosition(MenuComponent.Forms)
                .ClickSubmenuPosition(MenuComponent.Forms_PracticeForm);
            var studentSavedData = new FormPage(_driver)
                .FillNecessaryData(studentData)
                .SubmitForm()
                .GetDataFromModal();

            // Assert
            studentData.Should().BeEquivalentTo(studentSavedData);
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
