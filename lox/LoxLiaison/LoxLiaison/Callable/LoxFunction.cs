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

        /// <summary>
        /// Binds this function to an instance.
        /// </summary>
        /// <param name="instance">A parent <see cref="LoxInstance"/> to bind to.</param>
        /// <returns>The bound <see cref="LoxFunction"/>.</returns>
        public LoxFunction Bind(LoxInstance instance)
        {
            Environment env = new(_closure);
            env.Define("this", instance);
            return new LoxFunction(_declaration, env);
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
