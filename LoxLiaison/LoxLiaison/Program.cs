using System;

namespace LoxLiaison
{
    /// <summary>
    /// Main entry point.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// LoxLiaison entry point.
        /// </summary>
        /// <param name="args">Optionally, a script for LL to run.</param>
        public static void Main(string[] args)
        {
            Console.WriteLine("LoxLiaison v1.0.0 - Kai NeSmith 2023");

            if (args.Length > 1)
            {
                Console.WriteLine("Usage: LoxLiaison [Lox script]");
                System.Environment.Exit(64);
            }
            else if (args.Length == 1)
            {
                Liaison.RunFile(args[0]);
            }
            else
            {
                Liaison.RunPrompt();
            }
        }
    }
}
