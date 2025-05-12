using FluentAssertions;
using TestFramework_NET.Common.Helpers;
using TestFramework_NET.Common.Models;
using TestFramework_NET.Frameworks.Playwright;
using TestFramework_NET.TestProject.DemoQA.Data.Models;
using TestFramework_NET.TestProject.DemoQA.T_Playwright.Pages;
using TestFramework_NET.TestProject.DemoQA.T_Playwright.Pages.Components;

namespace TestFramework_NET.TestProject.DemoQA.T_Playwright.Tests
{
    public class FormsTests_SetUp : SetUpPageTest
    {
        private readonly string _settingsFilePath = "TestProject\\DemoQA\\settings.json";

        [SetUp]
        public async Task Setup()
        {
            SettingsModel settings = JsonHelper.ObjectFromFile<SettingsModel>(_settingsFilePath);
            await Page.GotoAsync(settings.BaseUrl);
        }

        [Test]
        public async Task CheckFormNecessaryData()
        {
            // Arrange
            StudentModel studentData = new()
            {
                FullName = "FirstName LastName",
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
            StudentModel studentSavedData = await formPage.GetDataFromModalAsync();

            // Assert
            studentData.Should().NotBeEquivalentTo(studentSavedData);
        }
    }
}
