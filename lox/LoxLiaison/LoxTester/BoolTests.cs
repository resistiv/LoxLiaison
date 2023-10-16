// Generated using LoxTestGenerator

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoxTester
{
	[TestClass]
	public class BoolTests
	{
		[TestMethod]
		public void EqualityTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\Kai\\source\\repos\\cs503\\lox\\LoxLiaison\\\\LoxTester\\Tests\\bool\\equality.lox");
			Assert.AreEqual(18, output.Length);
			Assert.AreEqual("True", output[0]);
			Assert.AreEqual("False", output[1]);
			Assert.AreEqual("False", output[2]);
			Assert.AreEqual("True", output[3]);
			Assert.AreEqual("False", output[4]);
			Assert.AreEqual("False", output[5]);
			Assert.AreEqual("False", output[6]);
			Assert.AreEqual("False", output[7]);
			Assert.AreEqual("False", output[8]);
			Assert.AreEqual("False", output[9]);
			Assert.AreEqual("True", output[10]);
			Assert.AreEqual("True", output[11]);
			Assert.AreEqual("False", output[12]);
			Assert.AreEqual("True", output[13]);
			Assert.AreEqual("True", output[14]);
			Assert.AreEqual("True", output[15]);
			Assert.AreEqual("True", output[16]);
			Assert.AreEqual("True", output[17]);
		}

		[TestMethod]
		public void NotTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\Kai\\source\\repos\\cs503\\lox\\LoxLiaison\\\\LoxTester\\Tests\\bool\\not.lox");
			Assert.AreEqual(3, output.Length);
			Assert.AreEqual("False", output[0]);
			Assert.AreEqual("True", output[1]);
			Assert.AreEqual("True", output[2]);
		}

	}
}
