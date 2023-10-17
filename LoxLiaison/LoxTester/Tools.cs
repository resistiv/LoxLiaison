using LoxLiaison;
using System;
using System.IO;

namespace LoxTester
{
    public static class Tools
    {
        /// <summary>
        /// Runs a file within LoxLiaison, reads the output into an array, and prints the output.
        /// </summary>
        /// <param name="filename">The name of the file to run.</param>
        /// <returns>Output of the script, split at newlines.</returns>
        public static string[] RunFile(string filename)
        {
            // Set recording StringWriter
            StringWriter sw = new();
            TextWriter original = Console.Out;
            Console.SetOut(sw);

            // Run the script
            Liaison.Run(File.ReadAllText(filename));

            // Reset console output and write out output
            Console.SetOut(original);
            Console.WriteLine(sw.ToString());

            // Reset the liaison to clear out state (errors, interpreter gunk)
            Liaison.DebugReset();
            //GC.Collect();

            // Return captured output
            return sw.ToString().Trim().Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
        }
    }
}
