using System;
using System.Collections.Generic;
using System.IO;

namespace LoxLiaison
{
    public class Program
    {
        /// <summary>
        /// LoxLiaison entry point.
        /// </summary>
        /// <param name="args">Optionally, a script for LL to run.</param>
        public static void Main(string[] args)
        {
            if (args.Length > 1)
            {
                Console.WriteLine("Usage: loxliaison [script]");
                Environment.Exit(64);
            }
            else if (args.Length == 1)
            {
                RunFile(args[0]);
            }
            else
            {
                RunPrompt();
            }
        }

        /// <summary>
        /// Runs a Lox file.
        /// </summary>
        /// <param name="path">A path to a Lox file.</param>
        private static void RunFile(string path)
        {
            string fileContents = File.ReadAllText(path);
            Run(fileContents);
        }

        /// <summary>
        /// Runs LL as a prompt;
        /// </summary>
        private static void RunPrompt()
        {
            while (true)
            {
                Console.WriteLine("> ");
                string line = Console.ReadLine();
                if (line == null)
                {
                    break;
                }
                Run(line);
            }
        }

        /// <summary>
        /// Runs some quantity of Lox code.
        /// </summary>
        /// <param name="source"></param>
        private static void Run(string source)
        {
            Scanner scanner = new Scanner(source);
            List<Token> tokens = scanner.scanTokens();

            for (int i = 0; i < tokens.Count; i++)
            {
                Console.WriteLine(tokens[i]);
            }
        }
    }
}
