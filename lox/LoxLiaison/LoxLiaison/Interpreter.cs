using System;
using System.Collections.Generic;
using LoxLiaison.Functions;

namespace LoxLiaison
{
    public class Interpreter : Expr.IVisitor<object>, Stmt.IVisitor<object>
    {
        public readonly Environment Globals = new();
        private Environment _environment;

        public Interpreter()
        {
            _environment = Globals;

            Globals.Define("clock", new Functions.Native.Clock());
        }

        /// <summary>
        /// Interprets a <see cref="List{T}"/> of <see cref="Stmt"/>s.
        /// </summary>
        /// <param name="statements"></param>
        public void Interpret(List<Stmt> statements)
        {
            try
            {
                for (int i = 0; i < statements.Count; i++)
                {
                    Execute(statements[i]);
                }
            }
            catch (RuntimeException e)
            {
                Liaison.RuntimeError(e);
            }
        }

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

        public object VisitAssignExpr(Expr.Assign expr)
        {
            object value = Evaluate(expr.Value);
            _environment.Assign(expr.Name, value);
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
                        return sl + sr;
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
            for (int i = 0; i < expr.Arguments.Count; i++)
            {
                arguments.Add(Evaluate(expr.Arguments[i]));
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
            return _environment.Get(expr.Name);
        }

        public object VisitExpressionStmt(Stmt.Expression stmt)
        {
            Evaluate(stmt.Expr);
            return null;
        }

        public object VisitFunctionStmt(Stmt.Function stmt)
        {
            LoxFunction function = new(stmt);
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

        public object VisitBlockStmt(Stmt.Block stmt)
        {
            ExecuteBlock(stmt.Statements, new Environment(_environment));
            return null;
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

        public void ExecuteBlock(List<Stmt> statements, Environment environment)
        {
            Environment previous = this._environment;
            try
            {
                this._environment = environment;

                for (int i = 0; i < statements.Count; i++)
                {
                    Execute(statements[i]);
                }
            }
            finally
            {
                this._environment = previous;
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
    }
}
