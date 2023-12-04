using aoc_2023_csharp.Extensions;

namespace aoc_2023_csharp.Day04;

public record ScratchCard(int CardNumber, int[] WinningNumbers, int[] NumbersYouHave)
{
    public static ScratchCard Parse(string input)
    {
        var (left, right) = input.Split(": ");
        var cardNumber = int.Parse(left[5..]);
        var (winningNumbers, numbersYouHave) = right.Split(" | ");

        var winningNumbersArray = winningNumbers.Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();

        var numbersYouHaveArray = numbersYouHave.Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();

        return new ScratchCard(cardNumber, winningNumbersArray, numbersYouHaveArray);
    }

    public int WinCount => WinningNumbers.Intersect(NumbersYouHave).Count();

    public int Score => Convert.ToInt32(Math.Pow(2, WinCount - 1));
}
