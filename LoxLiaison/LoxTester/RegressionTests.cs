// Generated using LoxTestGenerator

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoxTester
{
	[TestClass]
	public class RegressionTests
	{
		[TestMethod]
		public void M394Test()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/regression/394.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("B", output[0]);
		}

		[TestMethod]
		public void M40Test()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/regression/40.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("False", output[0]);
		}

	}
}