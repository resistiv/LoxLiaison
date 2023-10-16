// Generated using LoxTestGenerator

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoxTester
{
	[TestClass]
	public class StringTests
	{
		[TestMethod]
		public void ErrorAfterMultilineTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\Kai\\source\\repos\\cs503\\lox\\LoxLiaison\\\\LoxTester\\Tests\\string\\error_after_multiline.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("Undefined variable 'err'.", output[0]);
			Assert.AreEqual("[line 7]", output[1]);
		}

		[TestMethod]
		public void LiteralsTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\Kai\\source\\repos\\cs503\\lox\\LoxLiaison\\\\LoxTester\\Tests\\string\\literals.lox");
			Assert.AreEqual(3, output.Length);
			Assert.AreEqual("()", output[0]);
			Assert.AreEqual("a string", output[1]);
			Assert.AreEqual("A~¶Þॐஃ", output[2]);
		}

		[TestMethod]
		public void MultilineTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\Kai\\source\\repos\\cs503\\lox\\LoxLiaison\\\\LoxTester\\Tests\\string\\multiline.lox");
			Assert.AreEqual(3, output.Length);
			Assert.AreEqual("1", output[0]);
			Assert.AreEqual("2", output[1]);
			Assert.AreEqual("3", output[2]);
		}

		[TestMethod]
		public void UnterminatedTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\Kai\\source\\repos\\cs503\\lox\\LoxLiaison\\\\LoxTester\\Tests\\string\\unterminated.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 2] Error: Unterminated string.", output[0]);
		}

	}
}
