namespace aoc_2023_csharp.Day03;

public static class CharExtensions
{
    public static bool IsSymbol(this char c) => !char.IsDigit(c) && c != '.';
}
