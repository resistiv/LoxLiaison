namespace LoxLiaison
{
    internal class Token
    {
        internal readonly TokenType Type;
        internal readonly string Lexeme;
        internal readonly object Literal;
        internal readonly int Line;

        public Token(TokenType type, string lexeme, object literal, int line)
        {
            Type = type;
            Lexeme = lexeme;
            Literal = literal;
            Line = line;
        }

        public override string ToString()
        {
            return $"{Type} {Lexeme} {Literal}";
        }
    }
}
