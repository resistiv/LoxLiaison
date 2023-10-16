// Generated using LoxTestGenerator

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoxTester
{
	[TestClass]
	public class LimitTests
	{
		[TestMethod]
		public void LoopTooLargeTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\limit\\loop_too_large.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 2351] Error at '}': Loop body too large.", output[0]);
		}

		[TestMethod]
		public void NoReuseConstantsTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\limit\\no_reuse_constants.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 35] Error at '1': Too many constants in one chunk.", output[0]);
		}

		[TestMethod]
		public void StackOverflowTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\limit\\stack_overflow.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("Stack overflow.", output[0]);
			Assert.AreEqual("[line 18]", output[1]);
		}

		[TestMethod]
		public void TooManyConstantsTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\limit\\too_many_constants.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 35] Error at '\"oops\"': Too many constants in one chunk.", output[0]);
		}

		[TestMethod]
		public void TooManyLocalsTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\limit\\too_many_locals.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 52] Error at 'oops': Too many local variables in function.", output[0]);
		}

		[TestMethod]
		public void TooManyUpvaluesTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\limit\\too_many_upvalues.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 102] Error at 'oops': Too many closure variables in function.", output[0]);
		}

	}
}
