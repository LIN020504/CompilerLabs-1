using System.Collections.Generic;

namespace CompilerLabs.Core.Parser.Ast
{
    /// <summary>
    /// Инструкция-обертка над выражением.
    /// </summary>
    public class ExpressionStatement : Statement
    {
        public Expression Expression { get; }

        public ExpressionStatement(Expression expression)
        {
            Expression = expression;
        }
    }

    /// <summary>
    /// Вывод в консоль.
    /// </summary>
    public class PrintStatement : Statement
    {
        public Expression Expression { get; }

        public PrintStatement(Expression expression)
        {
            Expression = expression;
        }
    }

    /// <summary>
    /// Объявление переменной.
    /// </summary>
    public class VarStatement : Statement
    {
        public string Name { get; }
        public Expression Initializer { get; }

        public VarStatement(
            string name,
            Expression initializer)
        {
            Name = name;
            Initializer = initializer;
        }
    }

    /// <summary>
    /// Блок кода.
    /// </summary>
    public class BlockStatement : Statement
    {
        public List<Statement> Statements { get; }

        public BlockStatement(List<Statement> statements)
        {
            Statements = statements;
        }
    }

    /// <summary>
    /// Ветвление if.
    /// </summary>
    public class IfStatement : Statement
    {
        public Expression Condition { get; }
        public Statement ThenBranch { get; }
        public Statement ElseBranch { get; }

        public IfStatement(
            Expression condition,
            Statement thenBranch,
            Statement elseBranch)
        {
            Condition = condition;
            ThenBranch = thenBranch;
            ElseBranch = elseBranch;
        }
    }

    /// <summary>
    /// Цикл while.
    /// </summary>
    public class WhileStatement : Statement
    {
        public Expression Condition { get; }
        public Statement Body { get; }

        public WhileStatement(
            Expression condition,
            Statement body)
        {
            Condition = condition;
            Body = body;
        }
    }
}