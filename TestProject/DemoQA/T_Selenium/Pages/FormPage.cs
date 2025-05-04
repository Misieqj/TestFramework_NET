using OpenQA.Selenium;
using TestFramework_NET.Frameworks.Selenium.Extensions;
using TestFramework_NET.TestProject.DemoQA.Data.Models;

namespace TestFramework_NET.TestProject.DemoQA.T_Selenium.Pages
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

        internal FormPage FillNecessaryData(StudentModel student)
        {
            InputFirstName.ScrollAndSendKeys(student.FullName.Split(" ").First());
            InputLastName.ScrollAndSendKeys(student.FullName.Split(" ").Last());
            RadioGender.Where(x => x.GetAttribute("value") == student.Gender)
                .First()
                .FindElement(By.XPath("..//label"))
                .ScrollAndClick();
            InputMobile.ScrollAndSendKeys(student.Mobile);

            return this;
        }

        internal FormPage SubmitForm()
        {
            ButtonSubmit.ScrollAndClick();

            return this;
        }

        internal StudentModel GetDataFromModal()
            => new()
                {
                    FullName = StudentFullName.Text,
                    Gender = StudentGender.Text,
                    Mobile = StudentPhone.Text
                };
    }
}
