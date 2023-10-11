using LoxLiaison.Data;
using LoxLiaison.Exceptions;
using LoxLiaison.Utils;
using System.Collections.Generic;

namespace LoxLiaison.Callable
{
    /// <summary>
    /// Represents a user-defined function within Lox.
    /// </summary>
    public class LoxFunction : ILoxCallable
    {
        private readonly Stmt.Function _declaration;
        private readonly Environment _closure;

        public LoxFunction(Stmt.Function declaration, Environment closure)
        {
            _declaration = declaration;
            _closure = closure;
        }

        public int Arity()
        {
            return _declaration.Params.Count;
        }

        public object Call(Interpreter interpreter, List<object> arguments)
        {
            Environment env = new(_closure);
            for (int i = 0; i < _declaration.Params.Count; i++)
            {
                env.Define(_declaration.Params[i].Lexeme, arguments[i]);
            }

            try
            {
                interpreter.ExecuteBlock(_declaration.Body, env);
            }
            catch (ReturnException r)
            {
                return r.Value;
            }

            return null;
        }

        public override string ToString()
        {
            return $"<fn {_declaration.Name.Lexeme}>";
        }
    }
}
