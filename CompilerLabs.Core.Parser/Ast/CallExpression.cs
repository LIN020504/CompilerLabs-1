using CompilerLabs.Core.Parser.Ast;

public class CallExpression : Expression
{
    public string FunctionName;
    public List<Expression> Arguments;

    public CallExpression(
        string functionName,
        List<Expression> arguments)
    {
        FunctionName = functionName;
        Arguments = arguments;
    }
}