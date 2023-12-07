using aoc_2023_csharp.Extensions;

namespace aoc_2023_csharp.Day07;

public static class Day07
{
    private static readonly string[] Input = File.ReadAllLines("Day07/day07.txt").ToArray();

    private static readonly Dictionary<char, int> StrengthLookup = new()
    {
        {
            '2', 1
        },
        {
            '3', 2
        },
        {
            '4', 3
        },
        {
            '5', 4
        },
        {
            '6', 5
        },
        {
            '7', 6
        },
        {
            '8', 7
        },
        {
            '9', 8
        },
        {
            'T', 9
        },
        {
            'J', 10
        },
        {
            'Q', 11
        },
        {
            'K', 12
        },
        {
            'A', 13
        },
    };

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    public static int Solve1(string[] input)
    {
        var hands = new List<Hand>();

        foreach (var line in input)
        {
            var (cards, bid) = line.Split(" ");

            var cardArray = cards.Select(x => StrengthLookup[x]).Select(x => new Card(x)).ToArray();

            var hand = new Hand(cardArray, int.Parse(bid));

            hands.Add(hand);
        }

        var sortedHands =
            hands.OrderBy(x => x.HandType)
                .ThenBy(x => x.Cards[0].Strength)
                .ThenBy(x => x.Cards[1].Strength)
                .ThenBy(x => x.Cards[2].Strength)
                .ThenBy(x => x.Cards[3].Strength)
                .ThenBy(x => x.Cards[4].Strength)
                .ToList();

        var query = sortedHands.Select((hand, index) => hand.Bid * (index + 1)).Sum();

        return query;
    }

    public static int Solve2(string[] input)
    {
        return 0;
    }

    private record Hand(Card[] Cards, int Bid)
    {
        public bool IsFiveOfAKind() => Cards.GroupBy(x => x.Strength).Count() == 1;

        public bool IsFourOfAKind() => Cards.GroupBy(x => x.Strength).Any(g => g.Count() == 4);

        public bool IsFullHouse() =>
            Cards.GroupBy(x => x.Strength).Any(g => g.Count() == 3) &&
            Cards.GroupBy(x => x.Strength).Any(g => g.Count() == 2);

        public bool IsThreeOfAKind() => Cards.GroupBy(x => x.Strength).Any(g => g.Count() == 3);

        public bool IsTwoPairs() => Cards.GroupBy(x => x.Strength).Count() == 3;

        public bool IsOnePair() => Cards.GroupBy(x => x.Strength).Count() == 4;

        public bool IsHighCard() => Cards.GroupBy(x => x.Strength).Count() == 5;

        public HandType HandType
        {
            get
            {
                if (IsFiveOfAKind()) return HandType.FiveOfAKind;
                if (IsFourOfAKind()) return HandType.FourOfAKind;
                if (IsFullHouse()) return HandType.FullHouse;
                if (IsThreeOfAKind()) return HandType.ThreeOfAKind;
                if (IsTwoPairs()) return HandType.TwoPairs;
                if (IsOnePair()) return HandType.OnePair;
                if (IsHighCard()) return HandType.HighCard;

                throw new Exception("No hand type found");
            }
        }
    }

    private record Card(int Strength);

    public enum HandType
    {
        HighCard,
        OnePair,
        TwoPairs,
        ThreeOfAKind,
        FullHouse,
        FourOfAKind,
        FiveOfAKind,
    }
}
