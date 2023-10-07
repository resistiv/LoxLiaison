using LoxLiaison.Data;
using System.Collections.Generic;

namespace LoxLiaison.Utils
{
    public class Resolver : Expr.IVisitor<object>, Stmt.IVisitor<object>
    {
        private readonly Interpreter _interpreter;
        private readonly Stack<Dictionary<string, bool>> _scopes = new();

        public Resolver(Interpreter interpreter)
        {
            _interpreter = interpreter;
        }

        /// <summary>
        /// Resolves a statement.
        /// </summary>
        /// <param name="stmt">A <see cref="Stmt"/> to resolve.</param>
        private void Resolve(Stmt stmt)
        {
            stmt.Accept(this);
        }

        /// <summary>
        /// Resolves an expression.
        /// </summary>
        /// <param name="expr">An <see cref="Expr"/> to resolve.</param>
        private void Resolve(Expr expr)
        {
            expr.Accept(this);
        }

        /// <summary>
        /// Resolves a list of statements.
        /// </summary>
        /// <param name="statements">A <see cref="List{T}"/> of <see cref="Stmt"/>s to resolve.</param>
        private void Resolve(List<Stmt> statements)
        {
            for (int i = 0; i < statements.Count; i++)
            {
                Resolve(statements[i]);
            }
        }

        /// <summary>
        /// Creates a new scope.
        /// </summary>
        private void BeginScope()
        {
            _scopes.Push(new Dictionary<string, bool>());
        }

        /// <summary>
        /// Closes the current scope.
        /// </summary>
        private void EndScope()
        {
            _scopes.Pop();
        }

        public object VisitBlockStmt(Stmt.Block stmt)
        {
            BeginScope();
            Resolve(stmt.Statements);
            EndScope();
            return null;
        }
    }
}
