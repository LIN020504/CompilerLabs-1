using CompilerLabs.Core.Parser.Ast;

public class NumberExpression : Expression
{
    public double Value { get; }

    public NumberExpression(double value)
    {
        Value = value;
    }
}