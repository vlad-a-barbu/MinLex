using System.Text.RegularExpressions;

public class Lexer
{
    public const string EOF = "EOF";
    public const string WHITESPACE = "WHITESPACE";

    private readonly List<TokenDescriptor> descriptors;

    private static readonly Regex whiteSpaceDescriptor = 
        new ($@"((\r|\t|\v|\f| )*(?<NewLine>({Environment.NewLine}|\n)+)?)+", RegexOptions.Compiled);

    public Lexer()
    {
        descriptors = new List<TokenDescriptor>();
    }

    public void AddDescriptor(string expression, string tokenType)
    {
        descriptors.Add(new TokenDescriptor(expression, tokenType));
    }

    public IEnumerable<Token> Lex(string input)
    {
        var ptr = LexerPointer.Initial();

        while (ptr.CurrentIndex < input.Length)
        {
            var result = MatchDescriptors(input, ptr);

            var value = input.Substring(ptr.CurrentIndex, result.Match.Length);

            yield return new Token(result.Descriptor.Type, value, ptr);

            MovePtr(input, ptr, result.Match.Length);
        }

        yield return new Token(EOF, null, ptr);
    }

    private (Match Match, TokenDescriptor Descriptor) MatchDescriptors(string input, LexerPointer ptr)
    {
        var matchedResult =
                descriptors
                    .Select(descriptor => (Match: descriptor.Expr.Match(input, ptr.CurrentIndex), Descriptor: descriptor))
                    .Where(matchResult => matchResult.Match.Success && matchResult.Match.Index - ptr.CurrentIndex == 0)
                    .MaxBy(matchResult => matchResult.Match.Length);

        _ = matchedResult.Descriptor ?? throw new Exception($"Unrecognized '{input.ElementAt(ptr.CurrentIndex)}' at {ptr}");

        return matchedResult;
    }

    private void MovePtr(string input, LexerPointer ptr, int matchLength)
    {
        var whitespaceMatchResult = whiteSpaceDescriptor.Match(input, ptr.CurrentIndex + matchLength);

        int step = matchLength;

        if (whitespaceMatchResult.Success && whitespaceMatchResult.Length > 0)
        {
            step += whitespaceMatchResult.Length;

            var newLinesMatchResult = whitespaceMatchResult.Groups["NewLine"];

            if (newLinesMatchResult.Success)
            {
                ptr.CurrentRow += newLinesMatchResult.Captures.Count;
                ptr.CurrentColumn = whitespaceMatchResult.Length - (whitespaceMatchResult.Value.LastIndexOf(newLinesMatchResult.Value) + 1);
            }
            else
            {
                ptr.CurrentColumn += step;
            }

        }
        else
        {
            ptr.CurrentColumn += step;
        }

        ptr.CurrentIndex += step;
    }
}
