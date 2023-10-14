using LoxLiaison.Utils;
using System.Collections.Generic;

namespace LoxLiaison.Callable
{
    /// <summary>
    /// Handles class definitions.
    /// </summary>
    public class LoxClass : ILoxCallable
    {
        public readonly string Name;
        private readonly Dictionary<string, LoxFunction> _methods;

        public LoxClass(string name, Dictionary<string, LoxFunction> methods)
        {
            Name = name;
            _methods = methods;
        }

        public LoxFunction FindMethod(string name)
        {
            if (_methods.ContainsKey(name))
            {
                return _methods[name];
            }

            return null;
        }

        public int Arity()
        {
            LoxFunction initializer = FindMethod("init");
            return initializer == null ? 0 : initializer.Arity();
        }

        public object Call(Interpreter interpreter, List<object> arguments)
        {
            LoxInstance instance = new(this);
            LoxFunction initializer = FindMethod("init");
            initializer?.Bind(instance).Call(interpreter, arguments);
            return instance;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
