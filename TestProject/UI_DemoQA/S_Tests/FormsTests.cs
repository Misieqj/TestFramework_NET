using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TestFramework_NET.TestProject.UI_DemoQA.Data;
using TestFramework_NET.TestProject.UI_DemoQA.S_Pages;
using TestFramework_NET.TestProject.UI_DemoQA.S_Pages.Components;
using TestFramework_NET.Utilities;

namespace TestFramework_NET.TestProject.UI_DemoQA.S_Tests
{
    [TestFixture]
    public class FormsTests
    {
        private readonly string _baseUrl = TestContext.Parameters.Get("UI_DemoQa") ?? string.Empty;
        private IWebDriver _driver;
        //private ChromeDriver _driver; // if we want to set some special ChromeOptions()

        [SetUp]
        public void Setup()
        {
            QLogger.PrintStartWithTcName();
            _driver = new ChromeDriver(); // we can also use FirefoxDriver or EdgeDriver
            _driver.Manage().Window.Size = new System.Drawing.Size(1280, 720);
            _driver.Navigate().GoToUrl(_baseUrl);
        }

        [Test]
        public void CheckFormNecessaryData_Selenium()
        {
            // Arrange
            StudentFormModel studentData = new("First", "Last")
            {
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
