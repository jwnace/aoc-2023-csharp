﻿using aoc_2023_csharp.Extensions;

namespace aoc_2023_csharp.Day04;

public static class Day04
{
    private static readonly string[] Input = File.ReadAllLines("Day04/day04.txt").ToArray();

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    public static int Solve1(string[] input) => GetScratchCards(input).Sum(c => c.Score);

    public static int Solve2(string[] input)
    {
        var cards = GetScratchCards(input).ToArray();
        var cardCounts = cards.ToDictionary(card => card.CardNumber, _ => 1);

        var minCardNumber = cards.Min(c => c.CardNumber);
        var maxCardNumber = cards.Max(c => c.CardNumber);

        for (var cardNumber = minCardNumber; cardNumber <= maxCardNumber; cardNumber++)
        {
            var card = cards.First(c => c.CardNumber == cardNumber);
            var cardCount = cardCounts[card.CardNumber];

            for (var j = 1; j <= card.WinCount; j++)
            {
                cardCounts[cardNumber + j] += cardCount;
            }
        }

        return cardCounts.Values.Sum();
    }

    private static IEnumerable<ScratchCard> GetScratchCards(string[] input)
    {
        foreach (var line in input)
        {
            var (left, right) = line.Split(": ");
            var cardNumber = int.Parse(left[5..]);
            var (winningNumbers, numbersYouHave) = right.Split(" | ");

            var winningNumbersArray = winningNumbers.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var numbersYouHaveArray = numbersYouHave.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            yield return new ScratchCard(cardNumber, winningNumbersArray, numbersYouHaveArray);
        }
    }
}
