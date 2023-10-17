// Generated using LoxTestGenerator

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoxTester
{
	[TestClass]
	public class FieldTests
	{
		[TestMethod]
		public void CallFunctionFieldTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/field/call_function_field.lox");
			Assert.AreEqual(3, output.Length);
			Assert.AreEqual("bar", output[0]);
			Assert.AreEqual("1", output[1]);
			Assert.AreEqual("2", output[2]);
		}

		[TestMethod]
		public void CallNonfunctionFieldTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/field/call_nonfunction_field.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("Can only call functions and classes.", output[0]);
			Assert.AreEqual("[line 6]", output[1]);
		}

		[TestMethod]
		public void GetAndSetMethodTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/field/get_and_set_method.lox");
			Assert.AreEqual(4, output.Length);
			Assert.AreEqual("other", output[0]);
			Assert.AreEqual("1", output[1]);
			Assert.AreEqual("method", output[2]);
			Assert.AreEqual("2", output[3]);
		}

		[TestMethod]
		public void GetOnBoolTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/field/get_on_bool.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("Only instances have properties.", output[0]);
			Assert.AreEqual("[line 1]", output[1]);
		}

		[TestMethod]
		public void GetOnClassTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/field/get_on_class.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("Only instances have properties.", output[0]);
			Assert.AreEqual("[line 2]", output[1]);
		}

		[TestMethod]
		public void GetOnFunctionTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/field/get_on_function.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("Only instances have properties.", output[0]);
			Assert.AreEqual("[line 3]", output[1]);
		}

		[TestMethod]
		public void GetOnNilTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/field/get_on_nil.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("Only instances have properties.", output[0]);
			Assert.AreEqual("[line 1]", output[1]);
		}

		[TestMethod]
		public void GetOnNumTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/field/get_on_num.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("Only instances have properties.", output[0]);
			Assert.AreEqual("[line 1]", output[1]);
		}

		[TestMethod]
		public void GetOnStringTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/field/get_on_string.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("Only instances have properties.", output[0]);
			Assert.AreEqual("[line 1]", output[1]);
		}

		[TestMethod]
		public void ManyTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/field/many.lox");
			Assert.AreEqual(79, output.Length);
			Assert.AreEqual("apple", output[0]);
			Assert.AreEqual("apricot", output[1]);
			Assert.AreEqual("avocado", output[2]);
			Assert.AreEqual("banana", output[3]);
			Assert.AreEqual("bilberry", output[4]);
			Assert.AreEqual("blackberry", output[5]);
			Assert.AreEqual("blackcurrant", output[6]);
			Assert.AreEqual("blueberry", output[7]);
			Assert.AreEqual("boysenberry", output[8]);
			Assert.AreEqual("cantaloupe", output[9]);
			Assert.AreEqual("cherimoya", output[10]);
			Assert.AreEqual("cherry", output[11]);
			Assert.AreEqual("clementine", output[12]);
			Assert.AreEqual("cloudberry", output[13]);
			Assert.AreEqual("coconut", output[14]);
			Assert.AreEqual("cranberry", output[15]);
			Assert.AreEqual("currant", output[16]);
			Assert.AreEqual("damson", output[17]);
			Assert.AreEqual("date", output[18]);
			Assert.AreEqual("dragonfruit", output[19]);
			Assert.AreEqual("durian", output[20]);
			Assert.AreEqual("elderberry", output[21]);
			Assert.AreEqual("feijoa", output[22]);
			Assert.AreEqual("fig", output[23]);
			Assert.AreEqual("gooseberry", output[24]);
			Assert.AreEqual("grape", output[25]);
			Assert.AreEqual("grapefruit", output[26]);
			Assert.AreEqual("guava", output[27]);
			Assert.AreEqual("honeydew", output[28]);
			Assert.AreEqual("huckleberry", output[29]);
			Assert.AreEqual("jabuticaba", output[30]);
			Assert.AreEqual("jackfruit", output[31]);
			Assert.AreEqual("jambul", output[32]);
			Assert.AreEqual("jujube", output[33]);
			Assert.AreEqual("juniper", output[34]);
			Assert.AreEqual("kiwifruit", output[35]);
			Assert.AreEqual("kumquat", output[36]);
			Assert.AreEqual("lemon", output[37]);
			Assert.AreEqual("lime", output[38]);
			Assert.AreEqual("longan", output[39]);
			Assert.AreEqual("loquat", output[40]);
			Assert.AreEqual("lychee", output[41]);
			Assert.AreEqual("mandarine", output[42]);
			Assert.AreEqual("mango", output[43]);
			Assert.AreEqual("marionberry", output[44]);
			Assert.AreEqual("melon", output[45]);
			Assert.AreEqual("miracle", output[46]);
			Assert.AreEqual("mulberry", output[47]);
			Assert.AreEqual("nance", output[48]);
			Assert.AreEqual("nectarine", output[49]);
			Assert.AreEqual("olive", output[50]);
			Assert.AreEqual("orange", output[51]);
			Assert.AreEqual("papaya", output[52]);
			Assert.AreEqual("passionfruit", output[53]);
			Assert.AreEqual("peach", output[54]);
			Assert.AreEqual("pear", output[55]);
			Assert.AreEqual("persimmon", output[56]);
			Assert.AreEqual("physalis", output[57]);
			Assert.AreEqual("pineapple", output[58]);
			Assert.AreEqual("plantain", output[59]);
			Assert.AreEqual("plum", output[60]);
			Assert.AreEqual("plumcot", output[61]);
			Assert.AreEqual("pomegranate", output[62]);
			Assert.AreEqual("pomelo", output[63]);
			Assert.AreEqual("quince", output[64]);
			Assert.AreEqual("raisin", output[65]);
			Assert.AreEqual("rambutan", output[66]);
			Assert.AreEqual("raspberry", output[67]);
			Assert.AreEqual("redcurrant", output[68]);
			Assert.AreEqual("salak", output[69]);
			Assert.AreEqual("salmonberry", output[70]);
			Assert.AreEqual("satsuma", output[71]);
			Assert.AreEqual("strawberry", output[72]);
			Assert.AreEqual("tamarillo", output[73]);
			Assert.AreEqual("tamarind", output[74]);
			Assert.AreEqual("tangerine", output[75]);
			Assert.AreEqual("tomato", output[76]);
			Assert.AreEqual("watermelon", output[77]);
			Assert.AreEqual("yuzu", output[78]);
		}

		[TestMethod]
		public void MethodTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/field/method.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("got method", output[0]);
			Assert.AreEqual("arg", output[1]);
		}

		[TestMethod]
		public void MethodBindsThisTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/field/method_binds_this.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("foo1", output[0]);
			Assert.AreEqual("1", output[1]);
		}

		[TestMethod]
		public void OnInstanceTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/field/on_instance.lox");
			Assert.AreEqual(4, output.Length);
			Assert.AreEqual("bar value", output[0]);
			Assert.AreEqual("baz value", output[1]);
			Assert.AreEqual("bar value", output[2]);
			Assert.AreEqual("baz value", output[3]);
		}

		[TestMethod]
		public void SetEvaluationOrderTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/field/set_evaluation_order.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("Undefined variable 'undefined1'.", output[0]);
			Assert.AreEqual("[line 1]", output[1]);
		}

		[TestMethod]
		public void SetOnBoolTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/field/set_on_bool.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("Only instances have fields.", output[0]);
			Assert.AreEqual("[line 1]", output[1]);
		}

		[TestMethod]
		public void SetOnClassTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/field/set_on_class.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("Only instances have fields.", output[0]);
			Assert.AreEqual("[line 2]", output[1]);
		}

		[TestMethod]
		public void SetOnFunctionTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/field/set_on_function.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("Only instances have fields.", output[0]);
			Assert.AreEqual("[line 3]", output[1]);
		}

		[TestMethod]
		public void SetOnNilTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/field/set_on_nil.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("Only instances have fields.", output[0]);
			Assert.AreEqual("[line 1]", output[1]);
		}

		[TestMethod]
		public void SetOnNumTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/field/set_on_num.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("Only instances have fields.", output[0]);
			Assert.AreEqual("[line 1]", output[1]);
		}

		[TestMethod]
		public void SetOnStringTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/field/set_on_string.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("Only instances have fields.", output[0]);
			Assert.AreEqual("[line 1]", output[1]);
		}

		[TestMethod]
		public void UndefinedTest()
		{
			string[] output = Tools.RunFile("/mnt/c/users/kai/source/repos/LoxLiaison/LoxLiaison/LoxTester/Tests/field/undefined.lox");
			Assert.AreEqual(2, output.Length);
			Assert.AreEqual("Undefined property 'bar'.", output[0]);
			Assert.AreEqual("[line 4]", output[1]);
		}

	}
}
