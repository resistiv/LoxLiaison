using LoxLiaison.Exceptions;
using System.Collections.Generic;

namespace LoxLiaison
{
    public class Environment
    {
        public readonly Environment Enclosing;
        private readonly Dictionary<string, object> _values = new();

        public Environment()
        {
            Enclosing = null;
        }

        public Environment(Environment enclosing)
        {
            Enclosing = enclosing;
        }

        public void Define(string name, object value)
        {
            _values[name] = value;
        }

        public object Get(Token name)
        {
            if (_values.ContainsKey(name.Lexeme))
            {
                return _values[name.Lexeme];
            }

            if (Enclosing != null)
            {
                return Enclosing.Get(name);
            }

            throw new RuntimeException(name, $"Undefined variable '{name.Lexeme}'.");
        }

        public void Assign(Token name, object value)
        {
            if (_values.ContainsKey(name.Lexeme))
            {
                _values[name.Lexeme] = value;
                return;
            }

            if (Enclosing != null)
            {
                Enclosing.Assign(name, value);
                return;
            }

            throw new RuntimeException(name, $"Undefined variable '{name.Lexeme}'.");
        }
    }
}
