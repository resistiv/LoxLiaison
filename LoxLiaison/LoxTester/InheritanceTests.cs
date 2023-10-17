// Generated using LoxTestGenerator

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoxTester
{
	[TestClass]
	public class InheritanceTests
	{
		[TestMethod]
		public void ConstructorTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/inheritance/constructor.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("value", output[0]);
		}

		[TestMethod]
		public void InheritFromFunctionTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/inheritance/inherit_from_function.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("Superclass must be a class.", output[0]);
			Assert.AreEqual("[line 3]", output[1]);
		}

		[TestMethod]
		public void InheritFromNilTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/inheritance/inherit_from_nil.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("Superclass must be a class.", output[0]);
			Assert.AreEqual("[line 2]", output[1]);
		}

		[TestMethod]
		public void InheritFromNumberTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/inheritance/inherit_from_number.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("Superclass must be a class.", output[0]);
			Assert.AreEqual("[line 2]", output[1]);
		}

		[TestMethod]
		public void InheritMethodsTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/inheritance/inherit_methods.lox");
			Assert.AreEqual(3, output.Length);
			Assert.AreEqual("foo", output[0]);
			Assert.AreEqual("bar", output[1]);
			Assert.AreEqual("bar", output[2]);
		}

		[TestMethod]
		public void ParenthesizedSuperclassTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/inheritance/parenthesized_superclass.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 4] Error at '(': Expect superclass name.", output[0]);
		}

		[TestMethod]
		public void SetFieldsFromBaseClassTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/inheritance/set_fields_from_base_class.lox");
			Assert.AreEqual(6, output.Length);
			Assert.AreEqual("foo 1", output[0]);
			Assert.AreEqual("foo 2", output[1]);
			Assert.AreEqual("bar 1", output[2]);
			Assert.AreEqual("bar 2", output[3]);
			Assert.AreEqual("bar 1", output[4]);
			Assert.AreEqual("bar 2", output[5]);
		}

	}
}
