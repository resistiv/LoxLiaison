namespace LoxLiaison
{
    internal enum TokenType
    {
        // Single character tokens
        LeftParentheses, RightParentheses, LeftBrace, RightBrace,
        Comma, Dot, Minus, Plus, Semicolon, Slash, Star,

        // One/two character tokens
        Not, NotEqual,
        Equal, EqualEqual,
        GreaterThan, GreaterThanEqual,
        LessThan, LessThanEqual,

        // Literals
        Identifier, String, Number,

        // Keywords
        And, Class, Else, False, Fun, For, If, Nil, Or,
        Print, Return, Super, This, True, Var, While,
        Eof
    }
}
