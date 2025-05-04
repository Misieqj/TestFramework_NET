using System.Xml.Serialization;

namespace TestFramework_NET.TestProject.DemoQA.Data.Models
{
    [XmlRoot("Student")]
    public class StudentModel
    {
        public required string FullName { get; set; }
        public required string Gender { get; set; }
        public required string Mobile { get; set; }
    }
}
