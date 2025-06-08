using TestFramework_NET.Common;

namespace TestFramework_NET.Test_NUnitParams
{
    [TestFixture("First param for class")]
    [TestFixture("Second param for class")]
    internal class ClassParamsTests(string param)
    {
        // when using [TestFixture] attribute
        // when we use constructor we definem params like in OneTimeSetUp
        private readonly string _param = param;

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
        public void Test1()
        {
            QLogger.Print($"Param1 => {_param}");
        }
    }
}
