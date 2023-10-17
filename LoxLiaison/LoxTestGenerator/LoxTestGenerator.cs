using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LoxTestGenerator
{
    /// <summary>
    /// Generates MSTest unit tests given a directory of Lox tests.
    /// </summary>
    public static class LoxTestGenerator
    {
        /// <summary>
        /// Main entry point.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        public static void Main(string[] args)
        {
            Console.WriteLine("LoxTestGenerator v1.0.0 - Kai NeSmith 2023");

            // We need both directories!
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: LoxTestGenerator <test directory> <output directory>");
                Environment.Exit(0);
            }

            // Time to see if our user knows what they're doing
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

            // Parse it out!
            ParseTestClasses(args[0], args[1]);
        }

        /// <summary>
        /// Parses all directories into classes from the top-level test folder.
        /// </summary>
        /// <param name="testDir">The top-level test directory to start parsing from.</param>
        /// <param name="outDir">The output directory to write complete classes to.</param>
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
        /// <param name="testDir">The test directory to parse methods from.</param>
        /// <param name="outDir">The output directory to write complete classes to.</param>
        /// <param name="className">The name of the class that these methods will belong to.</param>
        private static void ParseTests(string testDir, string outDir, string className)
        {
            // Write header info
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

            // Parse each method
            string[] fileNames = Directory.GetFiles(testDir);
            foreach (string file in fileNames)
            {
                string methodName = Methodize(file);
                sb.AppendLine("\t\t[TestMethod]");
                sb.AppendLine($"\t\tpublic void {methodName}()");
                sb.AppendLine("\t\t{");

                sb.AppendLine($"\t\t\tstring[] output = Tools.RunFile(\"{file.Replace("\\", "\\\\")}\");");

                // Well, what do we expect?
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

            // Write it out!
            File.WriteAllText($"{outDir}{Path.DirectorySeparatorChar}{Path.GetFileName(className)}.cs", sb.ToString());
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Wrote {className}.cs");
        }

        /// <summary>
        /// Gets all expected output from a unit test.
        /// </summary>
        /// <param name="file">The path of the unit test file.</param>
        /// <returns>An array of <see cref="string"/>s representing the expected output lines.</returns>
        private static string[] GetExpectations(string file)
        {
            // Abandon hope, all ye who enter here.

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

        /// <summary>
        /// Uppercases the first letter of a string.
        /// </summary>
        /// <param name="str">A <see cref="string"/> to capitalize.</param>
        /// <returns>A capitalized <see cref="string"/>.</returns>
        private static string Capitalize(string str)
        {
            return $"{char.ToUpper(str[0])}{str[1..]}";
        }

        /// <summary>
        /// Converts a directory path to a class name.
        /// </summary>
        /// <param name="testDir">A path to a test directory.</param>
        /// <returns>A class name.</returns>
        private static string Classify(string testDir)
        {
            // We use GetFileName since it returns the last identifier in the path chain, unlike GetDirectoryName...
            testDir = Path.GetFileName(testDir);

            // Split at underscores, capitalize, and recombine
            string[] words = testDir.Split('_');
            StringBuilder sb = new();
            foreach (string word in words)
            {
                sb.Append(Capitalize(word));
            }
            sb.Append("Tests");

            return sb.ToString();
        }

        /// <summary>
        /// Converts a file path to a method name.
        /// </summary>
        /// <param name="loxFile">A path to a unit test file.</param>
        /// <returns>A method name.</returns>
        private static string Methodize(string loxFile)
        {
            // Strip extension
            loxFile = Path.GetFileNameWithoutExtension(loxFile);

            // Edge case for test cases starting with numbers
            if (char.IsDigit(loxFile[0]))
            {
                loxFile = $"M{loxFile}";
            }

            // Split at underscores, capitalize, and recombine
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