using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LoxTestGenerator
{
    /// <summary>
    /// Generates MSTest unit tests given a directory of Lox tests.
    /// </summary>
    public static class Generator
    {
        public static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: LoxTestGenerator [test directory] [output directory]");
                Environment.Exit(0);
            }

            // Sanity
            if (!Directory.Exists(args[0]))
            {
                Console.WriteLine($"Error: Could not find test directory '{args[0]}'");
                Environment.Exit(-1);
            }
            else if (!Directory.Exists(args[1]))
            {
                Console.WriteLine($"Error: Could not find output directory '{args[1]}'");
                Environment.Exit(-1);
            }

            ParseTestClasses(args[0], args[1]);
        }

        /// <summary>
        /// Parses all directories into classes from the top-level test folder.
        /// </summary>
        /// <param name="testDir"></param>
        /// <param name="outDir"></param>
        private static void ParseTestClasses(string testDir, string outDir)
        {
            ParseTests(testDir, outDir, "GeneralTests");

            string[] dirs = Directory.GetDirectories(testDir);
            foreach (string dir in dirs)
            {
                // Skip excluded directories
                if (Path.GetFileName(dir).StartsWith("_"))
                {
                    continue;
                }
                string className = Classify(dir);
                ParseTests(dir, outDir, className);
            }
        }

        /// <summary>
        /// Parses a directory into a test class with test methods.
        /// </summary>
        /// <param name="testDir"></param>
        /// <param name="outDir"></param>
        /// <param name="className"></param>
        private static void ParseTests(string testDir, string outDir, string className)
        {
            StringBuilder sb = new();
            sb.AppendLine("// Generated using LoxTestGenerator");
            sb.AppendLine();
            sb.AppendLine("using Microsoft.VisualStudio.TestTools.UnitTesting;");
            sb.AppendLine();
            sb.AppendLine("namespace LoxTester");
            sb.AppendLine("{");
            sb.AppendLine("\t[TestClass]");
            sb.AppendLine($"\tpublic class {className}");
            sb.AppendLine("\t{");

            string[] fileNames = Directory.GetFiles(testDir);
            foreach (string file in fileNames)
            {
                string methodName = Methodize(file);
                sb.AppendLine("\t\t[TestMethod]");
                sb.AppendLine($"\t\tpublic void {methodName}()");
                sb.AppendLine("\t\t{");

                sb.AppendLine($"\t\t\tstring[] output = Tools.RunFile(\"{file.Replace("\\", "\\\\")}\");");

                string[] expectations = GetExpectations(file);
                sb.AppendLine($"\t\t\tAssert.AreEqual({expectations.Length}, output.Length);");
                for (int i = 0; i < expectations.Length; i++)
                {
                    sb.AppendLine($"\t\t\tAssert.AreEqual(\"{expectations[i]}\", output[{i}]);");
                }

                sb.AppendLine("\t\t}");
                sb.AppendLine();
            }

            sb.AppendLine("\t}");
            sb.AppendLine("}");

            File.WriteAllText($"{outDir}\\{Path.GetFileName(className)}.cs", sb.ToString());
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Wrote {className}.cs");
        }

        private static string[] GetExpectations(string file)
        {
            string[] lines = File.ReadAllLines(file);
            List<string> expectations = new();
            for (int i = 0; i < lines.Length; i++)
            {
                // Prep line
                lines[i] = lines[i].Replace("\"", "\\\"");

                // Do we have an expectation on this line?
                if (lines[i].Contains(" expect"))
                {
                    // Get whatever is after it
                    expectations.Add(lines[i].Split(new[] { ':' }, 2)[1].TrimStart());

                    // Is this expectation a runtime error?
                    if (lines[i].Contains("expect runtime error"))
                    {
                        // Add line number report
                        expectations.Add($"[line {i + 1}]");
                    }
                    // Raw value
                    else
                    {
                        // Normalize booleans to C# string style
                        if (expectations[^1].Equals("true") || expectations[^1].Equals("false"))
                        {
                            expectations[^1] = Capitalize(expectations[^1]);
                        }
                    }
                }
                // Edge case ignore c line
                else if (lines[i].Contains("[c line "))
                {
                    continue;
                }
                // Edge case line error expectations
                else if (lines[i].Contains("[line ") || lines[i].Contains("[java line "))
                {
                    // Normalize edge case of "[java line x]"
                    if (lines[i].Contains("[java line "))
                    {
                        lines[i] = lines[i].Replace("[java line ", "[line ");
                    }
                    // Trim off leading comment
                    expectations.Add(lines[i].TrimStart()[3..]);
                }
                // Plain errors
                else if (lines[i].Contains(" Error"))
                {
                    string line = lines[i].Split("// ")[1];
                    line = $"[line {i + 1}] {line}";
                    expectations.Add(line);
                }
            }

            // No output edge case
            if (expectations.Count == 0)
            {
                expectations.Add(string.Empty);
            }

            return expectations.ToArray();
        }

        private static string Capitalize(string str)
        {
            return $"{char.ToUpper(str[0])}{str[1..]}";
        }

        private static string Classify(string testDir)
        {
            // We use GetFileName since it returns the last identifier in the chain, unlike GetDirectoryName
            testDir = Path.GetFileName(testDir);
            string[] words = testDir.Split('_');

            StringBuilder sb = new();
            foreach (string word in words)
            {
                sb.Append(Capitalize(word));
            }
            return $"{sb.ToString()}Tests";
        }

        private static string Methodize(string loxFile)
        {
            loxFile = Path.GetFileNameWithoutExtension(loxFile);
            if (char.IsDigit(loxFile[0]))
            {
                loxFile = $"M{loxFile}";
            }
            string[] words = loxFile.Split('_');

            StringBuilder sb = new();
            foreach (string word in words)
            {
                sb.Append(Capitalize(word));
            }
            sb.Append("Test");

            return sb.ToString();
        }
    }
}