using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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

                default:
                    Liaison.Error(_line, $"Unexpected character '{c}'.");
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
    }
}
