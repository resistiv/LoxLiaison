using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoxTester
{
    [TestClass]
    public class ClassTests
    {
        [TestMethod]
        public void EmptyTest()
        {
            string[] output = Tools.RunFile("..//..//..//Tests//class//empty.lox");
            Assert.AreEqual(1, output.Length);
            Assert.AreEqual("Foo", output[0]);
        }

        [TestMethod]
        public void InheritSelfTest()
        {
            string[] output = Tools.RunFile("..//..//..//Tests//class//inherit_self.lox");
            Assert.AreEqual(1, output.Length);
            Assert.AreEqual("[line 1] Error at 'Foo': A class can't inherit from itself.", output[0]);
        }

        [TestMethod]
        public void InheritedMethodTest()
        {
            string[] output = Tools.RunFile("..//..//..//Tests//class//inherited_method.lox");
            Assert.AreEqual(3, output.Length);
            Assert.AreEqual("in foo", output[0]);
            Assert.AreEqual("in bar", output[1]);
            Assert.AreEqual("in baz", output[2]);
        }

        [TestMethod]
        public void LocalInheritOtherTest()
        {
            string[] output = Tools.RunFile("..//..//..//Tests//class//local_inherit_other.lox");
            Assert.AreEqual(1, output.Length);
            Assert.AreEqual("B", output[0]);
        }

        [TestMethod]
        public void LocalInheritSelfTest()
        {
            string[] output = Tools.RunFile("..//..//..//Tests//class//local_inherit_self.lox");
            Assert.AreEqual(1, output.Length);
            Assert.AreEqual("[line 2] Error at 'Foo': A class can't inherit from itself.", output[0]);
        }

        [TestMethod]
        public void LocalReferenceSelfTest()
        {
            string[] output = Tools.RunFile("..//..//..//Tests//class//local_reference_self.lox");
            Assert.AreEqual(1, output.Length);
            Assert.AreEqual("Foo", output[0]);
        }

        [TestMethod]
        public void ReferenceSelfTest()
        {
            string[] output = Tools.RunFile("..//..//..//Tests//class//reference_self.lox");
            Assert.AreEqual(1, output.Length);
            Assert.AreEqual("Foo", output[0]);
        }
    }
}
