using Microsoft.Playwright;
using TestFramework_NET.TestProject.UI_DemoQA.Data.Models;

namespace TestFramework_NET.TestProject.UI_DemoQA.T_Playwright.Pages
{
    internal class FormPage(IPage _page)
    {
        private ILocator InputFirstName => _page.Locator("#firstName");
        private ILocator InputLastName => _page.Locator("#lastName"); //.Locator("#lastName");
        private ILocator RadioGender => _page.Locator($"//input[@name='gender']//..");
        private ILocator InputMobile => _page.GetByPlaceholder("Mobile Number");
        private ILocator ButtonSubmit => _page.GetByRole(AriaRole.Button, new() { Name = "Submit" });
        private ILocator ModalBox => _page.Locator("//div[@class='modal-body']");
        private ILocator StudentFullName => ModalBox.Locator("//tr[td[text()='Student Name']]/td").Last;
        private ILocator StudentGender => ModalBox.Locator("//td[text()='Gender']/../td").Last;
        private ILocator StudentPhone => ModalBox.Locator("//tr[td[text()='Mobile']]/td[2]");

        internal async Task FillNecessaryDataAsync(StudentModel student)
        {
            await InputFirstName.FillAsync(student.FullName.Split(" ").First());
            await InputLastName.FillAsync(student.FullName.Split(" ").Last());
            await RadioGender.GetByText(student.Gender, new() { Exact = true }).ClickAsync();
            await InputMobile.FillAsync(student.Mobile);
        }

        internal async Task SubmitFormAsync()
            => await ButtonSubmit.ClickAsync();

        internal async Task<StudentModel> GetDataFromModalAsync()
            => new()
                {
                    FullName = await StudentFullName.TextContentAsync() ?? string.Empty,
                    Gender = await StudentGender.TextContentAsync() ?? string.Empty,
                    Mobile = await StudentPhone.TextContentAsync() ?? string.Empty
                };
    }
}
