using CompilerLabs.Core.Parser.Ast;

public class IdentifierExpression : Expression
{
    public string Name;

    public IdentifierExpression(string name)
    {
        Name = name;
    }
}