// Generated using LoxTestGenerator

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoxTester
{
	[TestClass]
	public class VariableTests
	{
		[TestMethod]
		public void CollideWithParameterTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/variable/collide_with_parameter.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 2] Error at 'a': Already a variable with this name in this scope.", output[0]);
		}

		[TestMethod]
		public void DuplicateLocalTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/variable/duplicate_local.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 3] Error at 'a': Already a variable with this name in this scope.", output[0]);
		}

		[TestMethod]
		public void DuplicateParameterTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/variable/duplicate_parameter.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 2] Error at 'arg': Already a variable with this name in this scope.", output[0]);
		}

		[TestMethod]
		public void EarlyBoundTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/variable/early_bound.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("outer", output[0]);
			Assert.AreEqual("outer", output[1]);
		}

		[TestMethod]
		public void InMiddleOfBlockTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/variable/in_middle_of_block.lox");
			Assert.AreEqual(4, output.Length);
			Assert.AreEqual("a", output[0]);
			Assert.AreEqual("a b", output[1]);
			Assert.AreEqual("a c", output[2]);
			Assert.AreEqual("a b d", output[3]);
		}

		[TestMethod]
		public void InNestedBlockTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/variable/in_nested_block.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("outer", output[0]);
		}

		[TestMethod]
		public void LocalFromMethodTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/variable/local_from_method.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("variable", output[0]);
		}

		[TestMethod]
		public void RedeclareGlobalTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/variable/redeclare_global.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("nil", output[0]);
		}

		[TestMethod]
		public void RedefineGlobalTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/variable/redefine_global.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("2", output[0]);
		}

		[TestMethod]
		public void ScopeReuseInDifferentBlocksTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/variable/scope_reuse_in_different_blocks.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("first", output[0]);
			Assert.AreEqual("second", output[1]);
		}

		[TestMethod]
		public void ShadowAndLocalTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/variable/shadow_and_local.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("outer", output[0]);
			Assert.AreEqual("inner", output[1]);
		}

		[TestMethod]
		public void ShadowGlobalTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/variable/shadow_global.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("shadow", output[0]);
			Assert.AreEqual("global", output[1]);
		}

		[TestMethod]
		public void ShadowLocalTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/variable/shadow_local.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("shadow", output[0]);
			Assert.AreEqual("local", output[1]);
		}

		[TestMethod]
		public void UndefinedGlobalTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/variable/undefined_global.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("Undefined variable 'notDefined'.", output[0]);
			Assert.AreEqual("[line 1]", output[1]);
		}

		[TestMethod]
		public void UndefinedLocalTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/variable/undefined_local.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("Undefined variable 'notDefined'.", output[0]);
			Assert.AreEqual("[line 2]", output[1]);
		}

		[TestMethod]
		public void UninitializedTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/variable/uninitialized.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("nil", output[0]);
		}

		[TestMethod]
		public void UnreachedUndefinedTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/variable/unreached_undefined.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("ok", output[0]);
		}

		[TestMethod]
		public void UseFalseAsVarTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/variable/use_false_as_var.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 2] Error at 'false': Expect variable name.", output[0]);
		}

		[TestMethod]
		public void UseGlobalInInitializerTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/variable/use_global_in_initializer.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("value", output[0]);
		}

		[TestMethod]
		public void UseLocalInInitializerTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/variable/use_local_in_initializer.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 3] Error at 'a': Can't read local variable in its own initializer.", output[0]);
		}

		[TestMethod]
		public void UseNilAsVarTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/variable/use_nil_as_var.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 2] Error at 'nil': Expect variable name.", output[0]);
		}

		[TestMethod]
		public void UseThisAsVarTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/variable/use_this_as_var.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("[line 2] Error at 'this': Expect variable name.", output[0]);
		}

	}
}
