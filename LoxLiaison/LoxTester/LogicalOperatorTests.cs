// Generated using LoxTestGenerator

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoxTester
{
	[TestClass]
	public class LogicalOperatorTests
	{
		[TestMethod]
		public void AndTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\Kai\\source\\repos\\cs503\\lox\\LoxLiaison\\\\LoxTester\\Tests\\logical_operator\\and.lox");
			Assert.AreEqual(7, output.Length);
			Assert.AreEqual("False", output[0]);
			Assert.AreEqual("1", output[1]);
			Assert.AreEqual("False", output[2]);
			Assert.AreEqual("True", output[3]);
			Assert.AreEqual("3", output[4]);
			Assert.AreEqual("True", output[5]);
			Assert.AreEqual("False", output[6]);
		}

		[TestMethod]
		public void AndTruthTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\Kai\\source\\repos\\cs503\\lox\\LoxLiaison\\\\LoxTester\\Tests\\logical_operator\\and_truth.lox");
			Assert.AreEqual(5, output.Length);
			Assert.AreEqual("False", output[0]);
			Assert.AreEqual("nil", output[1]);
			Assert.AreEqual("ok", output[2]);
			Assert.AreEqual("ok", output[3]);
			Assert.AreEqual("ok", output[4]);
		}

		[TestMethod]
		public void OrTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\Kai\\source\\repos\\cs503\\lox\\LoxLiaison\\\\LoxTester\\Tests\\logical_operator\\or.lox");
			Assert.AreEqual(7, output.Length);
			Assert.AreEqual("1", output[0]);
			Assert.AreEqual("1", output[1]);
			Assert.AreEqual("True", output[2]);
			Assert.AreEqual("False", output[3]);
			Assert.AreEqual("False", output[4]);
			Assert.AreEqual("False", output[5]);
			Assert.AreEqual("True", output[6]);
		}

		[TestMethod]
		public void OrTruthTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\Kai\\source\\repos\\cs503\\lox\\LoxLiaison\\\\LoxTester\\Tests\\logical_operator\\or_truth.lox");
			Assert.AreEqual(5, output.Length);
			Assert.AreEqual("ok", output[0]);
			Assert.AreEqual("ok", output[1]);
			Assert.AreEqual("True", output[2]);
			Assert.AreEqual("0", output[3]);
			Assert.AreEqual("s", output[4]);
		}

	}
}
