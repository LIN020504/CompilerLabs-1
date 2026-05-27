using System;
using System.Collections.Generic;
using System.Text;

namespace CompilerLabs.Core.Lexer
{
    public class Lexer
    {
        private readonly string _input;
        private int _position = 0;

        public Lexer(string input)
        {
            _input = input ?? "";
        }

        // ==========================================
        // TOKENIZE
        // ==========================================

        public IEnumerable<Token> Tokenize()
        {
            while (_position < _input.Length)
            {
                char current = Peek();

                // whitespace

                if (char.IsWhiteSpace(current))
                {
                    Next();
                    continue;
                }

                // number

                if (char.IsDigit(current))
                {
                    yield return ReadNumber();
                    continue;
                }

                // identifier / keyword

                if (char.IsLetter(current) || current == '_')
                {
                    yield return ReadIdentifier();
                    continue;
                }

                // string

                if (current == '"')
                {
                    yield return ReadString();
                    continue;
                }

                // symbols

                switch (current)
                {
                    case '+':
                        Next();
                        yield return new Token(TokenType.PLUS, "+");
                        break;

                    case '-':
                        Next();
                        yield return new Token(TokenType.MINUS, "-");
                        break;

                    case '*':
                        Next();
                        yield return new Token(TokenType.STAR, "*");
                        break;

                    case '/':
                        Next();
                        yield return new Token(TokenType.SLASH, "/");
                        break;

                    case '=':
                        Next();
                        yield return new Token(TokenType.EQ, "=");
                        break;

                    case ';':
                        Next();
                        yield return new Token(TokenType.SEMICOLON, ";");
                        break;

                    case ',':
                        Next();
                        yield return new Token(TokenType.COMMA, ",");
                        break;

                    case '(':
                        Next();
                        yield return new Token(TokenType.LPAREN, "(");
                        break;

                    case ')':
                        Next();
                        yield return new Token(TokenType.RPAREN, ")");
                        break;

                    case '{':
                        Next();
                        yield return new Token(TokenType.LBRACE, "{");
                        break;

                    case '}':
                        Next();
                        yield return new Token(TokenType.RBRACE, "}");
                        break;

                    // ARRAY SUPPORT

                    case '[':
                        Next();
                        yield return new Token(TokenType.LBRACKET, "[");
                        break;

                    case ']':
                        Next();
                        yield return new Token(TokenType.RBRACKET, "]");
                        break;

                    default:
                        throw new Exception(
                            $"Unexpected character: {current}");
                }
            }

            yield return new Token(TokenType.EOF, "");
        }

        // ==========================================
        // NUMBER
        // ==========================================

        private Token ReadNumber()
        {
            int start = _position;

            while (char.IsDigit(Peek()))
                Next();

            // decimal support

            if (Peek() == '.')
            {
                Next();

                while (char.IsDigit(Peek()))
                    Next();
            }

            string text =
                _input.Substring(start,
                _position - start);

            return new Token(
                TokenType.NUMBER,
                text);
        }

        // ==========================================
        // STRING
        // ==========================================

        private Token ReadString()
        {
            // skip opening quote

            Next();

            var sb = new StringBuilder();

            while (Peek() != '"' && Peek() != '\0')
            {
                sb.Append(Peek());
                Next();
            }

            if (Peek() != '"')
            {
                throw new Exception(
                    "Unterminated string");
            }

            // skip closing quote

            Next();

            return new Token(
                TokenType.STRING,
                sb.ToString());
        }

        // ==========================================
        // IDENTIFIER / KEYWORD
        // ==========================================

        private Token ReadIdentifier()
        {
            int start = _position;

            while (char.IsLetterOrDigit(Peek()) || Peek() == '_')
                Next();

            string text =
                _input.Substring(start,
                _position - start);

            return text switch
            {
                "fun" =>
                    new Token(TokenType.FUN, text),

                "return" =>
                    new Token(TokenType.RETURN, text),

                "true" =>
                    new Token(TokenType.TRUE, text),

                "false" =>
                    new Token(TokenType.FALSE, text),

                _ =>
                    new Token(TokenType.ID, text)
            };
        }

        // ==========================================
        // HELPERS
        // ==========================================

        private char Peek()
        {
            if (_position >= _input.Length)
                return '\0';

            return _input[_position];
        }

        private void Next()
        {
            _position++;
        }
    }
}