using CompilerLabs.Core.Lexer;
using System.Collections.Generic;

namespace CompilerLabs.Core.Parser.Ast
{
    // ==========================================
    // ВЫРАЖЕНИЯ (Expressions)
    // ==========================================

    /// <summary>
    /// Числовой литерал
    /// </summary>
    public class NumberExpression : Expression
    {
        public double Value { get; }

        public NumberExpression(double value)
        {
            Value = value;
        }
    }

    /// <summary>
    /// Строковый литерал
    /// </summary>
    public class StringExpression : Expression
    {
        public string Value { get; }

        public StringExpression(string value)
        {
            Value = value;
        }
    }


    /// <summary>
    /// Переменная
    /// </summary>
    public class VariableExpression : Expression
    {
        public string Name { get; }

        public VariableExpression(string name)
        {
            Name = name;
        }
    }

    /// <summary>
    /// Бинарная операция
    /// </summary>
    public class BinaryExpression : Expression
    {
        public Expression Left { get; }
        public TokenType Operator { get; }
        public Expression Right { get; }

        public BinaryExpression(
            Expression left,
            TokenType op,
            Expression right)
        {
            Left = left;
            Operator = op;
            Right = right;
        }
    }

    /// <summary>
    /// Унарная операция
    /// </summary>
    public class UnaryExpression : Expression
    {
        public TokenType Operator { get; }
        public Expression Right { get; }

        public UnaryExpression(
            TokenType op,
            Expression right)
        {
            Operator = op;
            Right = right;
        }
    }

    /// <summary>
    /// Присваивание
    /// </summary>
    public class AssignExpression : Expression
    {
        public string Name { get; }
        public Expression Value { get; }

        public AssignExpression(
            string name,
            Expression value)
        {
            Name = name;
            Value = value;
        }
    }

    /// <summary>
    /// Вызов функции
    /// </summary>
    public class CallExpression : Expression
    {
        public string Callee { get; }
        public List<Expression> Arguments { get; }

        public CallExpression(
            string callee,
            List<Expression> arguments)
        {
            Callee = callee;
            Arguments = arguments;
        }
    }
}