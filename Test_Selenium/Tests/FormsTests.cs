using FluentAssertions;
using OpenQA.Selenium.Chrome;
using TestFramework_NET.Common;
using TestFramework_NET.Common.Helpers;
using TestFramework_NET.Common.Models;
using TestFramework_NET.Data.Models;
using TestFramework_NET.Test_Selenium.Pages.Components;
using TestFramework_NET.Test_Selenium.Pages;

namespace TestFramework_NET.Test_Selenium.Tests
{
    [TestFixture]
    public class FormsTests
    {
        private const string SettingsFilePath = "settings.json";
        private ChromeDriver _driver;

        [SetUp]
        public void Setup()
        {
            SettingsModel settings = JsonHelper.ObjectFromFile<SettingsModel>(SettingsFilePath);
            QLogger.PrintStartWithTcName();
            _driver = new ChromeDriver();
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
