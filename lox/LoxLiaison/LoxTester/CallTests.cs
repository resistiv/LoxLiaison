using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoxTester
{
    [TestClass]
    public class CallTests
    {
        [TestMethod]
        public void BoolTest()
        {
            string[] output = Tools.RunFile("..//..//..//Tests//call//bool.lox");
            Assert.AreEqual(2, output.Length);
            Assert.AreEqual("Can only call functions and classes.", output[0]);
            Assert.AreEqual("[line 1]", output[1]);
        }

        [TestMethod]
        public void NilTest()
        {
            string[] output = Tools.RunFile("..//..//..//Tests//call//nil.lox");
            Assert.AreEqual(2, output.Length);
            Assert.AreEqual("Can only call functions and classes.", output[0]);
            Assert.AreEqual("[line 1]", output[1]);
        }

        [TestMethod]
        public void NumTest()
        {
            string[] output = Tools.RunFile("..//..//..//Tests//call//num.lox");
            Assert.AreEqual(2, output.Length);
            Assert.AreEqual("Can only call functions and classes.", output[0]);
            Assert.AreEqual("[line 1]", output[1]);
        }

        [TestMethod]
        public void ObjectTest()
        {
            string[] output = Tools.RunFile("..//..//..//Tests//call//object.lox");
            Assert.AreEqual(2, output.Length);
            Assert.AreEqual("Can only call functions and classes.", output[0]);
            Assert.AreEqual("[line 4]", output[1]);
        }

        [TestMethod]
        public void StringTest()
        {
            string[] output = Tools.RunFile("..//..//..//Tests//call//string.lox");
            Assert.AreEqual(2, output.Length);
            Assert.AreEqual("Can only call functions and classes.", output[0]);
            Assert.AreEqual("[line 1]", output[1]);
        }
    }
}
