using System.Collections.Generic;

namespace LoxLiaison.Functions
{
    public interface ILoxCallable
    {
        public int Arity();
        public object Call(Interpreter interpreter, List<object> arguments);
    }
}
