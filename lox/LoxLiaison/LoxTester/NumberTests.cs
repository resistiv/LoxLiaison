// Generated using LoxTestGenerator

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoxTester
{
	[TestClass]
	public class NumberTests
	{
		[TestMethod]
		public void DecimalPointAtEofTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\Kai\\source\\repos\\cs503\\lox\\LoxLiaison\\\\LoxTester\\Tests\\number\\decimal_point_at_eof.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 2] Error at end: Expect property name after '.'.", output[0]);
		}

		[TestMethod]
		public void LeadingDotTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\Kai\\source\\repos\\cs503\\lox\\LoxLiaison\\\\LoxTester\\Tests\\number\\leading_dot.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 2] Error at '.': Expect expression.", output[0]);
		}

		[TestMethod]
		public void LiteralsTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\Kai\\source\\repos\\cs503\\lox\\LoxLiaison\\\\LoxTester\\Tests\\number\\literals.lox");
			Assert.AreEqual(6, output.Length);
			Assert.AreEqual("123", output[0]);
			Assert.AreEqual("987654", output[1]);
			Assert.AreEqual("0", output[2]);
			Assert.AreEqual("-0", output[3]);
			Assert.AreEqual("123.456", output[4]);
			Assert.AreEqual("-0.001", output[5]);
		}

		[TestMethod]
		public void TrailingDotTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\Kai\\source\\repos\\cs503\\lox\\LoxLiaison\\\\LoxTester\\Tests\\number\\trailing_dot.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 2] Error at ';': Expect property name after '.'.", output[0]);
		}

	}
}
