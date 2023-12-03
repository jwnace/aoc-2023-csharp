namespace aoc_2023_csharp.Day03;

public record PartNumber(int Number, int Row, int StartCol, int EndCol)
{
    public bool IsAdjacentToSymbol(Dictionary<(int Row, int Col), char> grid) =>
        grid.Where(g => g.Value.IsSymbol())
            .Any(symbol => IsAdjacentTo(symbol.Key));

    public bool IsAdjacentTo((int Row, int Col) position) =>
        Row >= position.Row - 1 &&
        Row <= position.Row + 1 &&
        EndCol >= position.Col - 1 &&
        StartCol <= position.Col + 1;
}
