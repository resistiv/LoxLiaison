// Generated using GenerateAst.

namespace LoxLiaison
{
    public abstract class Stmt
    {
        public abstract T Accept<T>(IVisitor<T> visitor);

        public interface IVisitor<T>
        {
            public T VisitBlockStmt(Block stmt);
            public T VisitExpressionStmt(Expression stmt);
            public T VisitIfStmt(If stmt);
            public T VisitPrintStmt(Print stmt);
            public T VisitVarStmt(Var stmt);
        }

        public class Block : Stmt
        {
            public readonly System.Collections.Generic.List<Stmt> Statements;
            
            public Block(System.Collections.Generic.List<Stmt> statements)
            {
                this.Statements = statements;
            }

            public override T Accept<T>(IVisitor<T> visitor)
            {
                return visitor.VisitBlockStmt(this);
            }
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

        public class If : Stmt
        {
            public readonly Expr Condition;
            public readonly Stmt ThenBranch;
            public readonly Stmt ElseBranch;
            
            public If(Expr condition, Stmt thenBranch, Stmt elseBranch)
            {
                this.Condition = condition;
                this.ThenBranch = thenBranch;
                this.ElseBranch = elseBranch;
            }

            public override T Accept<T>(IVisitor<T> visitor)
            {
                return visitor.VisitIfStmt(this);
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
