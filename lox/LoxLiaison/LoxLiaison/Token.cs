namespace LoxLiaison
{
    /// <summary>
    /// Represents a token parsed from raw Lox.
    /// </summary>
    public class Token
    {
        public readonly TokenType Type;
        public readonly string Lexeme;
        public readonly object Literal;
        public readonly int Line;

        /// <summary>
        /// Constructs a <see cref="Token"/>.
        /// </summary>
        /// <param name="type">The <see cref="TokenType"/> that identifies this <see cref="Token"/>.</param>
        /// <param name="lexeme">The base lexeme of this <see cref="Token"/><./param>
        /// <param name="literal">A parsed <see cref="object"/> of this <see cref="Token"/>'s lexeme.</param>
        /// <param name="line">The line number this <see cref="Token"/> appears on.</param>
        public Token(TokenType type, string lexeme, object literal, int line)
        {
            Type = type;
            Lexeme = lexeme;
            Literal = literal;
            Line = line;
        }

        /// <summary>
        /// Returns a string that represents the current <see cref="Token"/>.
        /// </summary>
        /// <returns>A string that represents the current <see cref="Token"/>.</returns>
        public override string ToString()
        {
            return $"{Type} {Lexeme} {Literal}";
        }
    }
}
