using System;

namespace LoxLiaison.Exceptions
{
    public class RuntimeException : Exception
    {
        public readonly Token Token;

        public RuntimeException(Token token, string message)
            : base(message)
        {
            Token = token;
        }
    }
}
