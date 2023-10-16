using System;
using System.Collections.Generic;
using System.IO;
using LoxLiaison.Data;
using LoxLiaison.Exceptions;
using LoxLiaison.Utils;

namespace LoxLiaison
{
    /// <summary>
    /// Main utility class.
    /// </summary>
    public class Liaison
    {
        private static Interpreter interpreter = new();
        private static bool HadError = false;
        private static bool HadRuntimeError = false;

        /// <summary>
        /// Runs a Lox file.
        /// </summary>
        /// <param name="path">A path to a Lox file.</param>
        public static void RunFile(string path)
        {
            // Is our user sane? Let's find out!
            if (!File.Exists(path))
            {
                Console.WriteLine($"Could not access file '{path}', aborting.");
                System.Environment.Exit(65);
            }

            string fileContents = File.ReadAllText(path);
            Run(fileContents);

            // An error occurred, exit!
            if (HadError)
            {
                System.Environment.Exit(65);
            }
            if (HadRuntimeError)
            {
                System.Environment.Exit(70);
            }
        }

        /// <summary>
        /// Runs LL as a REPL prompt.
        /// </summary>
        public static void RunPrompt()
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
        public static void Run(string source)
        {
            Scanner scanner = new(source);
            List<Token> tokens = scanner.ScanTokens();

            Parser parser = new(tokens);
            List<Stmt> statements = parser.Parse();

            // Stop if we encountered a syntax error.
            if (HadError)
            {
                return;
            }

            Resolver resolver = new(interpreter);
            resolver.Resolve(statements);

            if (HadError)
            {
                return;
            }

            interpreter.Interpret(statements);
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
                Report(token.Line, $" at '{token.Lexeme}'", message);
            }
        }

        /// <summary>
        /// Prints a runtime error.
        /// </summary>
        /// <param name="error">The <see cref="RuntimeException"/> representing the error.</param>
        public static void RuntimeError(RuntimeException error)
        {
            Console.WriteLine($"{error.Message}{System.Environment.NewLine}[line {error.Token.Line}]");
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
            Console.WriteLine($"[line {line}] Error{where}: {message}");
            HadError = true;
        }

        public static void DebugReset()
        {
            interpreter = new();
            HadError = false;
            HadRuntimeError = false;
        }
    }
}
