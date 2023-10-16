// Generated using LoxTestGenerator

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoxTester
{
	[TestClass]
	public class ReturnTests
	{
		[TestMethod]
		public void AfterElseTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\return\\after_else.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("ok", output[0]);
		}

		[TestMethod]
		public void AfterIfTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\return\\after_if.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("ok", output[0]);
		}

		[TestMethod]
		public void AfterWhileTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\return\\after_while.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("ok", output[0]);
		}

		[TestMethod]
		public void AtTopLevelTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\return\\at_top_level.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 1] Error at 'return': Can't return from top-level code.", output[0]);
		}

		[TestMethod]
		public void InFunctionTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\return\\in_function.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("ok", output[0]);
		}

		[TestMethod]
		public void InMethodTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\return\\in_method.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("ok", output[0]);
		}

		[TestMethod]
		public void ReturnNilIfNoValueTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\return\\return_nil_if_no_value.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("nil", output[0]);
		}

	}
}
