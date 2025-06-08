using FluentAssertions;
using TestFramework_NET.Common.Helpers;
using TestFramework_NET.Common.Models;
using TestFramework_NET.Data;
using TestFramework_NET.Data.Models;
using TestFramework_NET.Test_Playwright.Pages;
using TestFramework_NET.Test_Playwright.Pages.Components;

namespace TestFramework_NET.Test_Playwright.Tests
{
    public class FormsTests_SetUp : SetUpPageTest
    {
        private const string SettingsFilePath = "settings.json";

        [SetUp]
        public async Task Setup()
        {
            SettingsModel settings = JsonHelper.ObjectFromFile<SettingsModel>(SettingsFilePath);
            await Page.GotoAsync(settings.BaseUrl);
        }

        [Test]
        public async Task NegativeCheckFormNecessaryData()
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
            await menu.ClickMenuPositionAsync(MenuData.Forms);
            await menu.ClickSubmenuPositionAsync(MenuData.Forms_PracticeForm);
            FormPage formPage = new(Page);
            await formPage.FillNecessaryDataAsync(studentData);
            await formPage.SubmitFormAsync();
            StudentModel studentSavedData = await formPage.GetDataFromModalAsync();

            // Assert
            studentData.Should().NotBeEquivalentTo(studentSavedData);
        }
    }
}
