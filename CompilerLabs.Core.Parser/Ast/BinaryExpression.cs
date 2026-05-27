using CompilerLabs.Core.Lexer;
using CompilerLabs.Core.Parser.Ast;

public class BinaryExpression : Expression
{
    public Expression Left;
    public TokenType Operator;
    public Expression Right;

    public BinaryExpression(
        Expression left,
        CompilerLabs.Core.Lexer.TokenType op,
        Expression right)
    {
        Left = left;
        Operator = op;
        Right = right;
    }
}