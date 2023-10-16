// Generated using LoxTestGenerator

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoxTester
{
	[TestClass]
	public class IfTests
	{
		[TestMethod]
		public void ClassInElseTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\if\\class_in_else.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 2] Error at 'class': Expect expression.", output[0]);
		}

		[TestMethod]
		public void ClassInThenTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\if\\class_in_then.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 2] Error at 'class': Expect expression.", output[0]);
		}

		[TestMethod]
		public void DanglingElseTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\if\\dangling_else.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("good", output[0]);
		}

		[TestMethod]
		public void ElseTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\if\\else.lox");
			Assert.AreEqual(3, output.Length);
			Assert.AreEqual("good", output[0]);
			Assert.AreEqual("good", output[1]);
			Assert.AreEqual("block", output[2]);
		}

		[TestMethod]
		public void FunInElseTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\if\\fun_in_else.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 2] Error at 'fun': Expect expression.", output[0]);
		}

		[TestMethod]
		public void FunInThenTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\if\\fun_in_then.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 2] Error at 'fun': Expect expression.", output[0]);
		}

		[TestMethod]
		public void IfTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\if\\if.lox");
			Assert.AreEqual(3, output.Length);
			Assert.AreEqual("good", output[0]);
			Assert.AreEqual("block", output[1]);
			Assert.AreEqual("True", output[2]);
		}

		[TestMethod]
		public void TruthTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\if\\truth.lox");
			Assert.AreEqual(5, output.Length);
			Assert.AreEqual("False", output[0]);
			Assert.AreEqual("nil", output[1]);
			Assert.AreEqual("True", output[2]);
			Assert.AreEqual("0", output[3]);
			Assert.AreEqual("empty", output[4]);
		}

		[TestMethod]
		public void VarInElseTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\if\\var_in_else.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 2] Error at 'var': Expect expression.", output[0]);
		}

		[TestMethod]
		public void VarInThenTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\if\\var_in_then.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 2] Error at 'var': Expect expression.", output[0]);
		}

	}
}
