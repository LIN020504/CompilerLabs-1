namespace CompilerLabs.Core.Parser.Ast
{
    /// <summary>
    /// 数组索引访问
    /// 例如: arr[0]
    /// </summary>
    public class IndexExpression : Expression
    {
        public Expression Target { get; }
        public Expression Index { get; }

        public IndexExpression(
            Expression target,
            Expression index)
        {
            Target = target;
            Index = index;
        }
    }
}