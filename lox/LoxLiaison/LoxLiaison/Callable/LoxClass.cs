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
        public readonly LoxClass Superclass;
        private readonly Dictionary<string, LoxFunction> _methods;

        public LoxClass(string name, LoxClass superclass, Dictionary<string, LoxFunction> methods)
        {
            Name = name;
            Superclass = superclass;
            _methods = methods;
        }

        public LoxFunction FindMethod(string name)
        {
            if (_methods.ContainsKey(name))
            {
                return _methods[name];
            }

            if (Superclass != null)
            {
                return Superclass.FindMethod(name);
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
