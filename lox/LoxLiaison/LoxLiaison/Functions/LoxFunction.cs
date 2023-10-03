using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoxLiaison.Functions
{
    public class LoxFunction : ILoxCallable
    {
        private readonly Stmt.Function _declaration;

        public LoxFunction(Stmt.Function declaration)
        {
            _declaration = declaration;
        }

        public int Arity()
        {
            return _declaration.Params.Count;
        }

        public object Call(Interpreter interpreter, List<object> arguments)
        {
            Environment env = new(interpreter.Globals);
            for (int i = 0; i < _declaration.Params.Count; i++)
            {
                env.Define(_declaration.Params[i].Lexeme, arguments[i]);
            }

            interpreter.ExecuteBlock(_declaration.Body, env);
            return null;
        }

        public override string ToString()
        {
            return $"<fn {_declaration.Name.Lexeme}>";
        }
    }
}
