using OpenQA.Selenium;
using TestFramework_NET.Frameworks.Selenium.Extensions.WebElements;
using TestFramework_NET.TestProject.UI_DemoQA.Data;

namespace TestFramework_NET.TestProject.UI_DemoQA.T_Selenium.Pages
{
    internal class FormPage(IWebDriver _driver)
    {
        private IWebElement InputFirstName => _driver.FindElement(By.Id("firstName"));
        private IWebElement InputLastName => _driver.FindElement(By.Id("lastName"));
        private IList<IWebElement> RadioGender => _driver.FindElements(By.XPath("//input[@name='gender']"));
        private IWebElement InputMobile => _driver.FindElement(By.Id("userNumber"));
        private IWebElement ButtonSubmit => _driver.FindElement(By.Id("submit"));
        private IWebElement ModalBox => _driver.FindElement(By.XPath("//div[@class='modal-body']"));
        private IWebElement StudentFullName => ModalBox.FindElement(By.XPath("//tr[td[text()='Student Name']]/td[2]"));
        private IWebElement StudentGender => ModalBox.FindElement(By.XPath("//td[text()='Gender']/../td[2]"));
        private IWebElement StudentPhone => ModalBox.FindElement(By.XPath("//tr[td[text()='Mobile']]/td[2]"));

        internal FormPage FillNecessaryData(StudentFormModel studentData)
        {
            InputFirstName.ScrollAndSendKeys(studentData.GetFirstName());
            InputLastName.ScrollAndSendKeys(studentData.GetLastName());
            if (studentData.Gender != null)
                RadioGender.Where(x => x.GetAttribute("value") == studentData.Gender)
                    .First()
                    .FindElement(By.XPath("..//label"))
                    .ScrollAndClick();
            if (studentData.Mobile != null)
                InputMobile.ScrollAndSendKeys(studentData.Mobile);

            return this;
        }

        internal FormPage SubmitForm()
        {
            ButtonSubmit.ScrollAndClick();

            return this;
        }

        internal StudentFormModel GetDataFromModal()
            => new()
            {
                FullName = StudentFullName.Text,
                Gender = StudentGender.Text,
                Mobile = StudentPhone.Text
            };
    }
}
