namespace TestFramework_NET.TestProject.UI_DemoQA.Data
{
    internal class StudentFormModel(string firstName = "", string lastName = "")
    {
        private readonly string FirstName = firstName;
        private readonly string LastName = lastName;
        public string? FullName = $"{firstName} {lastName}";
        public string? Gender;
        public string? Mobile;

        public string GetFirstName()
            => FirstName;
        public string GetLastName()
            => LastName;
    }
}
