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
        private static readonly Interpreter interpreter = new();
        private static bool HadError = false;
        private static bool HadRuntimeError = false;

        /// <summary>
        /// LoxLiaison entry point.
        /// </summary>
        /// <param name="args">Optionally, a script for LL to run.</param>
        public static void Main(string[] args)
        {
            if (args.Length > 1)
            {
                Console.WriteLine("Usage: LoxLiaison [script]");
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
            if (HadRuntimeError)
            {
                Environment.Exit(70);
            }
        }

        /// <summary>
        /// Runs LL as a REPL prompt.
        /// </summary>
        private static void RunPrompt()
        {
            while (true)
            {
                Console.Write("> ");
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
            Scanner scanner = new(source);
            List<Token> tokens = scanner.ScanTokens();

            /*for (int i = 0; i < tokens.Count; i++)
            {
                Console.WriteLine(tokens[i]);
            }*/

            Parser parser = new(tokens);
            Expr expression = parser.Parse();

            // Stop if we encountered a syntax error.
            if (HadError)
            {
                return;
            }

            interpreter.Interpret(expression);
        }
    
        /// <summary>
        /// Prints an error.
        /// </summary>
        /// <param name="line">The line number of where the error occurred.</param>
        /// <param name="message">A message addressing the error.</param>
        public static void Error(int line, string message)
        {
            Report(line, "", message);
        }

        /// <summary>
        /// Prints an error.
        /// </summary>
        /// <param name="token">The <see cref="Token"/> that caused the error.</param>
        /// <param name="message">A message addressing the error.</param>
        public static void Error(Token token, string message)
        {
            if (token.Type == TokenType.Eof)
            {
                Report(token.Line, " at end", message);
            }
            else
            {
                Report(token.Line, $"at '{token.Lexeme}'", message);
            }
        }

        public static void RuntimeError(RuntimeException error)
        {
            Console.WriteLine($"{error.Message}\n[line {error.Token.Line}]");
            HadRuntimeError = true;
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
