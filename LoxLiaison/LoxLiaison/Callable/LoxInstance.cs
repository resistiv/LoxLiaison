using LoxLiaison.Data;
using LoxLiaison.Exceptions;
using System.Collections.Generic;

namespace LoxLiaison.Callable
{
    /// <summary>
    /// Represents an instance of a <see cref="LoxClass"/>.
    /// </summary>
    public class LoxInstance
    {
        private LoxClass _class;
        private readonly Dictionary<string, object> _fields = new();

        public LoxInstance(LoxClass @class)
        {
            _class = @class;
        }

        /// <summary>
        /// Gets the value of a property from this instance.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <returns>The value of the property.</returns>
        /// <exception cref="RuntimeException"></exception>
        public object Get(Token name)
        {
            if (_fields.ContainsKey(name.Lexeme))
            {
                return _fields[name.Lexeme];
            }

            LoxFunction method = _class.FindMethod(name.Lexeme);
            if (method != null)
            {
                return method.Bind(this);
            }

            throw new RuntimeException(name, $"Undefined property '{name.Lexeme}'.");
        }

        /// <summary>
        /// Sets a property of this instance.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="value">The value of the property.</param>
        public void Set(Token name, object value)
        {
            _fields[name.Lexeme] = value;
        }

        public override string ToString()
        {
            return $"{_class.Name} instance"; 
        }
    }
}
