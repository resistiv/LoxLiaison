using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoxTester
{
    [TestClass]
    public class ClosureTests
    {
        [TestMethod]
        public void AssignToClosureTest()
        {
            string[] output = Tools.RunFile("..//..//..//Tests//closure//assign_to_closure.lox");
            Assert.AreEqual(4, output.Length);
            Assert.AreEqual("local", output[0]);
            Assert.AreEqual("after f", output[1]);
            Assert.AreEqual("after f", output[2]);
            Assert.AreEqual("after g", output[3]);
        }

        [TestMethod]
        public void AssignToShadowedLaterTest()
        {
            string[] output = Tools.RunFile("..//..//..//Tests//closure//assign_to_shadowed_later.lox");
            Assert.AreEqual(2, output.Length);
            Assert.AreEqual("inner", output[0]);
            Assert.AreEqual("assigned", output[1]);
        }

        [TestMethod]
        public void CloseOverFunctionParameterTest()
        {
            string[] output = Tools.RunFile("..//..//..//Tests//closure//close_over_function_parameter.lox");
            Assert.AreEqual(1, output.Length);
            Assert.AreEqual("param", output[0]);
        }

        [TestMethod]
        public void CloseOverLaterVariableTest()
        {
            string[] output = Tools.RunFile("..//..//..//Tests//closure//close_over_later_variable.lox");
            Assert.AreEqual(2, output.Length);
            Assert.AreEqual("b", output[0]);
            Assert.AreEqual("a", output[1]);
        }

        [TestMethod]
        public void CloseOverMethodParameterTest()
        {
            string[] output = Tools.RunFile("..//..//..//Tests//closure//close_over_method_parameter.lox");
            Assert.AreEqual(1, output.Length);
            Assert.AreEqual("param", output[0]);
        }

        [TestMethod]
        public void ClosedClosureInFunctionTest()
        {
            string[] output = Tools.RunFile("..//..//..//Tests//closure//closed_closure_in_function.lox");
            Assert.AreEqual(1, output.Length);
            Assert.AreEqual("local", output[0]);
        }

        [TestMethod]
        public void NestedClosureTest()
        {
            string[] output = Tools.RunFile("..//..//..//Tests//closure//nested_closure.lox");
            Assert.AreEqual(3, output.Length);
            Assert.AreEqual("a", output[0]);
            Assert.AreEqual("b", output[1]);
            Assert.AreEqual("c", output[2]);
        }

        [TestMethod]
        public void OpenClosureInFunctionTest()
        {
            string[] output = Tools.RunFile("..//..//..//Tests//closure//open_closure_in_function.lox");
            Assert.AreEqual(1, output.Length);
            Assert.AreEqual("local", output[0]);
        }

        [TestMethod]
        public void ReferenceClosureMultipleTimesTest()
        {
            string[] output = Tools.RunFile("..//..//..//Tests//closure//reference_closure_multiple_times.lox");
            Assert.AreEqual(2, output.Length);
            Assert.AreEqual("a", output[0]);
            Assert.AreEqual("a", output[1]);
        }

        [TestMethod]
        public void ReuseClosureSlotTest()
        {
            string[] output = Tools.RunFile("..//..//..//Tests//closure//reuse_closure_slot.lox");
            Assert.AreEqual(1, output.Length);
            Assert.AreEqual("a", output[0]);
        }

        [TestMethod]
        public void ShadowClosureWithLocalTest()
        {
            string[] output = Tools.RunFile("..//..//..//Tests//closure//shadow_closure_with_local.lox");
            Assert.AreEqual(3, output.Length);
            Assert.AreEqual("closure", output[0]);
            Assert.AreEqual("shadow", output[0]);
            Assert.AreEqual("closure", output[0]);
        }

        [TestMethod]
        public void UnusedClosureTest()
        {
            string[] output = Tools.RunFile("..//..//..//Tests//closure//unused_closure.lox");
            Assert.AreEqual(1, output.Length);
            Assert.AreEqual("ok", output[0]);
        }

        [TestMethod]
        public void UnusedLaterClosureTest()
        {
            string[] output = Tools.RunFile("..//..//..//Tests//closure//unused_later_closure.lox");
            Assert.AreEqual(1, output.Length);
            Assert.AreEqual("a", output[0]);
        }
    }
}
