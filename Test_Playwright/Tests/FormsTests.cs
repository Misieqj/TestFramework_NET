using FluentAssertions;
using Microsoft.Playwright.NUnit;
using TestFramework_NET.Common;
using TestFramework_NET.Common.Helpers;
using TestFramework_NET.Common.Models;
using TestFramework_NET.Data.Models;
using TestFramework_NET.Test_Playwright.Pages;
using TestFramework_NET.Test_Playwright.Pages.Components;

namespace TestFramework_NET.Test_Playwright.Tests
{
    public class FormsTests : PageTest
    {
        private const string SettingsFilePath = "settings.json";
        private const string StudentXmlPath = "Data\\student.xml";
        private const string StudentJsonPath = "Data\\student.json";

        [SetUp]
        public async Task Setup()
        {
            SettingsModel settings = JsonHelper.ObjectFromFile<SettingsModel>(SettingsFilePath);
            QLogger.PrintStartWithTcName();
            await Page.GotoAsync(settings.BaseUrl);
        }

        [TearDown]
        public void TearDowns()
        {
            QLogger.PrintEnd();
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
            studentData.Should().BeEquivalentTo(studentSavedData);
        }

        [Test]
        public async Task CheckFormNecessaryData_XML()
        {
            // Arrange
            StudentModel studentData = XmlHelper.LoadXml<StudentModel>(StudentXmlPath);

            // Act
            MenuComponent menu = new(Page);
            await menu.ClickMenuPositionAsync(MenuComponent.Forms);
            await menu.ClickSubmenuPositionAsync(MenuComponent.Forms_PracticeForm);
            FormPage formPage = new(Page);
            await formPage.FillNecessaryDataAsync(studentData);
            await formPage.SubmitFormAsync();
            StudentModel studentSavedData = await formPage.GetDataFromModalAsync();

            // Assert
            studentData.Should().BeEquivalentTo(studentSavedData);
        }

        [Test]
        public async Task CheckFormNecessaryData_JSON()
        {
            // Arrange
            StudentModel studentData = JsonHelper.ObjectFromFile<StudentModel>(StudentJsonPath);

            // Act
            MenuComponent menu = new(Page);
            await menu.ClickMenuPositionAsync(MenuComponent.Forms);
            await menu.ClickSubmenuPositionAsync(MenuComponent.Forms_PracticeForm);
            FormPage formPage = new(Page);
            await formPage.FillNecessaryDataAsync(studentData);
            await formPage.SubmitFormAsync();
            StudentModel studentSavedData = await formPage.GetDataFromModalAsync();

            // Assert
            studentData.Should().BeEquivalentTo(studentSavedData);
        }
    }
}
