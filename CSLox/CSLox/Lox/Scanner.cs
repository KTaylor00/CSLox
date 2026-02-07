using CSLox.Enums;
using static CSLox.Enums.TokenType;

namespace CSLox.Lox;

public class Scanner(string source)
{
    private readonly string source = source;
    private readonly List<Token> tokens = [];
    private int start = 0;
    private int current = 0;
    private int line = 1;

    public List<Token> ScanTokens()
    {
        while (!IsAtEnd())
        {
            // beginning of the next lexeme.
            start = current;
            ScanToken();
        }

        tokens.Add(new Token(EOF, "", null, line));
        return tokens;
    }

    private bool IsAtEnd() => current >= source.Length;

    private void ScanToken()
    {
        char c = Advance();
        switch (c)
        {
            case '(': AddToken(LEFT_PAREN); 
                break;
            case ')': AddToken(RIGHT_PAREN); 
                break;
            case '{': AddToken(LEFT_BRACE); 
                break;
            case '}': AddToken(RIGHT_BRACE); 
                break;
            case ',': AddToken(COMMA); 
                break;
            case '.': AddToken(DOT); 
                break;
            case '-': AddToken(MINUS); 
                break;
            case '+': AddToken(PLUS); 
                break;
            case ';': AddToken(SEMICOLON); 
                break;
            case '*': AddToken(STAR); 
                break;
            case '!': AddToken(Match('=') ? BANG_EQUAL : BANG); 
                break;
            case '=': AddToken(Match('=') ? EQUAL_EQUAL : EQUAL);
                break;
            case '<': AddToken(Match('=') ? LESS_EQUAL : LESS);
                break;
            case '>': AddToken(Match('=') ? GREATER_EQUAL : GREATER);
                break;
            case '/': // I must do this still.
                break;

            default:
                Lox.Error(line, "Unexpected character.");
                break;
        }
    }

    private char Advance() => source[current++];

    private void AddToken(TokenType type) => AddToken(type, null);

    private void AddToken(TokenType type, object? literal)
    {
        string text = source.Substring(start, current);
        tokens.Add(new Token(type, text, literal, line));
    }

    private bool Match(char expected)
    {
        if (IsAtEnd())
            return false;

        if (source[current] != expected) 
            return false;

        current++;
        return true;
    }
}
