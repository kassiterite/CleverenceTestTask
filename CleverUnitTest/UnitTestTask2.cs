using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CleverTestTask2;
using System.Threading;

namespace CleverUnitTest
{
    [TestClass]
    public class UnitTestTask2
    {
        void SomeMethod(object s, EventArgs e)
        {
            Thread.Sleep(1000);
        }

        AsyncCaller GetAsyncCaller()
        {
            EventHandler h = new EventHandler(SomeMethod);
            AsyncCaller ac = new AsyncCaller(h);
            return ac;
        }

        [TestMethod]
        public void TestException()
        {
            Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => GetAsyncCaller().InvokeAsync(-1, null, EventArgs.Empty), "Should give an ArgumentOutOfRangeException");
        }

        [TestMethod]
        [DataRow(1000)]
        [DataRow(5000)]
        [DataRow(9000)]
        public void TestMillisecondsAmountIsGreaterOrEquals(int milliseconds)
        {
            Assert.IsTrue(GetAsyncCaller().InvokeAsync(milliseconds, null, EventArgs.Empty), "Should be true");
        }
        [TestMethod]
        [DataRow(1)]
        [DataRow(500)]
        [DataRow(900)]
        public void MuptipleTestOnBelowMillisecondsAmount(int milliseconds)
        {
            Assert.IsFalse(GetAsyncCaller().InvokeAsync(milliseconds, null, EventArgs.Empty), "Should be false");
        }
    }
}
