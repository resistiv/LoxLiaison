// Generated using GenerateAst.

namespace LoxLiaison
{
    public abstract class Stmt
    {
        public abstract T Accept<T>(IVisitor<T> visitor);

        public interface IVisitor<T>
        {
            public T VisitExpressionStmt(Expression stmt);
            public T VisitPrintStmt(Print stmt);
            public T VisitVarStmt(Var stmt);
        }

        public class Expression : Stmt
        {
            public readonly Expr Expr;
            
            public Expression(Expr expr)
            {
                this.Expr = expr;
            }

            public override T Accept<T>(IVisitor<T> visitor)
            {
                return visitor.VisitExpressionStmt(this);
            }
        }

        public class Print : Stmt
        {
            public readonly Expr Expr;
            
            public Print(Expr expr)
            {
                this.Expr = expr;
            }

            public override T Accept<T>(IVisitor<T> visitor)
            {
                return visitor.VisitPrintStmt(this);
            }
        }

        public class Var : Stmt
        {
            public readonly Token Name;
            public readonly Expr Initializer;
            
            public Var(Token name, Expr initializer)
            {
                this.Name = name;
                this.Initializer = initializer;
            }

            public override T Accept<T>(IVisitor<T> visitor)
            {
                return visitor.VisitVarStmt(this);
            }
        }
    }
}
