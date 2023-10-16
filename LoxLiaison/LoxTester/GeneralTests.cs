// Generated using LoxTestGenerator

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoxTester
{
	[TestClass]
	public class GeneralTests
	{
		[TestMethod]
		public void EmptyFileTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\Kai\\source\\repos\\cs503\\lox\\LoxLiaison\\\\LoxTester\\Tests\\empty_file.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("", output[0]);
		}

		[TestMethod]
		public void PrecedenceTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\Kai\\source\\repos\\cs503\\lox\\LoxLiaison\\\\LoxTester\\Tests\\precedence.lox");
			Assert.AreEqual(13, output.Length);
			Assert.AreEqual("14", output[0]);
			Assert.AreEqual("8", output[1]);
			Assert.AreEqual("4", output[2]);
			Assert.AreEqual("0", output[3]);
			Assert.AreEqual("True", output[4]);
			Assert.AreEqual("True", output[5]);
			Assert.AreEqual("True", output[6]);
			Assert.AreEqual("True", output[7]);
			Assert.AreEqual("0", output[8]);
			Assert.AreEqual("0", output[9]);
			Assert.AreEqual("0", output[10]);
			Assert.AreEqual("0", output[11]);
			Assert.AreEqual("4", output[12]);
		}

		[TestMethod]
		public void UnexpectedCharacterTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\Kai\\source\\repos\\cs503\\lox\\LoxLiaison\\\\LoxTester\\Tests\\unexpected_character.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("[line 3] Error: Unexpected character.", output[0]);
			Assert.AreEqual("[line 3] Error at 'b': Expect ')' after arguments.", output[1]);
		}

	}
}
