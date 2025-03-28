using Microsoft.Playwright;
using TestFramework_NET.TestProject.UI_DemoQA.Data;

namespace TestFramework_NET.TestProject.UI_DemoQA.PW_Pages
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

        internal async Task FillNecessaryDataAsync(StudentFormModel studentData)
        {
            await InputFirstName.FillAsync(studentData.GetFirstName());
            await InputLastName.FillAsync(studentData.GetLastName());
            if (studentData.Gender != null)
                await RadioGender.GetByText(studentData.Gender, new() { Exact = true }).ClickAsync();
            if (studentData.Mobile != null)
                await InputMobile.FillAsync(studentData.Mobile);
        }

        internal async Task SubmitFormAsync()
            => await ButtonSubmit.ClickAsync();

        internal async Task<StudentFormModel> GetDataFromModalAsync()
            => new StudentFormModel()
                {
                    FullName = await StudentFullName.TextContentAsync(),
                    Gender = await StudentGender.TextContentAsync(),
                    Mobile = await StudentPhone.TextContentAsync()
                };
    }
}
