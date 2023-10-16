using LoxLiaison.Data;
using LoxLiaison.Exceptions;
using System.Collections.Generic;

namespace LoxLiaison.Callable
{
    public class LoxInstance
    {
        private LoxClass _class;
        private readonly Dictionary<string, object> _fields = new();

        public LoxInstance(LoxClass @class)
        {
            _class = @class;
        }

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
