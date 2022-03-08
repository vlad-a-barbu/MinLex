using System.Reflection;

var lexer = new Lexer();

lexer.AddDescriptor(@";", "Delimiter");
lexer.AddDescriptor(@"\.", "Dot");
lexer.AddDescriptor(@",", "Comma");
lexer.AddDescriptor(@"\(", "LeftRoundBracket");
lexer.AddDescriptor(@"\)", "RightRoundBracket");
lexer.AddDescriptor(@"\[", "LeftSquareBracket");
lexer.AddDescriptor(@"\]", "RightSquareBracket");
lexer.AddDescriptor(@"{", "LeftCurlyBracket");
lexer.AddDescriptor(@"}", "RightCurlyBracket");
lexer.AddDescriptor(@"<", "LeftAngleBracket");
lexer.AddDescriptor(@">", "RightAngleBracket");
lexer.AddDescriptor(@"=|-|\*|\+|/|%|\^|&|@|!|\?|\$", "Operator");
lexer.AddDescriptor(@"int|string|float|double|bool|using|if|else|for|class|struct|return", "Keyword");
lexer.AddDescriptor("true|false", "BoolLiteral");
lexer.AddDescriptor(@"[_a-zA-Z][_a-zA-Z0-9]{0,30}", "Identifier");
lexer.AddDescriptor(@"[\-\+]?[0-9]*(\.[0-9]+)?", "Number");
lexer.AddDescriptor("\\\"(.*?)\\\"", "StringLiteral");
lexer.AddDescriptor(@"/[*][\w\d\s]*[*]/", "Comment");

var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MinLex.Demo.Input.cs")!;
using var reader = new StreamReader(stream);
var input = reader.ReadToEnd();

foreach (var token in lexer.Lex(input))
{
    Console.WriteLine(token);
}
