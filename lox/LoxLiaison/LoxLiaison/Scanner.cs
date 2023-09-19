using System.Collections.Generic;

namespace LoxLiaison
{
    /// <summary>
    /// Provides utilities for parsing raw Lox code.
    /// </summary>
    public class Scanner
    {
        private readonly string _source;
        private readonly List<Token> _tokens = new List<Token>();
        private int _startIndex = 0;
        private int _currentIndex = 0;
        private int _line = 1;

        private static readonly Dictionary<string, TokenType> Keywords = new()
        {
            { "and", TokenType.And },
            { "class", TokenType.Class },
            { "else", TokenType.Else },
            { "false", TokenType.False },
            { "for", TokenType.For },
            { "fun", TokenType.Fun },
            { "if", TokenType.If },
            { "nil", TokenType.Nil },
            { "or", TokenType.Or },
            { "print", TokenType.Print },
            { "return", TokenType.Return },
            { "super", TokenType.Super },
            { "this", TokenType.This },
            { "true", TokenType.True },
            { "var", TokenType.Var },
            { "while", TokenType.While }
        };

        /// <summary>
        /// Constructs a <see cref="Scanner"/>.
        /// </summary>
        /// <param name="source">A piece of raw Lox code to scan and parse.</param>
        public Scanner(string source)
        {
            _source = source;
        }

        /// <summary>
        /// Scans all <see cref="Token"/>s from the <see cref="Scanner"/>'s source <see cref="string"/>.
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of <see cref="Token"/>s parsed from the source <see cref="string"/>.</returns>
        public List<Token> ScanTokens()
        {
            while (!AtEnd())
            {
                _startIndex = _currentIndex;
                ScanToken();
            }

            _tokens.Add(new Token(TokenType.Eof, "", null, _line));
            return _tokens;
        }

        /// <summary>
        /// Returns whether or not the current index is at the end of the source <see cref="string"/>.
        /// </summary>
        /// <returns>Whether or not the current index is at the end of the source <see cref="string"/>.</returns>
        private bool AtEnd()
        {
            return _currentIndex >= _source.Length;
        }
    
        /// <summary>
        /// Scans an individual <see cref="Token"/> and adds it to the <see cref="Token"/> <see cref="List{T}"/>.
        /// </summary>
        private void ScanToken()
        {
            char c = NextCharacter();
            switch (c)
            {
                case '(':
                    AddToken(TokenType.LeftParentheses);
                    break;
                case ')':
                    AddToken(TokenType.RightParentheses);
                    break;
                case '{':
                    AddToken(TokenType.LeftBrace);
                    break;
                case '}':
                    AddToken(TokenType.RightBrace);
                    break;
                case ',':
                    AddToken(TokenType.Comma);
                    break;
                case '.':
                    AddToken(TokenType.Dot);
                    break;
                case '-':
                    AddToken(TokenType.Minus);
                    break;
                case '+':
                    AddToken(TokenType.Plus);
                    break;
                case ';':
                    AddToken(TokenType.Semicolon);
                    break;
                case '*':
                    AddToken(TokenType.Star);
                    break;
                case '!':
                    AddToken(Match('=') ? TokenType.NotEqual : TokenType.Not);
                    break;
                case '=':
                    AddToken(Match('=') ? TokenType.EqualEqual : TokenType.Equal);
                    break;
                case '<':
                    AddToken(Match('=') ? TokenType.LessThanEqual : TokenType.LessThan);
                    break;
                case '>':
                    AddToken(Match('=') ? TokenType.GreaterThanEqual : TokenType.GreaterThan);
                    break;
                case '/':
                    if (Match('/'))
                    {
                        while (Peek() != '\n' && !AtEnd())
                        {
                            NextCharacter();
                        }
                    }
                    else
                    {
                        AddToken(TokenType.Slash);
                    }
                    break;

                // Whitespace
                case ' ':
                case '\r':
                case '\t':
                    break;

                // Line break
                case '\n':
                    _line++;
                    break;

                // String
                case '"':
                    ReadString();
                    break;

                default:
                    // Number
                    if (IsDigit(c))
                    {
                        ReadNumber();
                    }
                    else if (IsAlpha(c))
                    {
                        Identifier();
                    }
                    else
                    {
                        Liaison.Error(_line, $"Unexpected character '{c}'.");
                    }
                    break;
            }
        }

        /// <summary>
        /// Returns the next character in the source <see cref="string"/> and increments the current index.
        /// </summary>
        /// <returns>The next character in the source <see cref="string"/>.</returns>
        private char NextCharacter()
        {
            return _source[_currentIndex++];
        }
    
        /// <summary>
        /// Adds a <see cref="Token"/> to the <see cref="Token"/> <see cref="List{T}"/> given only a <see cref="TokenType"/>.
        /// </summary>
        /// <param name="type">A <see cref="TokenType"/> from which to generate and add a <see cref="Token"/>.</param>
        private void AddToken(TokenType type)
        {
            AddToken(type, null);
        }

        /// <summary>
        /// Adds a <see cref="Token"/> to the <see cref="Token"/> <see cref="List{T}"/>.
        /// </summary>
        /// <param name="type">A <see cref="TokenType"/> from which to generate and add a <see cref="Token"/>.</param>
        /// <param name="literal">The literal <see cref="object"/> of the <see cref="Token"/>.</param>
        private void AddToken(TokenType type, object literal)
        {
            string lexeme = _source[_startIndex.._currentIndex];
            _tokens.Add(new Token(type, lexeme, literal, _line));
        }
    
        /// <summary>
        /// Checks if a character is an ASCII digit.
        /// </summary>
        /// <param name="c">A character to check.</param>
        /// <returns>Whether or not the character is an ASCII digit.</returns>
        private static bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        /// <summary>
        /// Checks if a character is an ASCII alphabetical character.
        /// </summary>
        /// <param name="c">A character to check.</param>
        /// <returns>Whether or not the character is an ASCII alphabetical character.</returns>
        private static bool IsAlpha(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || c == '_';
        }

        /// <summary>
        /// Checks if a character is an ASCII alpha-numeric character.
        /// </summary>
        /// <param name="c">A character to check.</param>
        /// <returns>Whether or not the character is an ASCII alpha-numeric character.</returns>
        private static bool IsAlphaNumeric(char c)
        {
            return IsAlpha(c) || IsDigit(c);
        }

        /// <summary>
        /// Looks ahead in the source <see cref="string"/> and sees if the next <see cref="char"/> matches what is expected.
        /// </summary>
        /// <param name="expected">An expected next <see cref="char"/>.</param>
        /// <returns>Whether or not the next <see cref="char"/> in the source <see cref="string"/> matches the expected <see cref="char"/>.</returns>
        private bool Match(char expected)
        {
            if (AtEnd())
            {
                return false;
            }
            if (_source[_currentIndex] != expected)
            {
                return false;
            }

            _currentIndex++;
            return true;
        }

        /// <summary>
        /// Peeks ahead at the next <see cref="char"/> in the source <see cref="string"/>, without incrementing the current index.
        /// </summary>
        /// <returns>The next <see cref="char"/> in the source <see cref="string"/>.</returns>
        private char Peek()
        {
            if (AtEnd())
            {
                return '\0';
            }
            return _source[_currentIndex];
        }

        /// <summary>
        /// Peeks ahead at the second <see cref="char"/> from the current one.
        /// </summary>
        /// <returns>The second <see cref="char"/> from the current one.</returns>
        private char PeekNext()
        {
            if (_currentIndex + 1 >= _source.Length)
            {
                return '\0';
            }
            return _source[_currentIndex + 1];
        }

        /// <summary>
        /// Reads a number from the source <see cref="string"/>.
        /// </summary>
        private void ReadNumber()
        {
            // Read digits
            while (IsDigit(Peek()))
            {
                NextCharacter();
            }

            // If there is a decimal point, read everything to right of it
            if (Peek() == '.' && IsDigit(PeekNext()))
            {
                NextCharacter();
                while (IsDigit(Peek()))
                {
                    NextCharacter();
                }
            }

            // Parse out
            double value = double.Parse(_source[_startIndex.._currentIndex]);
            AddToken(TokenType.Number, value);
        }

        /// <summary>
        /// Reads a <see cref="string"/> from the source <see cref="string"/>.
        /// </summary>
        private void ReadString()
        {
            // Read either until we reach closing quote or EOF
            while (Peek() != '"' && !AtEnd())
            {
                if (Peek() == '\n')
                {
                    _line++;
                }
                NextCharacter();
            }

            // We ran out of string to read!
            if (AtEnd())
            {
                Liaison.Error(_line, "Unterminated string.");
                return;
            }

            // Consume closing double quote
            NextCharacter();

            // Parse out string
            string value = _source[(_startIndex + 1)..(_currentIndex - 1)];
            AddToken(TokenType.String, value);
        }
    
        /// <summary>
        /// Parses keywords.
        /// </summary>
        private void Identifier()
        {
            while (IsAlphaNumeric(Peek()))
            {
                NextCharacter();
            }

            string text = _source[_startIndex.._currentIndex];
            TokenType type;
            try
            {
                type = Keywords[text];
            }
            catch (KeyNotFoundException)
            {
                type = TokenType.Identifier;
            }

            AddToken(type);
        }
    }
}
