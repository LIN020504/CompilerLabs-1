using CompilerLabs.Core;
using CompilerLabs.Core.Lexer;
using System.Text;

namespace LexerDemo
{
    class Program
    {
        static void Main()
        {
            var lexer = new Lexer("x = 10 + 20;");
            
            foreach (var token in lexer.Tokenize())
            {
                Console.WriteLine(token);
            }
        }
    }
}