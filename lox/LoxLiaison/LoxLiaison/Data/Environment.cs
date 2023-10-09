using LoxLiaison.Exceptions;
using System.Collections.Generic;

namespace LoxLiaison.Data
{
    /// <summary>
    /// Represents an encompassing environment during runtime.
    /// </summary>
    public class Environment
    {
        /// <summary>
        /// The <see cref="Environment"/> that encloses this <see cref="Environment"/>.
        /// </summary>
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

        /// <summary>
        /// Defines a variable within this <see cref="Environment"/>.
        /// </summary>
        /// <param name="name">The name, or identifier, of the variable.</param>
        /// <param name="value">The value of the variable.</param>
        public void Define(string name, object value)
        {
            _values[name] = value;
        }

        /// <summary>
        /// Gets a variable from this <see cref="Environment"/>.
        /// </summary>
        /// <param name="name">The name, or identifier, of the variable.</param>
        /// <returns>The value of the variable.</returns>
        /// <exception cref="RuntimeException">Thrown when the variable is undefined.</exception>
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

        /// <summary>
        /// Gets a variable at a set distance.
        /// </summary>
        /// <param name="distance">The distance of the variable.</param>
        /// <param name="name">The name of the variable.</param>
        /// <returns>The value of the variable from within an ancestor environment.</returns>
        public object GetAt(int distance, string name)
        {
            return Ancestor(distance)._values[name];
        }

        public void AssignAt(int distance, Token name, object value)
        {
            Ancestor(distance)._values[name.Lexeme] = value;
        }

        /// <summary>
        /// Fetches the ancestor environment some distance away.
        /// </summary>
        /// <param name="distance">The distance of the ancestor environment.</param>
        /// <returns>The ancestor environment.</returns>
        public Environment Ancestor(int distance)
        {
            Environment env = this;
            for (int i = 0; i < distance; i++)
            {
                env = env.Enclosing;
            }

            return env;
        }

        /// <summary>
        /// Assigns a variable a new value.
        /// </summary>
        /// <param name="name">The name, or identifier, of the variable.</param>
        /// <param name="value">The new value of the variable.</param>
        /// <exception cref="RuntimeException">Thrown when the variable is undefined.</exception>
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
