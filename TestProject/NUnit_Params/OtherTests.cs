using TestFramework_NET.Common;

namespace TestFramework_NET.TestProject.NUnit_Params
{
    internal class OtherTests
    {
        private static TestContext.PropertyBagAdapter TestProperties
            => TestContext.CurrentContext.Test.Properties;
        private readonly bool _trueResult = true;

        [SetUp]
        public void SetUp()
        {
            QLogger.PrintStartWithTcName();
        }

        [TearDown]
        public void TearDown()
        {
            QLogger.PrintEnd();
        }

        [Test]
        [Category("Tests Category")]
        [Property("KeyOfProperty", "ValueForKey")] // Key, Value
        [Description("Test with some atributes")]
        [Author("Misiek")]
        public void Test_ToCheckAtributes()
        {
            QLogger.Print($"Property    => {TestProperties.Get("KeyOfProperty")}");
            QLogger.Print($"Description => {TestProperties.Get("Description")}");
            QLogger.Print($"Author      => {TestProperties.Get("Author")}");

            Assert.That(_trueResult, Is.True);
        }

        [Test]
        [Ignore("Comments to put why this tests is ignore", Until = "2025-12-31")]
        public void Test_ToCheckIgnore()
        {
            QLogger.Print("This test is ignored");
        }

        [Test]
        [Repeat(2)]
        // https://docs.nunit.org/articles/nunit/writing-tests/attributes/repeat.html
        public void Test_ToCheckRepeat()
        {
            QLogger.Print("This test is repeat as many times as it's defined");
            QLogger.Print($"Repeat Count => {TestContext.CurrentContext.CurrentRepeatCount}");

            Assert.That(_trueResult, Is.True);
        }

        [Test]
        [Retry(4)]
        // https://docs.nunit.org/articles/nunit/writing-tests/attributes/retry.html
        // Output will be only for last try
        public void Test_ToCheckRetry()
        {
            QLogger.Print("This test is retry until pass, and output display only last try");
            var result = TestContext.CurrentContext.CurrentRepeatCount;
            QLogger.Print($"Repeat Count => {result}");

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        [TestCase(1, 2, 3, TestName = "Test no 1")]
        [TestCase(4, 5, 9)]
        public void Test_ToCheckParams(int a, int b, int result)
        {
            Assert.That(a + b, Is.EqualTo(result));
        }

        [TestCase(12, 3, ExpectedResult = 4)]
        [TestCase(12, 2, ExpectedResult = 6)]
        [TestCase(12, 4, ExpectedResult = 3)]
        public int DivideTest(int n, int d)
        {
            return n / d;
        }

        [TestCaseSource(nameof(TestData))]
        public void Tests_WithDataFrom_TestCaseData(int n, int d, int result)
        {
            var res = n / d;
            
            QLogger.Print($"Description => {TestProperties.Get("Description")}"); // will print "This test is ignored"
            QLogger.Print($"{res} {result}");

            Assert.That(res, Is.EqualTo(result));
        }

        private static readonly TestCaseData[] TestData =
        [
            // without SetName on the list there is name of this method
            new TestCaseData(12, 3, 4)
                .SetName($"{nameof(Tests_WithDataFrom_TestCaseData)} => 1")
                .SetDescription("Desc for first"),
            new TestCaseData(12, 2, 6)
                .SetName($"{nameof(Tests_WithDataFrom_TestCaseData)} => 2 we can use space :)")
                .SetDescription("Desc for second")
        ];
    }
}
