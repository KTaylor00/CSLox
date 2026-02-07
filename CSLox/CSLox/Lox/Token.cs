using CSLox.Enums;

namespace CSLox.Lox;

public class Token(TokenType type, string lexeme, object? literal, int line)
{
    private readonly TokenType type = type;
    private readonly string lexeme = lexeme;
    private readonly object? literal = literal;
    private readonly int line = line;

    public override string ToString() => type + " " + lexeme + " " + literal;
}
