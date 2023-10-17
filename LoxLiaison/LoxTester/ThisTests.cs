// Generated using LoxTestGenerator

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoxTester
{
	[TestClass]
	public class ThisTests
	{
		[TestMethod]
		public void ClosureTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/this/closure.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("Foo", output[0]);
		}

		[TestMethod]
		public void NestedClassTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/this/nested_class.lox");
			Assert.AreEqual(3, output.Length);
			Assert.AreEqual("Outer instance", output[0]);
			Assert.AreEqual("Outer instance", output[1]);
			Assert.AreEqual("Inner instance", output[2]);
		}

		[TestMethod]
		public void NestedClosureTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/this/nested_closure.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("Foo", output[0]);
		}

		[TestMethod]
		public void ThisAtTopLevelTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/this/this_at_top_level.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 1] Error at 'this': Can't use 'this' outside of a class.", output[0]);
		}

		[TestMethod]
		public void ThisInMethodTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/this/this_in_method.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("baz", output[0]);
		}

		[TestMethod]
		public void ThisInTopLevelFunctionTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/this/this_in_top_level_function.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 2] Error at 'this': Can't use 'this' outside of a class.", output[0]);
		}

	}
}
