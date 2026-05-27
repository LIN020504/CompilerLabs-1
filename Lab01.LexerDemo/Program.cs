using CompilerLabs.Core.Lexer;
using System;

namespace Lab01.LexerDemo
{
    class Program
    {
        static void Main()
        {
            string code = @"
                fun add(a, b) {
                    return [a + b, ""hello"", true];
                }

                x = add(10, 20);
                y = x[0];
            ";

            var lexer = new Lexer(code);

            Console.WriteLine("TOKENS:");
            Console.WriteLine("--------------------------------");

            foreach (var token in lexer.Tokenize())
            {
                Console.WriteLine(
                    $"{token.Type,-15} {token.Value}");
            }

            Console.WriteLine("--------------------------------");
        }
    }
}