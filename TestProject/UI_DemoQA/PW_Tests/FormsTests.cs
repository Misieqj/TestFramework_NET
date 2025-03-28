using FluentAssertions;
using Microsoft.Playwright.NUnit;
using TestFramework_NET.TestProject.UI_DemoQA.Data;
using TestFramework_NET.TestProject.UI_DemoQA.PW_Pages;
using TestFramework_NET.TestProject.UI_DemoQA.PW_Pages.Components;
using TestFramework_NET.Utilities;

namespace TestFramework_NET.TestProject.UI_DemoQA.PW_Tests
{
    public class FormsTests : PageTest
    {
        private readonly string _baseUrl = TestContext.Parameters.Get("UI_DemoQa") ?? string.Empty;

        [SetUp]
        public async Task Setup()
        {
            QLogger.PrintStartWithTcName();
            await Page.GotoAsync(_baseUrl);
        }

        [Test]
        public async Task CheckFormNecessaryData_Playwright()
        {
            // Arrange
            StudentFormModel studentData = new("First", "Last")
            {
                Gender = "Male",
                Mobile = "1234567890"
            };

            // Act
            MenuComponent menu = new(Page);
            await menu.ClickMenuPositionAsync(MenuComponent.Forms);
            await menu.ClickSubmenuPositionAsync(MenuComponent.Forms_PracticeForm);
            FormPage formPage = new(Page);
            await formPage.FillNecessaryDataAsync(studentData);
            await formPage.SubmitFormAsync();
            var studentSavedData = await formPage.GetDataFromModalAsync();

            // Assert
            studentData.Should().BeEquivalentTo(studentSavedData);
        }

        [TearDown]
        public void TearDowns()
        {
            QLogger.PrintEnd();
        }
    }
}
