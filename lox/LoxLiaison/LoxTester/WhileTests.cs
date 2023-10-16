// Generated using LoxTestGenerator

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoxTester
{
	[TestClass]
	public class WhileTests
	{
		[TestMethod]
		public void ClassInBodyTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\Kai\\source\\repos\\cs503\\lox\\LoxLiaison\\\\LoxTester\\Tests\\while\\class_in_body.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 2] Error at 'class': Expect expression.", output[0]);
		}

		[TestMethod]
		public void ClosureInBodyTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\Kai\\source\\repos\\cs503\\lox\\LoxLiaison\\\\LoxTester\\Tests\\while\\closure_in_body.lox");
			Assert.AreEqual(3, output.Length);
			Assert.AreEqual("1", output[0]);
			Assert.AreEqual("2", output[1]);
			Assert.AreEqual("3", output[2]);
		}

		[TestMethod]
		public void FunInBodyTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\Kai\\source\\repos\\cs503\\lox\\LoxLiaison\\\\LoxTester\\Tests\\while\\fun_in_body.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 2] Error at 'fun': Expect expression.", output[0]);
		}

		[TestMethod]
		public void ReturnClosureTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\Kai\\source\\repos\\cs503\\lox\\LoxLiaison\\\\LoxTester\\Tests\\while\\return_closure.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("i", output[0]);
		}

		[TestMethod]
		public void ReturnInsideTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\Kai\\source\\repos\\cs503\\lox\\LoxLiaison\\\\LoxTester\\Tests\\while\\return_inside.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("i", output[0]);
		}

		[TestMethod]
		public void SyntaxTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\Kai\\source\\repos\\cs503\\lox\\LoxLiaison\\\\LoxTester\\Tests\\while\\syntax.lox");
			Assert.AreEqual(6, output.Length);
			Assert.AreEqual("1", output[0]);
			Assert.AreEqual("2", output[1]);
			Assert.AreEqual("3", output[2]);
			Assert.AreEqual("0", output[3]);
			Assert.AreEqual("1", output[4]);
			Assert.AreEqual("2", output[5]);
		}

		[TestMethod]
		public void VarInBodyTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\Kai\\source\\repos\\cs503\\lox\\LoxLiaison\\\\LoxTester\\Tests\\while\\var_in_body.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 2] Error at 'var': Expect expression.", output[0]);
		}

	}
}
