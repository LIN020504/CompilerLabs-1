using CompilerLabs.Core.Parser.Ast;

namespace CompilerLabs.Core.Parser.Ast
{
    /// <summary>
    /// 数组字面量
    /// 例如: [1, 2, 3]
    /// </summary>
    public class ArrayExpression : Expression
    {
        public List<Expression> Elements { get; }

        public ArrayExpression(List<Expression> elements)
        {
            Elements = elements;
        }
    }
}