// Generated using LoxTestGenerator

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoxTester
{
	[TestClass]
	public class ConstructorTests
	{
		[TestMethod]
		public void ArgumentsTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\constructor\\arguments.lox");
			Assert.AreEqual(3, output.Length);
			Assert.AreEqual("init", output[0]);
			Assert.AreEqual("1", output[1]);
			Assert.AreEqual("2", output[2]);
		}

		[TestMethod]
		public void CallInitEarlyReturnTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\constructor\\call_init_early_return.lox");
			Assert.AreEqual(3, output.Length);
			Assert.AreEqual("init", output[0]);
			Assert.AreEqual("init", output[1]);
			Assert.AreEqual("Foo instance", output[2]);
		}

		[TestMethod]
		public void CallInitExplicitlyTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\constructor\\call_init_explicitly.lox");
			Assert.AreEqual(4, output.Length);
			Assert.AreEqual("Foo.init(one)", output[0]);
			Assert.AreEqual("Foo.init(two)", output[1]);
			Assert.AreEqual("Foo instance", output[2]);
			Assert.AreEqual("init", output[3]);
		}

		[TestMethod]
		public void DefaultTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\constructor\\default.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("Foo instance", output[0]);
		}

		[TestMethod]
		public void DefaultArgumentsTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\constructor\\default_arguments.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("Expected 0 arguments but got 3.", output[0]);
			Assert.AreEqual("[line 3]", output[1]);
		}

		[TestMethod]
		public void EarlyReturnTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\constructor\\early_return.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("init", output[0]);
			Assert.AreEqual("Foo instance", output[1]);
		}

		[TestMethod]
		public void ExtraArgumentsTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\constructor\\extra_arguments.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("Expected 2 arguments but got 4.", output[0]);
			Assert.AreEqual("[line 8]", output[1]);
		}

		[TestMethod]
		public void InitNotMethodTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\constructor\\init_not_method.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("not initializer", output[0]);
		}

		[TestMethod]
		public void MissingArgumentsTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\constructor\\missing_arguments.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("Expected 2 arguments but got 1.", output[0]);
			Assert.AreEqual("[line 5]", output[1]);
		}

		[TestMethod]
		public void ReturnInNestedFunctionTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\constructor\\return_in_nested_function.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("bar", output[0]);
			Assert.AreEqual("Foo instance", output[1]);
		}

		[TestMethod]
		public void ReturnValueTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\constructor\\return_value.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 3] Error at 'return': Can't return a value from an initializer.", output[0]);
		}

	}
}
