using System;
using LoxLiaison.Data;

namespace LoxLiaison.Exceptions
{
    /// <summary>
    /// Represents a runtime exception.
    /// </summary>
    public class RuntimeException : Exception
    {
        /// <summary>
        /// The <see cref="Data.Token"/> that caused the exception.
        /// </summary>
        public readonly Token Token;

        public RuntimeException(Token token, string message)
            : base(message)
        {
            Token = token;
        }
    }
}
