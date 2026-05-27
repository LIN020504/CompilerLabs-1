using CompilerLabs.Core.Parser.Ast;

public class FunctionDeclaration : Statement
{
    public string Name;
    public List<string> Parameters;
    public List<Statement> Body;

    public FunctionDeclaration(
        string name,
        List<string> parameters,
        List<Statement> body)
    {
        Name = name;
        Parameters = parameters;
        Body = body;
    }
}