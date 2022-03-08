public class LexerPointer
{
    public int CurrentIndex { get; set; }
    public int CurrentRow { get; set; }
    public int CurrentColumn { get; set; }

    public static LexerPointer Initial() => new()
    {
        CurrentIndex = 0,
        CurrentRow = 1,
        CurrentColumn = 0
    };

    public override string ToString()
    {
        return $"Row {CurrentRow} Column {CurrentColumn} Index {CurrentIndex}";
    }
}
