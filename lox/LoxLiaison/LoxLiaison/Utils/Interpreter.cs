using System;
using System.Collections.Generic;
using LoxLiaison.Callable;
using LoxLiaison.Data;
using LoxLiaison.Exceptions;

namespace LoxLiaison.Utils
{
    /// <summary>
    /// Handles interpreting the parsed syntax tree.
    /// </summary>
    public class Interpreter : Expr.IVisitor<object>, Stmt.IVisitor<object>
    {
        public readonly Data.Environment Globals = new();
        private Data.Environment _environment;
        private readonly Dictionary<Expr, int> _locals = new();

        public Interpreter()
        {
            _environment = Globals;

            // Define native functions
            Globals.Define("clock", new Callable.Native.Clock());
        }

        /// <summary>
        /// Evaluates an expression.
        /// </summary>
        /// <param name="expr">An <see cref="Expr"/> to evaluate.</param>
        /// <returns>The result of the evaluation.</returns>
        private object Evaluate(Expr expr)
        {
            return expr.Accept(this);
        }

        /// <summary>
        /// Executes a statement.
        /// </summary>
        /// <param name="stmt">A <see cref="Stmt"/> to execute.</param>
        private void Execute(Stmt stmt)
        {
            stmt.Accept(this);
        }

        /// <summary>
        /// Saves the depth of an expression within scopes.
        /// </summary>
        /// <param name="expr">The expression itself.</param>
        /// <param name="depth">The depth of the expression.</param>
        public void Resolve(Expr expr, int depth)
        {
            _locals.Add(expr, depth);
        }

        /// <summary>
        /// Executes a block of statements.
        /// </summary>
        /// <param name="statements">A <see cref="List{T}"/> of <see cref="Stmt"/> to execute.</param>
        /// <param name="environment">The <see cref="Data.Environment"/> to execute the statements within.</param>
        public void ExecuteBlock(List<Stmt> statements, Data.Environment environment)
        {
            Data.Environment previous = _environment;
            try
            {
                _environment = environment;

                for (int i = 0; i < statements.Count; i++)
                {
                    Execute(statements[i]);
                }
            }
            finally
            {
                _environment = previous;
            }
        }

        /// <summary>
        /// Interprets a <see cref="List{T}"/> of <see cref="Stmt"/>s.
        /// </summary>
        /// <param name="statements"></param>
        public void Interpret(List<Stmt> statements)
        {
            try
            {
                foreach (Stmt statement in statements)
                {
                    Execute(statement);
                }
            }
            catch (RuntimeException e)
            {
                Liaison.RuntimeError(e);
            }
        }

        /// <summary>
        /// Checks the operand of an unary expression.
        /// </summary>
        /// <param name="operator">The operator to apply to the operand.</param>
        /// <param name="operand">The operand in question.</param>
        /// <exception cref="RuntimeException">Thrown when the operand is not a <see cref="double"/>.</exception>
        private static void CheckNumberOperand(Token @operator, object operand)
        {
            if (operand is double)
            {
                return;
            }
            else
            {
                throw new RuntimeException(@operator, "Operand must be a number.");
            }
        }
        
        /// <summary>
        /// Checks the operands of a binary expression.
        /// </summary>
        /// <param name="operator">The operator to apply to the operands.</param>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <exception cref="RuntimeException">Thrown when either operand is not a <see cref="double"/>.</exception>
        private static void CheckNumberOperands(Token @operator, object left, object right)
        {
            if (left is double && right is double)
            {
                return;
            }
            else
            {
                throw new RuntimeException(@operator, "Operands must be numbers.");
            }
        }

        /// <summary>
        /// Determines if two <see cref="object"/>s are equal.
        /// </summary>
        /// <param name="a">An <see cref="object"/> to compare.</param>
        /// <param name="b">Another <see cref="object"/> to compare.</param>
        /// <returns>Whether or not the <see cref="object"/>s are equal.</returns>
        private static bool IsEqual(object a, object b)
        {
            if (a == null && b == null)
            {
                return true;
            }
            if (a == null)
            {
                return false;
            }

            return a.Equals(b);
        }

        /// <summary>
        /// Determines if an object is truthy (not nil or false).
        /// </summary>
        /// <param name="obj">An <see cref="object"/> to test.</param>
        /// <returns>Whether or not the <see cref="object"/> is truthy.</returns>
        private static bool IsTruthy(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj is bool b)
            {
                return b;
            }

            return true;
        }

        /// <summary>
        /// Looks up a local variable.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="expr"></param>
        /// <returns></returns>
        private object LookUpVariable(Token name, Expr expr)
        {
            if (_locals.TryGetValue(expr, out int distance))
            {
                return _environment.GetAt(distance, name.Lexeme);
            }
            else
            {
                return Globals.Get(name);
            }
        }

        /// <summary>
        /// Converts an <see cref="object"/> to a <see cref="string"/>.
        /// </summary>
        /// <param name="obj">An <see cref="object"/> to convert.</param>
        /// <returns>A <see cref="string"/> representation of the <see cref="object"/>.</returns>
        private static string Stringify(object obj)
        {
            if (obj == null)
            {
                return "nil";
            }

            if (obj is double)
            {
                string text = obj.ToString();
                if (text.EndsWith(".0"))
                {
                    text = text[..^2];
                }
                return text;
            }

            return obj.ToString();
        }

        #region Expression Visitors

        public object VisitAssignExpr(Expr.Assign expr)
        {
            object value = Evaluate(expr.Value);

            if (_locals.TryGetValue(expr, out int distance))
            {
                _environment.AssignAt(distance, expr.Name, value);
            }
            else
            {
                Globals.Assign(expr.Name, value);
            }

            return value;
        }

        public object VisitBinaryExpr(Expr.Binary expr)
        {
            object left = Evaluate(expr.Left);
            object right = Evaluate(expr.Right);

            switch (expr.Operator.Type)
            {
                case TokenType.NotEqual:
                    return !IsEqual(left, right);
                case TokenType.EqualEqual:
                    return IsEqual(left, right);
                case TokenType.GreaterThan:
                    CheckNumberOperands(expr.Operator, left, right);
                    return (double)left > (double)right;
                case TokenType.GreaterThanEqual:
                    CheckNumberOperands(expr.Operator, left, right);
                    return (double)left >= (double)right;
                case TokenType.LessThan:
                    CheckNumberOperands(expr.Operator, left, right);
                    return (double)left < (double)right;
                case TokenType.LessThanEqual:
                    CheckNumberOperands(expr.Operator, left, right);
                    return (double)left <= (double)right;
                case TokenType.Minus:
                    CheckNumberOperands(expr.Operator, left, right);
                    return (double)left - (double)right;
                case TokenType.Plus:
                    if (left is double dl && right is double dr)
                    {
                        return dl + dr;
                    }
                    if (left is string sl && right is string sr)
                    {
                        return $"{sl}{sr}";
                    }
                    throw new RuntimeException(expr.Operator, "Operands must be two numbers or two strings.");
                case TokenType.Slash:
                    CheckNumberOperands(expr.Operator, left, right);
                    return (double)left / (double)right;
                case TokenType.Star:
                    CheckNumberOperands(expr.Operator, left, right);
                    return (double)left * (double)right;
            }

            return null;
        }

        public object VisitCallExpr(Expr.Call expr)
        {
            object callee = Evaluate(expr.Callee);

            List<object> arguments = new();
            foreach (Expr argument in expr.Arguments)
            {
                arguments.Add(Evaluate(argument));
            }

            if (callee is not ILoxCallable)
            {
                throw new RuntimeException(expr.Paren, "Can only call functions and classes.");
            }

            ILoxCallable function = (ILoxCallable)callee;
            if (arguments.Count != function.Arity())
            {
                throw new RuntimeException(expr.Paren, $"Expected {function.Arity()} arguments but got {arguments.Count}.");
            }

            return function.Call(this, arguments);
        }

        public object VisitGetExpr(Expr.Get expr)
        {
            object obj = Evaluate(expr.Object);

            if (obj is LoxInstance instance)
            {
                return instance.Get(expr.Name);
            }

            throw new RuntimeException(expr.Name, "Only instances have properties.");
        }

        public object VisitGroupingExpr(Expr.Grouping expr)
        {
            return Evaluate(expr.Expression);
        }

        public object VisitLiteralExpr(Expr.Literal expr)
        {
            return expr.Value;
        }

        public object VisitLogicalExpr(Expr.Logical expr)
        {
            object left = Evaluate(expr.Left);

            if (expr.Operator.Type == TokenType.Or)
            {
                if (IsTruthy(left))
                {
                    return left;
                }
            }
            else
            {
                if (!IsTruthy(left))
                {
                    return left;
                }
            }

            return Evaluate(expr.Right);
        }

        public object VisitSetExpr(Expr.Set expr)
        {
            object obj = Evaluate(expr.Object);

            if (obj is not LoxInstance)
            {
                throw new RuntimeException(expr.Name, "Only instances have fields.");
            }

            object value = Evaluate(expr.Value);
            ((LoxInstance)obj).Set(expr.Name, value);
            return value;
        }

        public object VisitSuperExpr(Expr.Super expr)
        {
            int distance = _locals[expr];
            LoxClass superclass = (LoxClass)_environment.GetAt(distance, "super");

            LoxInstance obj = (LoxInstance)_environment.GetAt(distance - 1, "this");

            LoxFunction method = superclass.FindMethod(expr.Method.Lexeme);

            if (method == null)
            {
                throw new RuntimeException(expr.Method, $"Undefined property '{expr.Method.Lexeme}'.");
            }

            return method.Bind(obj);
        }

        public object VisitThisExpr(Expr.This expr)
        {
            return LookUpVariable(expr.Keyword, expr);
        }

        public object VisitUnaryExpr(Expr.Unary expr)
        {
            object right = Evaluate(expr.Right);

            switch (expr.Operator.Type)
            {
                case TokenType.Not:
                    return !IsTruthy(right);
                case TokenType.Minus:
                    CheckNumberOperand(expr.Operator, right);
                    return -(double)right;
            }

            return null;
        }

        public object VisitVariableExpr(Expr.Variable expr)
        {
            return LookUpVariable(expr.Name, expr);
        }

        #endregion

        #region Statement Visitors

        public object VisitBlockStmt(Stmt.Block stmt)
        {
            ExecuteBlock(stmt.Statements, new Data.Environment(_environment));
            return null;
        }

        public object VisitClassStmt(Stmt.Class stmt)
        {
            object superclass = null;
            if (stmt.Superclass != null)
            {
                superclass = Evaluate(stmt.Superclass);
                if (superclass is not LoxClass)
                {
                    throw new RuntimeException(stmt.Superclass.Name, "Superclass must be a class.");
                }
            }

            _environment.Define(stmt.Name.Lexeme, null);

            if (stmt.Superclass != null)
            {
                _environment = new(_environment);
                _environment.Define("super", superclass);
            }

            Dictionary<string, LoxFunction> methods = new();

            foreach (Stmt.Function method in stmt.Methods)
            {
                LoxFunction func = new(method, _environment, method.Name.Lexeme.Equals("init"));
                methods.Add(method.Name.Lexeme, func);
            }

            LoxClass @class = new(stmt.Name.Lexeme, (LoxClass)superclass, methods);

            if (superclass != null)
            {
                _environment = _environment.Enclosing;
            }

            _environment.Assign(stmt.Name, @class);

            return null;
        }

        public object VisitExpressionStmt(Stmt.Expression stmt)
        {
            Evaluate(stmt.Expr);
            return null;
        }

        public object VisitFunctionStmt(Stmt.Function stmt)
        {
            LoxFunction function = new(stmt, _environment, false);
            _environment.Define(stmt.Name.Lexeme, function);
            return null;
        }

        public object VisitIfStmt(Stmt.If stmt)
        {
            if (IsTruthy(Evaluate(stmt.Condition)))
            {
                Execute(stmt.ThenBranch);
            }
            else if (stmt.ElseBranch != null)
            {
                Execute(stmt.ElseBranch);
            }
            return null;
        }

        public object VisitPrintStmt(Stmt.Print stmt)
        {
            object value = Evaluate(stmt.Expr);
            Console.WriteLine(Stringify(value));
            return null;
        }

        public object VisitReturnStmt(Stmt.Return stmt)
        {
            object value = null;
            if (stmt.Value != null)
            {
                value = Evaluate(stmt.Value);
            }

            throw new ReturnException(value);
        }

        public object VisitVarStmt(Stmt.Var stmt)
        {
            object value = null;
            if (stmt.Initializer != null)
            {
                value = Evaluate(stmt.Initializer);
            }

            _environment.Define(stmt.Name.Lexeme, value);
            return null;
        }

        public object VisitWhileStmt(Stmt.While stmt)
        {
            while (IsTruthy(Evaluate(stmt.Condition)))
            {
                Execute(stmt.Body);
            }
            return null;
        }

        #endregion
    }
}
