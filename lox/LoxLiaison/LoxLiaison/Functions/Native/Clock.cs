using System;
using System.Collections.Generic;

namespace LoxLiaison.Functions.Native
{
    public class Clock : ILoxCallable
    {
        public int Arity()
        {
            return 0;
        }

        public object Call(Interpreter interpreter, List<object> arguments)
        {
            return (DateTime.UtcNow - DateTime.UnixEpoch).TotalMilliseconds / 1000.0;
        }

        public override string ToString()
        {
            return "<native fn>";
        }
    }
}
