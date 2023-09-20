using System;
using System.Collections.Generic;
using System.IO;

namespace LoxLiaison.Tool
{
    public class GenerateAst
    {
        private static readonly List<string> ExprList = new()
        {
            "Binary : Expr left, Token operator, Expr right",
            "Grouping : Expr expression",
            "Literal : object value",
            "Unary : Token operator, Expr right"
        };

        public static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.Error.WriteLine("Usage: GenerateAst <output directory>");
                Environment.Exit(64);
            }

            string outputDir = args[0];

            DefineAst(outputDir, "Expr", ExprList);
        }

        private static void DefineAst(string outputDir, string baseName, List<string> types)
        {
            string path = $"{outputDir}/{baseName}.cs";
            StreamWriter writer = File.CreateText(path);

            writer.WriteLine("using System.Collections.Generic;");
            writer.WriteLine();
            writer.WriteLine("namespace LoxLiaison");
            writer.WriteLine("{");
            writer.WriteLine($"    abstract class {baseName}");
            writer.WriteLine("    {");
        }
    }
}