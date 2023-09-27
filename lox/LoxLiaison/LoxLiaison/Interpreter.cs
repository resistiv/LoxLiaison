using System;

namespace LoxLiaison
{
    public class Interpreter : Expr.IVisitor<object>
    {
        public void Interpret(Expr expression)
        {
            try
            {
                object value = Evaluate(expression);
                Console.WriteLine(Stringify(value));
            }
            catch (RuntimeException e)
            {
                Liaison.RuntimeError(e);
            }
        }

        private string Stringify(object obj)
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

        public object VisitGroupingExpr(Expr.Grouping expr)
        {
            return Evaluate(expr.Expression);
        }

        public object VisitLiteralExpr(Expr.Literal expr)
        {
            return expr.Value;
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

        /// <summary>
        /// Checks the operand of an unary expression.
        /// </summary>
        /// <param name="operator">The operator to apply to the operand.</param>
        /// <param name="operand">The operand in question.</param>
        /// <exception cref="RuntimeException">Thrown when the operand is not a <see cref="double"/>.</exception>
        private void CheckNumberOperand(Token @operator, object operand)
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
        private void CheckNumberOperands(Token @operator, object left, object right)
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
        /// Determines if two <see cref="object"/>s are equal.
        /// </summary>
        /// <param name="a">An <see cref="object"/> to compare.</param>
        /// <param name="b">Another <see cref="object"/> to compare.</param>
        /// <returns>Whether or not the <see cref="object"/>s are equal.</returns>
        private bool IsEqual(object a, object b)
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
