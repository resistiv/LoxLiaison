// Generated using LoxAstGenerator

using System.Collections.Generic;
using LoxLiaison.Data;

namespace LoxLiaison
{
	public abstract class Stmt
	{
		public abstract T Accept<T>(IVisitor<T> visitor);

		public interface IVisitor<T>
		{
			public T VisitBlockStmt(Block stmt);
			public T VisitClassStmt(Class stmt);
			public T VisitExpressionStmt(Expression stmt);
			public T VisitFunctionStmt(Function stmt);
			public T VisitIfStmt(If stmt);
			public T VisitPrintStmt(Print stmt);
			public T VisitReturnStmt(Return stmt);
			public T VisitVarStmt(Var stmt);
			public T VisitWhileStmt(While stmt);
		}

		public class Block : Stmt
		{
			public readonly List<Stmt> Statements;
			
			public Block(List<Stmt> statements)
			{
				this.Statements = statements;
			}

			public override T Accept<T>(IVisitor<T> visitor)
			{
				return visitor.VisitBlockStmt(this);
			}
		}

		public class Class : Stmt
		{
			public readonly Token Name;
			public readonly Expr.Variable Superclass;
			public readonly List<Stmt.Function> Methods;
			
			public Class(Token name, Expr.Variable superclass, List<Stmt.Function> methods)
			{
				this.Name = name;
				this.Superclass = superclass;
				this.Methods = methods;
			}

			public override T Accept<T>(IVisitor<T> visitor)
			{
				return visitor.VisitClassStmt(this);
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

		public class Function : Stmt
		{
			public readonly Token Name;
			public readonly List<Token> Params;
			public readonly List<Stmt> Body;
			
			public Function(Token name, List<Token> @params, List<Stmt> body)
			{
				this.Name = name;
				this.Params = @params;
				this.Body = body;
			}

			public override T Accept<T>(IVisitor<T> visitor)
			{
				return visitor.VisitFunctionStmt(this);
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

		public class Return : Stmt
		{
			public readonly Token Keyword;
			public readonly Expr Value;
			
			public Return(Token keyword, Expr value)
			{
				this.Keyword = keyword;
				this.Value = value;
			}

			public override T Accept<T>(IVisitor<T> visitor)
			{
				return visitor.VisitReturnStmt(this);
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

		public class While : Stmt
		{
			public readonly Expr Condition;
			public readonly Stmt Body;
			
			public While(Expr condition, Stmt body)
			{
				this.Condition = condition;
				this.Body = body;
			}

			public override T Accept<T>(IVisitor<T> visitor)
			{
				return visitor.VisitWhileStmt(this);
			}
		}
	}
}
