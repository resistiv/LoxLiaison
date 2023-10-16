// Generated using LoxTestGenerator

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoxTester
{
	[TestClass]
	public class MethodTests
	{
		[TestMethod]
		public void ArityTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\method\\arity.lox");
			Assert.AreEqual(9, output.Length);
			Assert.AreEqual("no args", output[0]);
			Assert.AreEqual("1", output[1]);
			Assert.AreEqual("3", output[2]);
			Assert.AreEqual("6", output[3]);
			Assert.AreEqual("10", output[4]);
			Assert.AreEqual("15", output[5]);
			Assert.AreEqual("21", output[6]);
			Assert.AreEqual("28", output[7]);
			Assert.AreEqual("36", output[8]);
		}

		[TestMethod]
		public void EmptyBlockTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\method\\empty_block.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("nil", output[0]);
		}

		[TestMethod]
		public void ExtraArgumentsTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\method\\extra_arguments.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("Expected 2 arguments but got 4.", output[0]);
			Assert.AreEqual("[line 8]", output[1]);
		}

		[TestMethod]
		public void MissingArgumentsTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\method\\missing_arguments.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("Expected 2 arguments but got 1.", output[0]);
			Assert.AreEqual("[line 5]", output[1]);
		}

		[TestMethod]
		public void NotFoundTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\method\\not_found.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("Undefined property 'unknown'.", output[0]);
			Assert.AreEqual("[line 3]", output[1]);
		}

		[TestMethod]
		public void PrintBoundMethodTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\method\\print_bound_method.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("<fn method>", output[0]);
		}

		[TestMethod]
		public void ReferToNameTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\method\\refer_to_name.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("Undefined variable 'method'.", output[0]);
			Assert.AreEqual("[line 3]", output[1]);
		}

		[TestMethod]
		public void TooManyArgumentsTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\method\\too_many_arguments.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 259] Error at 'a': Can't have more than 255 arguments.", output[0]);
		}

		[TestMethod]
		public void TooManyParametersTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\method\\too_many_parameters.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 258] Error at 'a': Can't have more than 255 parameters.", output[0]);
		}

	}
}
