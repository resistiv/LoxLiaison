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
        /// <returns>An <see cref="Expr"/> representing the parsed <see cref="Token"/>s.</returns>
        public Expr Parse()
        {
            try
            {
                return Expression();
            }
            catch (ParsingException)
            {
                return null;
            }
        }

        /// <summary>
        /// Resolves an expression.
        /// </summary>
        /// <returns>An <see cref="Expr"/> representing the expression.</returns>
        private Expr Expression()
        {
            return Equality();
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

            return Primary();
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
