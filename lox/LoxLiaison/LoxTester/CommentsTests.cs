// Generated using LoxTestGenerator

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoxTester
{
	[TestClass]
	public class CommentsTests
	{
		[TestMethod]
		public void LineAtEofTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\comments\\line_at_eof.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("ok", output[0]);
		}

		[TestMethod]
		public void OnlyLineCommentTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\comments\\only_line_comment.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("", output[0]);
		}

		[TestMethod]
		public void OnlyLineCommentAndLineTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\comments\\only_line_comment_and_line.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("", output[0]);
		}

		[TestMethod]
		public void UnicodeTest()
		{
			string[] output = Tools.RunFile("C:\\Users\\nesmi\\source\\repos\\cs503\\lox\\LoxLiaison\\LoxTester\\Tests\\comments\\unicode.lox");
			Assert.AreEqual(1, output.Length);
			Assert.AreEqual("ok", output[0]);
		}

	}
}
