using aoc_2023_csharp.Extensions;

namespace aoc_2023_csharp.Day04;

public static class Day04
{
    private static readonly string[] Input = File.ReadAllLines("Day04/day04.txt").ToArray();

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    public static int Solve1(string[] input)
    {
        var total = 0;

        foreach (var line in input)
        {
            var (left, right) = line.Split(": ");

            var (l, r) = right.Split(" | ");

            var winningNumbers = l.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var numbersYouHave = r.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            var intersection = winningNumbers.Intersect(numbersYouHave);
            var count = intersection.Count();

            var score = Convert.ToInt32(Math.Pow(2, count - 1));

            total += score;
        }

        return total;
    }

    public static int Solve2(string[] input)
    {
        var scratchCards = GetScratchCards(input).ToList();

        var min = scratchCards.Min(x => x.CardNumber);
        var max = scratchCards.Max(x => x.CardNumber);

        for (var i = min; i <= max; i++)
        {
            var cardCount = scratchCards.Count(x => x.CardNumber == i);

            if (cardCount > 0)
            {
                var card = scratchCards.First(x => x.CardNumber == i);
                var winCount = card.WinningNumbers.Intersect(card.NumbersYouHave).Count();

                if (winCount > 0)
                {
                    for (var k = 0; k < cardCount; k++)
                    {
                        for (var j = 1; j <= winCount; j++)
                        {
                            var cardToCopy = scratchCards.First(x => x.CardNumber == i + j);
                            scratchCards.Add(cardToCopy);
                        }
                    }
                }
            }
        }

        return scratchCards.Count;
    }

    private static IEnumerable<ScratchCard> GetScratchCards(string[] input)
    {
        foreach (var line in input)
        {
            var (left, right) = line.Split(": ");
            var cardNumber = int.Parse(left[5..]);

            var (winningNumbers, numbersYouHave) = right.Split(" | ");

            var winningNumbersArray = winningNumbers.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var numbersYouHaveArray = numbersYouHave.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            yield return new ScratchCard(cardNumber, winningNumbersArray, numbersYouHaveArray);
        }
    }

    private record ScratchCard(int CardNumber, int[] WinningNumbers, int[] NumbersYouHave);
}
