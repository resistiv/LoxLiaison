using LoxLiaison.Data;
using LoxLiaison.Callable;
using System.Collections.Generic;

namespace LoxLiaison.Utils
{
    /// <summary>
    /// Handles resolving and binding.
    /// </summary>
    public class Resolver : Expr.IVisitor<object>, Stmt.IVisitor<object>
    {
        private readonly Interpreter _interpreter;
        private readonly Stack<Dictionary<string, bool>> _scopes = new();
        private FunctionType _currentFunction = FunctionType.None;
        private ClassType _currentClass = ClassType.None;

        public Resolver(Interpreter interpreter)
        {
            _interpreter = interpreter;
        }

        /// <summary>
        /// Resolves a list of statements.
        /// </summary>
        /// <param name="statements">A <see cref="List{T}"/> of <see cref="Stmt"/>s to resolve.</param>
        public void Resolve(List<Stmt> statements)
        {
            foreach (Stmt statement in statements)
            {
                Resolve(statement);
            }
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
        /// Resolves a function.
        /// </summary>
        /// <param name="function">A function to resolve.</param>
        private void ResolveFunction(Stmt.Function function, FunctionType type)
        {
            FunctionType enclosingFunction = _currentFunction;
            _currentFunction = type;

            BeginScope();
            foreach (Token param in function.Params)
            {
                Declare(param);
                Define(param);
            }
            Resolve(function.Body);
            EndScope();

            _currentFunction = enclosingFunction;
        }

        /// <summary>
        /// Resolves a local variable by traversing scopes.
        /// </summary>
        /// <param name="expr">The expression to evaluate with the value of the local variable.</param>
        /// <param name="name">The name of the local variable.</param>
        private void ResolveLocal(Expr expr, Token name)
        {
            for (int i = 0; i < _scopes.Count; i++)
            //for (int i = _scopes.Count - 1; i >= 0; i--)
            {
                if (_scopes.ToArray()[i].ContainsKey(name.Lexeme))
                //if (_scopes.ToArray()[_scopes.Count - 1 - i].ContainsKey(name.Lexeme))
                {
                    _interpreter.Resolve(expr, i);
                    //_interpreter.Resolve(expr, _scopes.Count - 1 - i);
                    return;
                }
            }

            // This function has been giving me some trouble, but I think I've got it figured out:
            // Java's .get() accessor on stacks treats the stack like a FIFO array,
            // while C#'s .ToArray() flattens the stack to LIFO order, meaning
            // this loop needs to be reversed.
            // It also means that depth is reflected by i implicitly.
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

        /// <summary>
        /// Declares a variable within the current scope.
        /// </summary>
        /// <param name="name">The name of the variable to declare.</param>
        private void Declare(Token name)
        {
            if (_scopes.Count == 0)
            {
                return;
            }

            Dictionary<string, bool> scope = _scopes.Peek();
            if (scope.ContainsKey(name.Lexeme))
            {
                Liaison.Error(name, "Already a variable with this name in this scope.");
            }

            scope[name.Lexeme] = false;
        }

        /// <summary>
        /// Marks a variable as defined within the current scope.
        /// </summary>
        /// <param name="name">The name of the variable to define.</param>
        private void Define(Token name)
        {
            if (_scopes.Count == 0)
            {
                return;
            }

            _scopes.Peek()[name.Lexeme] = true;
        }

        #region Statement Visitors

        public object VisitBlockStmt(Stmt.Block stmt)
        {
            BeginScope();
            Resolve(stmt.Statements);
            EndScope();
            return null;
        }

        public object VisitClassStmt(Stmt.Class stmt)
        {
            ClassType enclosingClass = _currentClass;
            _currentClass = ClassType.Class;

            Declare(stmt.Name);
            Define(stmt.Name);

            if (stmt.Superclass != null)
            {
                if (stmt.Name.Lexeme.Equals(stmt.Superclass.Name.Lexeme))
                {
                    Liaison.Error(stmt.Superclass.Name, "A class can't inherit from itself.");
                }

                _currentClass = ClassType.Subclass;
                Resolve(stmt.Superclass);

                BeginScope();
                _scopes.Peek().Add("super", true);
            }

            BeginScope();
            _scopes.Peek().Add("this", true);

            foreach (Stmt.Function method in stmt.Methods)
            {
                FunctionType declaration = FunctionType.Method;
                if (method.Name.Lexeme.Equals("init"))
                {
                    declaration = FunctionType.Initializer;
                }

                ResolveFunction(method, declaration);
            }

            EndScope();

            if (stmt.Superclass != null)
            {
                EndScope();
            }

            _currentClass = enclosingClass;
            return null;
        }

        public object VisitExpressionStmt(Stmt.Expression stmt)
        {
            Resolve(stmt.Expr);
            return null;
        }

        public object VisitFunctionStmt(Stmt.Function stmt)
        {
            Declare(stmt.Name);
            Define(stmt.Name);

            ResolveFunction(stmt, FunctionType.Function);
            return null;
        }

        public object VisitIfStmt(Stmt.If stmt)
        {
            Resolve(stmt.Condition);
            Resolve(stmt.ThenBranch);
            if (stmt.ElseBranch != null)
            {
                Resolve(stmt.ElseBranch);
            }
            return null;
        }

        public object VisitPrintStmt(Stmt.Print stmt)
        {
            Resolve(stmt.Expr);
            return null;
        }

        public object VisitReturnStmt(Stmt.Return stmt)
        {
            if (_currentFunction == FunctionType.None)
            {
                Liaison.Error(stmt.Keyword, "Can't return from top-level code.");
            }

            if (stmt.Value != null)
            {
                if (_currentFunction == FunctionType.Initializer)
                {
                    Liaison.Error(stmt.Keyword, "Can't return a value from an initializer.");
                }

                Resolve(stmt.Value);
            }

            return null;
        }

        public object VisitVarStmt(Stmt.Var stmt)
        {
            Declare(stmt.Name);
            if (stmt.Initializer != null)
            {
                Resolve(stmt.Initializer);
            }
            Define(stmt.Name);
            return null;
        }

        public object VisitWhileStmt(Stmt.While stmt)
        {
            Resolve(stmt.Condition);
            Resolve(stmt.Body);
            return null;
        }

        #endregion

        #region Expression Visitors

        public object VisitAssignExpr(Expr.Assign expr)
        {
            Resolve(expr.Value);
            ResolveLocal(expr, expr.Name);
            return null;
        }

        public object VisitBinaryExpr(Expr.Binary expr)
        {
            Resolve(expr.Left);
            Resolve(expr.Right);
            return null;
        }

        public object VisitCallExpr(Expr.Call expr)
        {
            Resolve(expr.Callee);

            foreach (Expr argument in expr.Arguments)
            {
                Resolve(argument);
            }

            return null;
        }

        public object VisitGetExpr(Expr.Get expr)
        {
            Resolve(expr.Object);
            return null;
        }

        public object VisitGroupingExpr(Expr.Grouping expr)
        {
            Resolve(expr.Expression);
            return null;
        }

        public object VisitLiteralExpr(Expr.Literal expr)
        {
            return null;
        }

        public object VisitLogicalExpr(Expr.Logical expr)
        {
            Resolve(expr.Left);
            Resolve(expr.Right);
            return null;
        }

        public object VisitSetExpr(Expr.Set expr)
        {
            Resolve(expr.Value);
            Resolve(expr.Object);
            return null;
        }

        public object VisitSuperExpr(Expr.Super expr)
        {
            if (_currentClass == ClassType.None)
            {
                Liaison.Error(expr.Keyword, "Can't use 'super' outside of a class.");
            }
            else if (_currentClass != ClassType.Subclass)
            {
                Liaison.Error(expr.Keyword, "Can't use 'super' in a class with no superclass.");
            }

            ResolveLocal(expr, expr.Keyword);
            return null;
        }

        public object VisitThisExpr(Expr.This expr)
        {
            if (_currentClass == ClassType.None)
            {
                Liaison.Error(expr.Keyword, "Can't use 'this' outside of a class.");
                return null;
            }

            ResolveLocal(expr, expr.Keyword);
            return null;
        }

        public object VisitUnaryExpr(Expr.Unary expr)
        {
            Resolve(expr.Right);
            return null;
        }

        public object VisitVariableExpr(Expr.Variable expr)
        {
            //if (_scopes.Count != 0 && _scopes.Peek().TryGetValue(expr.Name.Lexeme, out bool outVal) && outVal == false)
            //if (_scopes.Count != 0 && _scopes.Peek()[expr.Name.Lexeme] == false)
            if (_scopes.Count != 0 && _scopes.Peek().ContainsKey(expr.Name.Lexeme) && _scopes.Peek()[expr.Name.Lexeme] == false)
            {
                Liaison.Error(expr.Name, "Can't read local variable in its own initializer.");
            }

            ResolveLocal(expr, expr.Name);
            return null;
        }

        #endregion
    }
}
