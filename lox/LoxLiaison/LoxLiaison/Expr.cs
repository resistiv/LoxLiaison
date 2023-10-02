// Generated using GenerateAst.

namespace LoxLiaison
{
    public abstract class Expr
    {
        public abstract T Accept<T>(IVisitor<T> visitor);

        public interface IVisitor<T>
        {
            public T VisitAssignExpr(Assign expr);
            public T VisitBinaryExpr(Binary expr);
            public T VisitGroupingExpr(Grouping expr);
            public T VisitLiteralExpr(Literal expr);
            public T VisitLogicalExpr(Logical expr);
            public T VisitUnaryExpr(Unary expr);
            public T VisitVariableExpr(Variable expr);
        }

        public class Assign : Expr
        {
            public readonly Token Name;
            public readonly Expr Value;
            
            public Assign(Token name, Expr value)
            {
                this.Name = name;
                this.Value = value;
            }

            public override T Accept<T>(IVisitor<T> visitor)
            {
                return visitor.VisitAssignExpr(this);
            }
        }

        public class Binary : Expr
        {
            public readonly Expr Left;
            public readonly Token Operator;
            public readonly Expr Right;
            
            public Binary(Expr left, Token @operator, Expr right)
            {
                this.Left = left;
                this.Operator = @operator;
                this.Right = right;
            }

            public override T Accept<T>(IVisitor<T> visitor)
            {
                return visitor.VisitBinaryExpr(this);
            }
        }

        public class Grouping : Expr
        {
            public readonly Expr Expression;
            
            public Grouping(Expr expression)
            {
                this.Expression = expression;
            }

            public override T Accept<T>(IVisitor<T> visitor)
            {
                return visitor.VisitGroupingExpr(this);
            }
        }

        public class Literal : Expr
        {
            public readonly object Value;
            
            public Literal(object value)
            {
                this.Value = value;
            }

            public override T Accept<T>(IVisitor<T> visitor)
            {
                return visitor.VisitLiteralExpr(this);
            }
        }

        public class Logical : Expr
        {
            public readonly Expr Left;
            public readonly Token Operator;
            public readonly Expr Right;
            
            public Logical(Expr left, Token @operator, Expr right)
            {
                this.Left = left;
                this.Operator = @operator;
                this.Right = right;
            }

            public override T Accept<T>(IVisitor<T> visitor)
            {
                return visitor.VisitLogicalExpr(this);
            }
        }

        public class Unary : Expr
        {
            public readonly Token Operator;
            public readonly Expr Right;
            
            public Unary(Token @operator, Expr right)
            {
                this.Operator = @operator;
                this.Right = right;
            }

            public override T Accept<T>(IVisitor<T> visitor)
            {
                return visitor.VisitUnaryExpr(this);
            }
        }

        public class Variable : Expr
        {
            public readonly Token Name;
            
            public Variable(Token name)
            {
                this.Name = name;
            }

            public override T Accept<T>(IVisitor<T> visitor)
            {
                return visitor.VisitVariableExpr(this);
            }
        }
    }
}
