using System;
using System.Collections.Generic;
using LoxLiaison.Utils;

namespace LoxLiaison.Functions.Native
{
    /// <summary>
    /// Handles runtime time calculation.
    /// </summary>
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
