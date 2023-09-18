using System;
using System.Collections.Generic;
using System.IO;

namespace LoxLiaison
{
    /// <summary>
    /// Main entry and utility class.
    /// </summary>
    public class Liaison
    {
        /// <summary>
        /// Whether the interpreter has encountered an error.
        /// </summary>
        private static bool HadError = false;

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

            // An error occurred, exit!
            if (HadError)
            {
                Environment.Exit(65);
            }
        }

        /// <summary>
        /// Runs LL as a REPL prompt.
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

                // Reset error flag
                HadError = false;
            }
        }

        /// <summary>
        /// Runs some quantity of Lox code.
        /// </summary>
        /// <param name="source">A piece of Lox code.</param>
        private static void Run(string source)
        {
            Scanner scanner = new Scanner(source);
            List<Token> tokens = scanner.scanTokens();

            for (int i = 0; i < tokens.Count; i++)
            {
                Console.WriteLine(tokens[i]);
            }
        }
    
        /// <summary>
        /// Prints an error.
        /// </summary>
        /// <param name="line">The line number of where the error occurred.</param>
        /// <param name="message">A message addressing the error.</param>
        internal static void Error(int line, string message)
        {
            Report(line, "", message);
        }

        /// <summary>
        /// Reports a message.
        /// </summary>
        /// <param name="line">The line number of where the message originates.</param>
        /// <param name="where">Where the message originates from.</param>
        /// <param name="message">A message.</param>
        private static void Report(int line, string where, string message)
        {
            Console.WriteLine($"[Line {line}] Error in {where}: {message}");
        }
    }
}
