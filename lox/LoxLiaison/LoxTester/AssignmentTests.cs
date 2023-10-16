// Generated using LoxTestGenerator

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoxTester
{
	[TestClass]
	public class AssignmentTests
	{
		[TestMethod]
		public void AssociativityTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\assignment\\associativity.lox");
			Assert.AreEqual(3, output.Length);
			Assert.AreEqual("c", output[0]);
			Assert.AreEqual("c", output[1]);
			Assert.AreEqual("c", output[2]);
		}

		[TestMethod]
		public void GlobalTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\assignment\\global.lox");
			Assert.AreEqual(4, output.Length);
			Assert.AreEqual("before", output[0]);
			Assert.AreEqual("after", output[1]);
			Assert.AreEqual("arg", output[2]);
			Assert.AreEqual("arg", output[3]);
		}

		[TestMethod]
		public void GroupingTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\assignment\\grouping.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 2] Error at '=': Invalid assignment target.", output[0]);
		}

		[TestMethod]
		public void InfixOperatorTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\assignment\\infix_operator.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 3] Error at '=': Invalid assignment target.", output[0]);
		}

		[TestMethod]
		public void LocalTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\assignment\\local.lox");
			Assert.AreEqual(4, output.Length);
			Assert.AreEqual("before", output[0]);
			Assert.AreEqual("after", output[1]);
			Assert.AreEqual("arg", output[2]);
			Assert.AreEqual("arg", output[3]);
		}

		[TestMethod]
		public void PrefixOperatorTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\assignment\\prefix_operator.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 2] Error at '=': Invalid assignment target.", output[0]);
		}

		[TestMethod]
		public void SyntaxTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\assignment\\syntax.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("var", output[0]);
			Assert.AreEqual("var", output[1]);
		}

		[TestMethod]
		public void ToThisTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\assignment\\to_this.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 3] Error at '=': Invalid assignment target.", output[0]);
		}

		[TestMethod]
		public void UndefinedTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\assignment\\undefined.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("Undefined variable 'unknown'.", output[0]);
			Assert.AreEqual("[line 1]", output[1]);
		}

	}
}
