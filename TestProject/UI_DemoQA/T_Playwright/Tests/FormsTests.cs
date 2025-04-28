using FluentAssertions;
using Microsoft.Playwright.NUnit;
using TestFramework_NET.Common;
using TestFramework_NET.Common.Models;
using TestFramework_NET.TestProject.UI_DemoQA.Data;
using TestFramework_NET.TestProject.UI_DemoQA.T_Playwright.Pages;
using TestFramework_NET.TestProject.UI_DemoQA.T_Playwright.Pages.Components;

namespace TestFramework_NET.TestProject.UI_DemoQA.T_Playwright.Tests
{
    public class FormsTests : PageTest
    {
        private readonly string _filePath = "TestProject\\UI_DemoQA\\settings.json";

        [SetUp]
        public async Task Setup()
        {
            SettingsModel settings = JsonHelper.LoadJsonAsync<SettingsModel>(_filePath);
            QLogger.PrintStartWithTcName();
            await Page.GotoAsync(settings.BaseUrl);
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
