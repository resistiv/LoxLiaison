using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoxTester
{
    [TestClass]
    public class BlockTests
    {
        [TestMethod]
        public void EmptyTest()
        {
            string[] output = Tools.RunFile("..//..//..//Tests//block//empty.lox");
            Assert.AreEqual(1, output.Length);
            Assert.AreEqual("ok", output[0]);
        }

        [TestMethod]
        public void ScopeTest()
        {
            string[] output = Tools.RunFile("..//..//..//Tests//block//scope.lox");
            Assert.AreEqual(2, output.Length);
            Assert.AreEqual("inner", output[0]);
            Assert.AreEqual("outer", output[1]);
        }
    }
}
