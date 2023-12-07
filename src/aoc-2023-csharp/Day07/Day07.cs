using aoc_2023_csharp.Extensions;

namespace aoc_2023_csharp.Day07;

public static class Day07
{
    private static readonly string[] Input = File.ReadAllLines("Day07/day07.txt").ToArray();

    public static long Part1() => Solve1(Input);

    public static long Part2() => Solve2(Input);

    public static long Solve1(string[] input)
    {
        var hands = new List<Hand>();

        foreach (var line in input)
        {
            var (cards, bid) = line.Split(" ");

            var cardArray = cards.Select(c => GetCardType(c, useJokers: false))
                .Select(type => new Card(type))
                .ToArray();

            var hand = new Hand(cardArray, long.Parse(bid));

            hands.Add(hand);
        }

        var sortedHands =
            hands.OrderBy(x => x.HandType)
                .ThenBy(x => x.Cards[0].Type)
                .ThenBy(x => x.Cards[1].Type)
                .ThenBy(x => x.Cards[2].Type)
                .ThenBy(x => x.Cards[3].Type)
                .ThenBy(x => x.Cards[4].Type)
                .ToList();

        var query = sortedHands.Select((hand, index) => hand.Bid * (index + 1)).Sum();

        return query;
    }

    public static long Solve2(string[] input)
    {
        var hands = new List<Hand>();

        foreach (var line in input)
        {
            var (cards, bid) = line.Split(" ");

            var cardArray = cards.Select(c => GetCardType(c, useJokers: true))
                .Select(type => new Card(type))
                .ToArray();

            var hand = new Hand(cardArray, long.Parse(bid));

            hands.Add(hand);
        }

        var sortedHands =
            hands.OrderBy(x => x.HandType)
                .ThenBy(x => x.Cards[0].Type)
                .ThenBy(x => x.Cards[1].Type)
                .ThenBy(x => x.Cards[2].Type)
                .ThenBy(x => x.Cards[3].Type)
                .ThenBy(x => x.Cards[4].Type)
                .ToList();

        return sortedHands.Select((hand, index) => hand.Bid * (index + 1)).Sum();
    }

    private static CardType GetCardType(char c, bool useJokers = false) => c switch
    {
        'J' when useJokers == true => CardType.Joker,
        '2' => CardType.Two,
        '3' => CardType.Three,
        '4' => CardType.Four,
        '5' => CardType.Five,
        '6' => CardType.Six,
        '7' => CardType.Seven,
        '8' => CardType.Eight,
        '9' => CardType.Nine,
        'T' => CardType.Ten,
        'J' when useJokers == false => CardType.Jack,
        'Q' => CardType.Queen,
        'K' => CardType.King,
        'A' => CardType.Ace,
        _ => CardType.None,
    };
}
