public class Token
{
    public string? Val { get; }
    public string Type { get; }
    public LexerPointer Ptr { get; }

    public Token(string type, string? val, LexerPointer ptr)
    {
        Type = type;
        Val = val;
        Ptr = ptr;
    }

    public override string ToString()
    {
        return $"[Token] Type '{Type}' | Value '{Val}' | Length '{Val?.Length ?? 0}' | {Ptr}";
    }
}
