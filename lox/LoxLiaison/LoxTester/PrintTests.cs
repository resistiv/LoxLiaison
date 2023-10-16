// Generated using LoxTestGenerator

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoxTester
{
	[TestClass]
	public class PrintTests
	{
		[TestMethod]
		public void MissingArgumentTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\Kai\\source\\repos\\cs503\\lox\\LoxLiaison\\\\LoxTester\\Tests\\print\\missing_argument.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 2] Error at ';': Expect expression.", output[0]);
		}

	}
}
