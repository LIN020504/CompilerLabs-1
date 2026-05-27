namespace CompilerLabs.Core.Lexer;
public enum TokenType
{
    STRING,
    NUMBER,
    ID,

    PLUS, MINUS, STAR, SLASH,
    EQ,

    SEMICOLON,

    FUN,
    RETURN,
    COMMA,
    LPAREN, RPAREN,
    LBRACE, RBRACE,

    EOF,

    
    LBRACKET,   // [
    RBRACKET,   // ]
    TRUE,
    FALSE,
    
}