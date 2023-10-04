using System;
using System.Collections.Generic;

namespace LoxLiaison
{
    /// <summary>
    /// Handles parsing of <see cref="Token"/>s.
    /// </summary>
    public class Parser
    {
        private class ParsingException : Exception { }

        private readonly List<Token> _tokens;
        private int _current = 0;

        /// <summary>
        /// Constructs a <see cref="Parser"/>.
        /// </summary>
        /// <param name="tokens">A <see cref="List{T}"/> of <see cref="Token"/>s to parse.</param>
        public Parser(List<Token> tokens)
        {
            _tokens = tokens;
        }

        /// <summary>
        /// Starts parsing this <see cref="Parser"/>'s <see cref="Token"/>s.
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of statements parsed from the <see cref="Token"/>s.</returns>
        public List<Stmt> Parse()
        {
            List<Stmt> statements = new();

            while (!AtEnd())
            {
                statements.Add(Declaration());
            }

            return statements;
        }

        /// <summary>
        /// Resolves an expression.
        /// </summary>
        /// <returns>An <see cref="Expr"/> representing the expression.</returns>
        private Expr Expression()
        {
            return Assignment();
        }

        /// <summary>
        /// Resolves a declaration.
        /// </summary>
        /// <returns>A <see cref="Stmt"/> representing the declaration.</returns>
        private Stmt Declaration()
        {
            try
            {
                if (MatchToken(TokenType.Fun))
                {
                    return Function("function");
                }
                if (MatchToken(TokenType.Var))
                {
                    return VarDeclaration();
                }
                else
                {
                    return Statement();
                }
            }
            catch (ParsingException)
            {
                SynchronizeState();
                return null;
            }
        }
        
        /// <summary>
        /// Resolves a function declaration.
        /// </summary>
        /// <param name="kind">The kind of function declaration, such as function or method.</param>
        /// <returns>A <see cref="Stmt.Function"/> representing the function.</returns>
        private Stmt.Function Function(string kind)
        {
            Token name = ConsumeToken(TokenType.Identifier, $"Expect {kind} name.");
            ConsumeToken(TokenType.LeftParentheses, $"Expect '(' after {kind} name.");
            List<Token> parameters = new();
            if (!CheckToken(TokenType.RightParentheses))
            {
                do
                {
                    if (parameters.Count >= 255)
                    {
                        Error(Peek(), "Can't have more than 255 parameters.");
                    }
                    parameters.Add(ConsumeToken(TokenType.Identifier, "Expect parameter name."));
                }
                while (MatchToken(TokenType.Comma));
            }
            ConsumeToken(TokenType.RightParentheses, "Expect ')' after parameters.");

            ConsumeToken(TokenType.LeftBrace, $"Expect '{{' before {kind} body.");
            List<Stmt> body = Block();
            return new Stmt.Function(name, parameters, body);
        }

        /// <summary>
        /// Resolves a variable declaration.
        /// </summary>
        /// <returns>A <see cref="Stmt"/> representing the variable declaration.</returns>
        private Stmt VarDeclaration()
        {
            Token name = ConsumeToken(TokenType.Identifier, "Expect variable name.");

            Expr initializer = null;
            if (MatchToken(TokenType.Equal))
            {
                initializer = Expression();
            }

            ConsumeToken(TokenType.Semicolon, "Expect ';' after variable declaration.");
            return new Stmt.Var(name, initializer);
        }

        /// <summary>
        /// Resolves a statement.
        /// </summary>
        /// <returns>A <see cref="Stmt"/> representing the statement.</returns>
        private Stmt Statement()
        {
            if (MatchToken(TokenType.For))
            {
                return ForStatement();
            }
            if (MatchToken(TokenType.If))
            {
                return IfStatement();
            }
            if (MatchToken(TokenType.Print))
            {
                return PrintStatement();
            }
            if (MatchToken(TokenType.Return))
            {
                return ReturnStatement();
            }
            if (MatchToken(TokenType.While))
            {
                return WhileStatement();
            }
            if (MatchToken(TokenType.LeftBrace))
            {
                return new Stmt.Block(Block());
            }

            return ExpressionStatement();
        }

        /// <summary>
        /// Resolves a while statement.
        /// </summary>
        /// <returns>A <see cref="Stmt"/> representing the while statement.</returns>
        private Stmt WhileStatement()
        {
            ConsumeToken(TokenType.LeftParentheses, "Expect '(' after 'while'.");
            Expr condition = Expression();
            ConsumeToken(TokenType.RightParentheses, "Expect ')' after condition.");
            Stmt body = Statement();

            return new Stmt.While(condition, body);
        }

        /// <summary>
        /// Resolves a for statement.
        /// </summary>
        /// <returns>A <see cref="Stmt"/> representing the for statement.</returns>
        private Stmt ForStatement()
        {
            ConsumeToken(TokenType.LeftParentheses, "Expect '(' after 'for'.");

            Stmt initializer;
            if (MatchToken(TokenType.Semicolon))
            {
                initializer = null;
            }
            else if (MatchToken(TokenType.Var))
            {
                initializer = VarDeclaration();
            }
            else
            {
                initializer = ExpressionStatement();
            }

            Expr condition = null;
            if (!CheckToken(TokenType.Semicolon))
            {
                condition = Expression();
            }
            ConsumeToken(TokenType.Semicolon, "Expect ';' after loop condition.");

            Expr increment = null;
            if (!CheckToken(TokenType.RightParentheses))
            {
                increment = Expression();
            }
            ConsumeToken(TokenType.RightParentheses, "Expect ')' after for clauses.");

            Stmt body = Statement();

            if (increment != null)
            {
                List<Stmt> statements = new()
                {
                    body,
                    new Stmt.Expression(increment)
                };
                body = new Stmt.Block(statements);
            }

            condition ??= new Expr.Literal(true);
            body = new Stmt.While(condition, body);

            if (initializer != null)
            {
                List<Stmt> statements = new()
                {
                    initializer,
                    body
                };
                body = new Stmt.Block(statements);
            }

            return body;
        }

        /// <summary>
        /// Resolves an if statment.
        /// </summary>
        /// <returns>A <see cref="Stmt"/> representing the if statement.</returns>
        private Stmt IfStatement()
        {
            ConsumeToken(TokenType.LeftParentheses, "Expect '(' after 'if'.");
            Expr condition = Expression();
            ConsumeToken(TokenType.RightParentheses, "Expect ')' after if condition.");

            Stmt thenBranch = Statement();
            Stmt elseBranch = null;
            if (MatchToken(TokenType.Else))
            {
                elseBranch = Statement();
            }

            return new Stmt.If(condition, thenBranch, elseBranch);
        }

        /// <summary>
        /// Resolves a print statement.
        /// </summary>
        /// <returns>A <see cref="Stmt"/> representing the print statement.</returns>
        private Stmt PrintStatement()
        {
            Expr value = Expression();
            ConsumeToken(TokenType.Semicolon, "Expect ';' after value.");
            return new Stmt.Print(value);
        }

        private Stmt ReturnStatement()
        {
            Token keyword = PreviousToken();
            Expr value = null;
            if (!CheckToken(TokenType.Semicolon))
            {
                value = Expression();
            }

            ConsumeToken(TokenType.Semicolon, "Expect ';' after return value.");
            return new Stmt.Return(keyword, value);
        }

        /// <summary>
        /// Resolves an expression statement.
        /// </summary>
        /// <returns>A <see cref="Stmt"/> representing the expression statement.</returns>
        private Stmt ExpressionStatement()
        {
            Expr expr = Expression();
            ConsumeToken(TokenType.Semicolon, "Expect ';' after expression.");
            return new Stmt.Expression(expr);
        }

        /// <summary>
        /// Resolves a block of code.
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of <see cref="Stmt"/>s contained within the block.</returns>
        private List<Stmt> Block()
        {
            List<Stmt> statements = new();

            while (!CheckToken(TokenType.RightBrace) && !AtEnd())
            {
                statements.Add(Declaration());
            }

            ConsumeToken(TokenType.RightBrace, "Expect '}' after block.");
            return statements;
        }

        /// <summary>
        /// Resolves an assignment statement.
        /// </summary>
        /// <returns>An <see cref="Expr"/> representing the assignment statement.</returns>
        private Expr Assignment()
        {
            Expr expr = Or();

            if (MatchToken(TokenType.Equal))
            {
                Token equals = PreviousToken();
                Expr value = Assignment();

                if (expr is Expr.Variable v)
                {
                    Token name = v.Name;
                    return new Expr.Assign(name, value);
                }

                Error(equals, "Invalid assignment target.");
            }

            return expr;
        }

        /// <summary>
        /// Resolves an or expression.
        /// </summary>
        /// <returns>An <see cref="Expr"/> representing the or expression.</returns>
        private Expr Or()
        {
            Expr expr = And();

            while (MatchToken(TokenType.Or))
            {
                Token @operator = PreviousToken();
                Expr right = And();
                expr = new Expr.Logical(expr, @operator, right);
            }

            return expr;
        }

        /// <summary>
        /// Resolves an and expression.
        /// </summary>
        /// <returns>An <see cref="Expr"/> representing the and expression.</returns>
        private Expr And()
        {
            Expr expr = Equality();

            while (MatchToken(TokenType.And))
            {
                Token @operator = PreviousToken();
                Expr right = Equality();
                expr = new Expr.Logical(expr, @operator, right);
            }

            return expr;
        }

        /// <summary>
        /// Resolves an equality.
        /// </summary>
        /// <returns>An <see cref="Expr"/> representing the equality.</returns>
        private Expr Equality()
        {
            Expr expr = Comparison();

            while (MatchToken(TokenType.NotEqual, TokenType.EqualEqual))
            {
                Token @operator = PreviousToken();
                Expr right = Comparison();
                expr = new Expr.Binary(expr, @operator, right);
            }

            return expr;
        }

        /// <summary>
        /// Resolves a comparison.
        /// </summary>
        /// <returns>An <see cref="Expr"/> representing the comparison.</returns>
        private Expr Comparison()
        {
            Expr expr = Term();

            while (MatchToken(TokenType.GreaterThan, TokenType.GreaterThanEqual, TokenType.LessThan, TokenType.LessThanEqual))
            {
                Token @operator = PreviousToken();
                Expr right = Term();
                expr = new Expr.Binary(expr, @operator, right);
            }

            return expr;
        }

        /// <summary>
        /// Resolves a term.
        /// </summary>
        /// <returns>An <see cref="Expr"/> representing the term.</returns>
        private Expr Term()
        {
            Expr expr = Factor();

            while (MatchToken(TokenType.Minus, TokenType.Plus))
            {
                Token @operator = PreviousToken();
                Expr right = Factor();
                expr = new Expr.Binary(expr, @operator, right);
            }

            return expr;
        }

        /// <summary>
        /// Resolves a factor.
        /// </summary>
        /// <returns>An <see cref="Expr"/> representing the factor.</returns>
        private Expr Factor()
        {
            Expr expr = Unary();

            while (MatchToken(TokenType.Slash, TokenType.Star))
            {
                Token @operator = PreviousToken();
                Expr right = Unary();
                expr = new Expr.Binary(expr, @operator, right);
            }

            return expr;
        }

        /// <summary>
        /// Resolves a unary expression.
        /// </summary>
        /// <returns>An <see cref="Expr"/> representing the unary expression.</returns>
        private Expr Unary()
        {
            if (MatchToken(TokenType.Not, TokenType.Minus))
            {
                Token @operator = PreviousToken();
                Expr right = Unary();
                return new Expr.Unary(@operator, right);
            }

            return Call();
        }

        /// <summary>
        /// Resolves a call expression.
        /// </summary>
        /// <returns>An <see cref="Expr"/> representing the call expression.</returns>
        private Expr Call()
        {
            Expr expr = Primary();

            while (true)
            {
                if (MatchToken(TokenType.LeftParentheses))
                {
                    expr = FinishCall(expr);
                }
                else
                {
                    break;
                }
            }

            return expr;
        }

        private Expr FinishCall(Expr callee)
        {
            List<Expr> arguments = new();
            if (!CheckToken(TokenType.RightParentheses))
            {
                do
                {
                    if (arguments.Count >= 255)
                    {
                        Error(Peek(), "Can't have more than 255 arguments.");
                    }
                    arguments.Add(Expression());
                }
                while (MatchToken(TokenType.Comma));
            }

            Token paren = ConsumeToken(TokenType.RightParentheses, "Expect ')' after arguments.");

            return new Expr.Call(callee, paren, arguments);
        }

        /// <summary>
        /// Resolves a primary expression.
        /// </summary>
        /// <returns>An <see cref="Expr"/> representing the primary expression.</returns>
        private Expr Primary()
        {
            if (MatchToken(TokenType.False))
            {
                return new Expr.Literal(false);
            }
            if (MatchToken(TokenType.True))
            {
                return new Expr.Literal(true);
            }
            if (MatchToken(TokenType.Nil))
            {
                return new Expr.Literal(null);
            }

            if (MatchToken(TokenType.Number, TokenType.String))
            {
                return new Expr.Literal(PreviousToken().Literal);
            }

            if (MatchToken(TokenType.Identifier))
            {
                return new Expr.Variable(PreviousToken());
            }

            if (MatchToken(TokenType.LeftParentheses))
            {
                Expr expr = Expression();
                ConsumeToken(TokenType.RightParentheses, "Expected ')' after expression.");
                return new Expr.Grouping(expr);
            }

            throw Error(Peek(), "Expected expression.");
        }

        /// <summary>
        /// Checks if the next <see cref="Token"/> matches a list of <see cref="TokenType"/>s.
        /// </summary>
        /// <param name="types">A list of <see cref="TokenType"/>s.</param>
        /// <returns>Whether or not the next <see cref="Token"/> matches any given <see cref="TokenType"/> provided.</returns>
        private bool MatchToken(params TokenType[] types)
        {
            for (int i = 0; i < types.Length; i++)
            {
                if (CheckToken(types[i]))
                {
                    NextToken();
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Attempts to consume a <see cref="Token"/>.
        /// </summary>
        /// <param name="type">The expected <see cref="TokenType"/> of the <see cref="Token"/>.</param>
        /// <param name="message">A message detailing the expected <see cref="Token"/>.</param>
        /// <exception cref="ParsingException"></exception>
        /// <returns>A <see cref="Token"/> that was consumed.</returns>
        private Token ConsumeToken(TokenType type, string message)
        {
            if (CheckToken(type))
            {
                return NextToken();
            }

            throw Error(Peek(), message);
        }
        /// <summary>
        /// Returns true if the given <see cref="TokenType"/> matches that of the next <see cref="Token"/>.
        /// </summary>
        /// <param name="type">A <see cref="TokenType"/> to match.</param>
        /// <returns>Whether or not the given <see cref="TokenType"/> matches that of the next <see cref="Token"/>.</returns>
        private bool CheckToken(TokenType type)
        {
            if (AtEnd())
            {
                return false;
            }
            return Peek().Type == type;
        }

        /// <summary>
        /// Consumes the next <see cref="Token"/>.
        /// </summary>
        /// <returns>The next <see cref="Token"/>.</returns>
        private Token NextToken()
        {
            if (!AtEnd())
            {
                _current++;
            }
            return PreviousToken();
        }

        /// <summary>
        /// Returns whether or not the end of the <see cref="Token"/> list has been reached.
        /// </summary>
        /// <returns>Whether or not the end of the <see cref="Token"/> list has been reached.</returns>
        private bool AtEnd()
        {
            return Peek().Type == TokenType.Eof;
        }

        /// <summary>
        /// Peeks at the next <see cref="Token"/> without advancing.
        /// </summary>
        /// <returns>The next <see cref="Token"/>.</returns>
        private Token Peek()
        {
            return _tokens[_current];
        }

        /// <summary>
        /// Returns the previous <see cref="Token"/>.
        /// </summary>
        /// <returns>The previous <see cref="Token"/>.</returns>
        private Token PreviousToken()
        {
            return _tokens[_current - 1];
        }

        /// <summary>
        /// Reports and returns a <see cref="ParsingException"/>.
        /// </summary>
        /// <param name="token">A <see cref="Token"/> that caused the error.</param>
        /// <param name="message">A message detailing the error.</param>
        /// <returns>A <see cref="ParsingException"/>.</returns>
        private ParsingException Error(Token token, string message)
        {
            Liaison.Error(token, message);
            return new ParsingException();
        }
    
        /// <summary>
        /// Attempts to synchronize the state of the parser after encountering a <see cref="ParsingException"/>.
        /// </summary>
        private void SynchronizeState()
        {
            NextToken();

            while (!AtEnd())
            {
                if (PreviousToken().Type == TokenType.Semicolon)
                {
                    return;
                }

                switch (Peek().Type)
                {
                    case TokenType.Class:
                    case TokenType.Fun:
                    case TokenType.Var:
                    case TokenType.For:
                    case TokenType.If:
                    case TokenType.While:
                    case TokenType.Print:
                    case TokenType.Return:
                        return;
                }

                NextToken();
            }
        }
    }
}
