using Microsoft.VisualStudio.TestTools.UnitTesting;
using CleverTestTask1;
using System.Threading.Tasks;


namespace CleverUnitTest
{
    [TestClass]
    public class UnitTestTask1
    {
        [TestMethod]
        public void ParallelTest()
        {
            Parallel.For(0, 1000, x =>
            {
                if (x % 2 == 0)
                {
                    Server.GetCount();
                    Server.AddToCount(1);
                }
                else
                    Server.AddToCount(1);
            });
            Assert.AreEqual(1000, Server.GetCount());
        }
    }
}