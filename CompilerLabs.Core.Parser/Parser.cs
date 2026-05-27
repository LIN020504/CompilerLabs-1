using CompilerLabs.Core.Lexer;
using CompilerLabs.Core.Parser.Ast;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompilerLabs.Core.Parser
{
    public class Parser
    {
        private readonly List<Token> _tokens;
        private int _position = 0;

        public List<string> Errors { get; } = new();

        public Parser(IEnumerable<Token> tokens)
        {
            _tokens = tokens.ToList();
        }

        // ==========================================
        // PROGRAM
        // ==========================================

        public List<Statement> ParseProgram()
        {
            var statements = new List<Statement>();

            while (!IsAtEnd())
            {
                statements.Add(ParseStatement());
            }

            return statements;
        }

        // ==========================================
        // STATEMENTS
        // ==========================================

        private Statement ParseStatement()
        {
            if (Match(TokenType.FUN))
                return ParseFunctionDeclaration();

            if (Match(TokenType.RETURN))
                return ParseReturnStatement();

            throw Error(Peek(), "Unexpected statement");
        }

        private FunctionDeclaration ParseFunctionDeclaration()
        {
            var name =
                Consume(TokenType.ID,
                "Expected function name").Value;

            Consume(TokenType.LPAREN,
                "Expected '(' after function name");

            var parameters = new List<string>();

            if (!Check(TokenType.RPAREN))
            {
                do
                {
                    parameters.Add(
                        Consume(
                            TokenType.ID,
                            "Expected parameter name").Value);
                }
                while (Match(TokenType.COMMA));
            }

            Consume(TokenType.RPAREN,
                "Expected ')' after parameters");

            var body = ParseBlock();

            return new FunctionDeclaration(
                name,
                parameters,
                body);
        }

        private ReturnStatement ParseReturnStatement()
        {
            var value = ParseExpression();

            Consume(
                TokenType.SEMICOLON,
                "Expected ';' after return value");

            return new ReturnStatement(value);
        }

        private List<Statement> ParseBlock()
        {
            Consume(TokenType.LBRACE,
                "Expected '{'");

            var statements = new List<Statement>();

            while (!Check(TokenType.RBRACE))
            {
                statements.Add(ParseStatement());
            }

            Consume(TokenType.RBRACE,
                "Expected '}'");

            return statements;
        }

        // ==========================================
        // EXPRESSIONS
        // ==========================================

        private Expression ParseExpression()
            => ParseAddition();

        private Expression ParseAddition()
        {
            var expr = ParseMultiplication();

            while (Match(TokenType.PLUS, TokenType.MINUS))
            {
                var op = Previous().Type;

                var right = ParseMultiplication();

                expr = new BinaryExpression(
                    expr,
                    op,
                    right);
            }

            return expr;
        }

        private Expression ParseMultiplication()
        {
            var expr = ParsePrimary();

            while (Match(TokenType.STAR, TokenType.SLASH))
            {
                var op = Previous().Type;

                var right = ParsePrimary();

                expr = new BinaryExpression(
                    expr,
                    op,
                    right);
            }

            return expr;
        }

        // ==========================================
        // PRIMARY
        // ==========================================

        private Expression ParsePrimary()
        {
            // NUMBER

            if (Match(TokenType.NUMBER))
            {
                return new NumberExpression(
                    double.Parse(Previous().Value));
            }

            // STRING

            if (Match(TokenType.STRING))
            {
                return new StringExpression(
                    Previous().Value);
            }

            // ARRAY

            if (Match(TokenType.LBRACKET))
            {
                var elements = new List<Expression>();

                if (!Check(TokenType.RBRACKET))
                {
                    do
                    {
                        elements.Add(ParseExpression());
                    }
                    while (Match(TokenType.COMMA));
                }

                Consume(
                    TokenType.RBRACKET,
                    "Expected ']'");

                return new ArrayExpression(elements);
            }

            // IDENTIFIER

            if (Match(TokenType.ID))
            {
                Expression expr =
                    new VariableExpression(
                        Previous().Value);

                while (true)
                {
                    // FUNCTION CALL

                    if (Match(TokenType.LPAREN))
                    {
                        var args =
                            new List<Expression>();

                        if (!Check(TokenType.RPAREN))
                        {
                            do
                            {
                                args.Add(ParseExpression());
                            }
                            while (Match(TokenType.COMMA));
                        }

                        Consume(
                            TokenType.RPAREN,
                            "Expected ')'");

                        expr = new CallExpression(
                            ((VariableExpression)expr).Name,
                            args);

                        continue;
                    }

                    // ARRAY INDEX

                    if (Match(TokenType.LBRACKET))
                    {
                        var index = ParseExpression();

                        Consume(
                            TokenType.RBRACKET,
                            "Expected ']'");

                        expr = new IndexExpression(
                            expr,
                            index);

                        continue;
                    }

                    break;
                }

                return expr;
            }

            // GROUPING

            if (Match(TokenType.LPAREN))
            {
                var expr = ParseExpression();

                Consume(
                    TokenType.RPAREN,
                    "Expected ')'");

                return expr;
            }

            throw Error(Peek(), "Expected expression");
        }

        // ==========================================
        // HELPERS
        // ==========================================

        private bool Match(params TokenType[] types)
        {
            foreach (var type in types)
            {
                if (Check(type))
                {
                    Advance();
                    return true;
                }
            }

            return false;
        }

        private Token Consume(
            TokenType type,
            string message)
        {
            if (Check(type))
                return Advance();

            throw Error(Peek(), message);
        }

        private bool Check(TokenType type)
        {
            if (IsAtEnd())
                return false;

            return Peek().Type == type;
        }

        private Token Advance()
        {
            if (!IsAtEnd())
                _position++;

            return Previous();
        }

        private bool IsAtEnd()
            => Peek().Type == TokenType.EOF;

        private Token Peek()
            => _tokens[_position];

        private Token Previous()
            => _tokens[_position - 1];

        private Exception Error(
            Token token,
            string message)
        {
            var error =
                $"Parser error at '{token.Value}': {message}";

            Errors.Add(error);

            return new Exception(error);
        }
    }
}