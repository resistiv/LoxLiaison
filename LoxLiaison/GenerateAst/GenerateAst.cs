using System;
using System.Collections.Generic;
using System.IO;

namespace LoxLiaison.Tools
{
    /// <summary>
    /// Handles generation of classes for the abstract syntax tree.
    /// </summary>
    public class GenerateAst
    {
        /// <summary>
        /// Holds all expressions to be generated.
        /// </summary>
        private static readonly List<string> ExprList = new()
        {
            "Assign : Token name, Expr value",
            "Binary : Expr left, Token @operator, Expr right",
            "Call : Expr callee, Token paren, List<Expr> arguments",
            "Get : Expr @object, Token name",
            "Grouping : Expr expression",
            "Literal : object value",
            "Logical : Expr left, Token @operator, Expr right",
            "Set : Expr @object, Token name, Expr value",
            "Super : Token keyword, Token method",
            "This : Token keyword",
            "Unary : Token @operator, Expr right",
            "Variable : Token name"
        };
        /// <summary>
        /// Holds all statements to be generated.
        /// </summary>
        private static readonly List<string> StmtList = new()
        {
            "Block : List<Stmt> statements",
            "Class : Token name, Expr.Variable superclass, List<Stmt.Function> methods",
            "Expression : Expr expr",
            "Function : Token name, List<Token> @params, List<Stmt> body",
            "If : Expr condition, Stmt thenBranch, Stmt elseBranch",
            "Print : Expr expr",
            "Return : Token keyword, Expr value",
            "Var : Token name, Expr initializer",
            "While : Expr condition, Stmt body"
        };

        /// <summary>
        /// Main entry point.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        public static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.Error.WriteLine("Usage: GenerateAst <output directory>");
                Environment.Exit(64);
            }

            string outputDir = args[0];

            DefineAst(outputDir, "Expr", ExprList);
            DefineAst(outputDir, "Stmt", StmtList);
        }

        /// <summary>
        /// Defines an abstract syntax tree.
        /// </summary>
        /// <param name="outputDir">A directory path to output the file to.</param>
        /// <param name="baseName">The name of the base class to generate.</param>
        /// <param name="types">A list of subclasses of the base class.</param>
        private static void DefineAst(string outputDir, string baseName, List<string> types)
        {
            string path = $"{outputDir}/{baseName}.cs";
            StreamWriter writer = File.CreateText(path);

            writer.WriteLine("// Generated using GenerateAst.");
            writer.WriteLine();
            writer.WriteLine("using System.Collections.Generic;");
            writer.WriteLine("using LoxLiaison.Data;");
            writer.WriteLine();
            writer.WriteLine("namespace LoxLiaison");
            writer.WriteLine("{");
            writer.WriteLine($"    public abstract class {baseName}");
            writer.WriteLine("    {");

            // Base Accept() method
            writer.WriteLine("        public abstract T Accept<T>(IVisitor<T> visitor);");
            writer.WriteLine();

            DefineVisitor(writer, baseName, types);

            for (int i = 0; i < types.Count; i++)
            {
                string className = types[i].Split(':')[0].Trim();
                string fieldList = types[i].Split(':')[1].Trim();
                DefineType(writer, baseName, className, fieldList);
            }

            writer.WriteLine("    }");
            writer.WriteLine("}");
            writer.Close();
        }

        /// <summary>
        /// Defines a particular subclass of the base class.
        /// </summary>
        /// <param name="writer">A <see cref="StreamWriter"/> set to the current output file.</param>
        /// <param name="baseName">The base class' name.</param>
        /// <param name="className">The name of the subclass.</param>
        /// <param name="fieldList">A list of the fields in the subclass.</param>
        private static void DefineType(StreamWriter writer, string baseName, string className, string fieldList)
        {
            writer.WriteLine();

            // Class header
            writer.WriteLine($"        public class {className} : {baseName}");
            writer.WriteLine("        {");

            // Write fields
            string[] fields = fieldList.Split(", ");
            for (int i = 0; i < fields.Length; i++)
            {
                string type = fields[i].Split(' ')[0];
                string name = fields[i].Split(' ')[1];
                name = GetFieldName(name);
                writer.WriteLine($"            public readonly {type} {name};");
            }
            writer.WriteLine("            ");

            // Write constructor
            writer.WriteLine($"            public {className}({fieldList})");
            writer.WriteLine("            {");
            for (int i = 0; i < fields.Length; i++)
            {
                string name = fields[i].Split(' ')[1];
                writer.WriteLine($"                this.{GetFieldName(name)} = {name};");
            }
            writer.WriteLine("            }");


            // Override visitor pattern
            writer.WriteLine();
            writer.WriteLine($"            public override T Accept<T>(IVisitor<T> visitor)");
            writer.WriteLine("            {");
            writer.WriteLine($"                return visitor.Visit{className}{baseName}(this);");
            writer.WriteLine("            }");

            writer.WriteLine("        }");
        }

        /// <summary>
        /// Defines a visitor for this abstract syntax tree.
        /// </summary>
        /// <param name="writer">A <see cref="StreamWriter"/> set to the current output file.</param>
        /// <param name="baseName">The base class' name.</param>
        /// <param name="types">A list of subclasses of the base class.</param>
        private static void DefineVisitor(StreamWriter writer, string baseName, List<string> types)
        {
            writer.WriteLine("        public interface IVisitor<T>");
            writer.WriteLine("        {");
            for (int i = 0; i < types.Count; i++)
            {
                string typeName = types[i].Split(':')[0].Trim();
                writer.WriteLine($"            public T Visit{typeName}{baseName}({typeName} {baseName.ToLower()});");

            }
            writer.WriteLine("        }");
        }

        /// <summary>
        /// Gets the field name of a field (capitalized first letter).
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static string GetFieldName(string name)
        {
            int i = name.StartsWith('@') ? 1 : 0;
            return $"{char.ToUpper(name[i])}{name[(i + 1)..name.Length]}";
        }
    }
}