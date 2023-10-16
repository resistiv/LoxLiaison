// Generated using LoxTestGenerator

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoxTester
{
	[TestClass]
	public class NilTests
	{
		[TestMethod]
		public void LiteralTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\nil\\literal.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("nil", output[0]);
		}

	}
}
