using OpenQA.Selenium;
using TestFramework_NET.TestProject.UI_DemoQA.Data;

namespace TestFramework_NET.TestProject.UI_DemoQA.S_Pages
{
    internal class FormPage(IWebDriver _driver)
    {
        private IWebElement InputFirstName => _driver.FindElement(By.Id("firstName"));
        private IWebElement InputLastName => _driver.FindElement(By.Id("lastName"));
        private IWebElement RadioGender => _driver.FindElement(By.XPath("//input[@name='gender']//.."));
        private IWebElement InputMobile => _driver.FindElement(By.Id("userNumber"));
        private IWebElement ButtonSubmit => _driver.FindElement(By.Id("submit"));
        private IWebElement ModalBox => _driver.FindElement(By.XPath("//div[@class='modal-body']"));
        private IWebElement StudentFullName => ModalBox.FindElement(By.XPath("//tr[td[text()='Student Name']]/td[2]"));
        private IWebElement StudentGender => ModalBox.FindElement(By.XPath("//td[text()='Gender']/../td[2]"));
        private IWebElement StudentPhone => ModalBox.FindElement(By.XPath("//tr[td[text()='Mobile']]/td[2]"));

        internal FormPage FillNecessaryData(StudentFormModel studentData)
        {
            InputFirstName.SendKeys(studentData.GetFirstName());
            InputLastName.SendKeys(studentData.GetLastName());
            if (studentData.Gender != null)
                RadioGender.FindElement(By.XPath($"//input[@name='gender']/../../*[label[contains(text(), '{studentData.Gender}')]]")).Click();
            if (studentData.Mobile != null)
                InputMobile.SendKeys(studentData.Mobile);

            return this;
        }

        internal FormPage SubmitForm()
        {
            ButtonSubmit.Click();

            return this;
        }

        internal StudentFormModel GetDataFromModal()
            => new StudentFormModel()
            {
                FullName = StudentFullName.Text,
                Gender = StudentGender.Text,
                Mobile = StudentPhone.Text
            };
    }
}
