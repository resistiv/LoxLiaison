using System.Text;

namespace LoxLiaison.Utils
{
    /// <summary>
    /// Debugging printer to ensure proper AST behaviour.
    /// </summary>
    /*public class AstPrinter : Expr.IVisitor<string>
    {
        public static void Main(string[] args)
        {
            Expr expression = new Expr.Binary(
                new Expr.Unary(
                    new Token(TokenType.Minus, "-", null, 1),
                    new Expr.Literal(123)),
                new Token(TokenType.Star, "*", null, 1),
                new Expr.Grouping(
                    new Expr.Literal(45.67))
                );

            Console.WriteLine(new AstPrinter().Print(expression));
        }

        public string Print(Expr expr)
        {
            return expr.Accept(this);
        }

        string Expr.IVisitor<string>.VisitBinaryExpr(Expr.Binary expr)
        {
            return Parenthesize(expr.Operator.Lexeme, expr.Left, expr.Right);
        }

        string Expr.IVisitor<string>.VisitGroupingExpr(Expr.Grouping expr)
        {
            return Parenthesize("group", expr.Expression);
        }

        string Expr.IVisitor<string>.VisitLiteralExpr(Expr.Literal expr)
        {
            return expr.Value.ToString() ?? "nil";
        }

        string Expr.IVisitor<string>.VisitUnaryExpr(Expr.Unary expr)
        {
            return Parenthesize(expr.Operator.Lexeme, expr.Right);
        }

        private string Parenthesize(string name, params Expr[] exprs)
        {
            StringBuilder builder = new();

            builder.Append('(').Append(name);
            for (int i = 0; i < exprs.Length; i++)
            {
                builder.Append(' ');
                builder.Append(exprs[i].Accept(this));
            }
            builder.Append(')');

            return builder.ToString();
        }
    }*/
}
