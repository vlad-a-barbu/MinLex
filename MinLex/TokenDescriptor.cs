using System.Text.RegularExpressions;

public class TokenDescriptor
{
    public string Type { get; }
    public Regex Expr { get; }

    public TokenDescriptor(string regex, string type)
    {
        Expr = new Regex(regex);
        Type = type;
    }
}
