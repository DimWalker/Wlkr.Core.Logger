namespace Wlkr.Core.Logger.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Example example = new Example();
            example.IntentLogger();
            example.StaticLogger();
            //Assert.Fail();
        }
    }
}