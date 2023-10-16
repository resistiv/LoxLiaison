// Generated using LoxTestGenerator

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoxTester
{
	[TestClass]
	public class ForTests
	{
		[TestMethod]
		public void ClassInBodyTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\Kai\\source\\repos\\cs503\\lox\\LoxLiaison\\\\LoxTester\\Tests\\for\\class_in_body.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 2] Error at 'class': Expect expression.", output[0]);
		}

		[TestMethod]
		public void ClosureInBodyTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\Kai\\source\\repos\\cs503\\lox\\LoxLiaison\\\\LoxTester\\Tests\\for\\closure_in_body.lox");
			Assert.AreEqual(6, output.Length);
			Assert.AreEqual("4", output[0]);
			Assert.AreEqual("1", output[1]);
			Assert.AreEqual("4", output[2]);
			Assert.AreEqual("2", output[3]);
			Assert.AreEqual("4", output[4]);
			Assert.AreEqual("3", output[5]);
		}

		[TestMethod]
		public void FunInBodyTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\Kai\\source\\repos\\cs503\\lox\\LoxLiaison\\\\LoxTester\\Tests\\for\\fun_in_body.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 2] Error at 'fun': Expect expression.", output[0]);
		}

		[TestMethod]
		public void ReturnClosureTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\Kai\\source\\repos\\cs503\\lox\\LoxLiaison\\\\LoxTester\\Tests\\for\\return_closure.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("i", output[0]);
		}

		[TestMethod]
		public void ReturnInsideTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\Kai\\source\\repos\\cs503\\lox\\LoxLiaison\\\\LoxTester\\Tests\\for\\return_inside.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("i", output[0]);
		}

		[TestMethod]
		public void ScopeTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\Kai\\source\\repos\\cs503\\lox\\LoxLiaison\\\\LoxTester\\Tests\\for\\scope.lox");
			Assert.AreEqual(4, output.Length);
			Assert.AreEqual("0", output[0]);
			Assert.AreEqual("-1", output[1]);
			Assert.AreEqual("after", output[2]);
			Assert.AreEqual("0", output[3]);
		}

		[TestMethod]
		public void StatementConditionTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\Kai\\source\\repos\\cs503\\lox\\LoxLiaison\\\\LoxTester\\Tests\\for\\statement_condition.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("[line 3] Error at '{': Expect expression.", output[0]);
			Assert.AreEqual("[line 3] Error at ')': Expect ';' after expression.", output[1]);
		}

		[TestMethod]
		public void StatementIncrementTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\Kai\\source\\repos\\cs503\\lox\\LoxLiaison\\\\LoxTester\\Tests\\for\\statement_increment.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 2] Error at '{': Expect expression.", output[0]);
		}

		[TestMethod]
		public void StatementInitializerTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\Kai\\source\\repos\\cs503\\lox\\LoxLiaison\\\\LoxTester\\Tests\\for\\statement_initializer.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("[line 3] Error at '{': Expect expression.", output[0]);
			Assert.AreEqual("[line 3] Error at ')': Expect ';' after expression.", output[1]);
		}

		[TestMethod]
		public void SyntaxTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\Kai\\source\\repos\\cs503\\lox\\LoxLiaison\\\\LoxTester\\Tests\\for\\syntax.lox");
			Assert.AreEqual(14, output.Length);
			Assert.AreEqual("1", output[0]);
			Assert.AreEqual("2", output[1]);
			Assert.AreEqual("3", output[2]);
			Assert.AreEqual("0", output[3]);
			Assert.AreEqual("1", output[4]);
			Assert.AreEqual("2", output[5]);
			Assert.AreEqual("done", output[6]);
			Assert.AreEqual("0", output[7]);
			Assert.AreEqual("1", output[8]);
			Assert.AreEqual("0", output[9]);
			Assert.AreEqual("1", output[10]);
			Assert.AreEqual("2", output[11]);
			Assert.AreEqual("0", output[12]);
			Assert.AreEqual("1", output[13]);
		}

		[TestMethod]
		public void VarInBodyTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\Kai\\source\\repos\\cs503\\lox\\LoxLiaison\\\\LoxTester\\Tests\\for\\var_in_body.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 2] Error at 'var': Expect expression.", output[0]);
		}

	}
}
