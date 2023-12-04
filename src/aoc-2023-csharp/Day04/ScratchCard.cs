namespace aoc_2023_csharp.Day04;

public record ScratchCard(int CardNumber, int[] WinningNumbers, int[] NumbersYouHave)
{
    public int WinCount => WinningNumbers.Intersect(NumbersYouHave).Count();

    public int Score => Convert.ToInt32(Math.Pow(2, WinCount - 1));
}
