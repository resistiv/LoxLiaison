// Generated using LoxTestGenerator

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoxTester
{
	[TestClass]
	public class SuperTests
	{
		[TestMethod]
		public void BoundMethodTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\super\\bound_method.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("A.method(arg)", output[0]);
		}

		[TestMethod]
		public void CallOtherMethodTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\super\\call_other_method.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("Derived.bar()", output[0]);
			Assert.AreEqual("Base.foo()", output[1]);
		}

		[TestMethod]
		public void CallSameMethodTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\super\\call_same_method.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("Derived.foo()", output[0]);
			Assert.AreEqual("Base.foo()", output[1]);
		}

		[TestMethod]
		public void ClosureTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\super\\closure.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("Base", output[0]);
		}

		[TestMethod]
		public void ConstructorTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\super\\constructor.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("Derived.init()", output[0]);
			Assert.AreEqual("Base.init(a, b)", output[1]);
		}

		[TestMethod]
		public void ExtraArgumentsTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\super\\extra_arguments.lox");
			Assert.AreEqual(3, output.Length);
			Assert.AreEqual("Derived.foo()", output[0]);
			Assert.AreEqual("Expected 2 arguments but got 4.", output[1]);
			Assert.AreEqual("[line 10]", output[2]);
		}

		[TestMethod]
		public void IndirectlyInheritedTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\super\\indirectly_inherited.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("C.foo()", output[0]);
			Assert.AreEqual("A.foo()", output[1]);
		}

		[TestMethod]
		public void MissingArgumentsTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\super\\missing_arguments.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("Expected 2 arguments but got 1.", output[0]);
			Assert.AreEqual("[line 9]", output[1]);
		}

		[TestMethod]
		public void NoSuperclassBindTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\super\\no_superclass_bind.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 3] Error at 'super': Can't use 'super' in a class with no superclass.", output[0]);
		}

		[TestMethod]
		public void NoSuperclassCallTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\super\\no_superclass_call.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 3] Error at 'super': Can't use 'super' in a class with no superclass.", output[0]);
		}

		[TestMethod]
		public void NoSuperclassMethodTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\super\\no_superclass_method.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("Undefined property 'doesNotExist'.", output[0]);
			Assert.AreEqual("[line 5]", output[1]);
		}

		[TestMethod]
		public void ParenthesizedTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\super\\parenthesized.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual(" // [line 8] Error at ')': Expect '.' after 'super'.", output[0]);
		}

		[TestMethod]
		public void ReassignSuperclassTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\super\\reassign_superclass.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("Base.method()", output[0]);
			Assert.AreEqual("Base.method()", output[1]);
		}

		[TestMethod]
		public void SuperAtTopLevelTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\super\\super_at_top_level.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("[line 1] Error at 'super': Can't use 'super' outside of a class.", output[0]);
			Assert.AreEqual("[line 2] Error at 'super': Can't use 'super' outside of a class.", output[1]);
		}

		[TestMethod]
		public void SuperInClosureInInheritedMethodTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\super\\super_in_closure_in_inherited_method.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("A", output[0]);
		}

		[TestMethod]
		public void SuperInInheritedMethodTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\super\\super_in_inherited_method.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("A", output[0]);
		}

		[TestMethod]
		public void SuperInTopLevelFunctionTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\super\\super_in_top_level_function.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 1] Error at 'super': Can't use 'super' outside of a class.", output[0]);
		}

		[TestMethod]
		public void SuperWithoutDotTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\super\\super_without_dot.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual(" // [line 6] Error at ';': Expect '.' after 'super'.", output[0]);
		}

		[TestMethod]
		public void SuperWithoutNameTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\super\\super_without_name.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 5] Error at ';': Expect superclass method name.", output[0]);
		}

		[TestMethod]
		public void ThisInSuperclassMethodTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\super\\this_in_superclass_method.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("a", output[0]);
			Assert.AreEqual("b", output[1]);
		}

	}
}
