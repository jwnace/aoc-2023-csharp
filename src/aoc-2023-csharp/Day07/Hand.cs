using aoc_2023_csharp.Extensions;

namespace aoc_2023_csharp.Day07;

public record Hand(Card[] Cards, long Bid)
{
    private HandType _handType = HandType.Unknown;

    public HandType HandType
    {
        get
        {
            if (_handType == HandType.Unknown)
            {
                _handType = CalculateHandType();
            }

            return _handType;
        }
    }

    private static CardType GetCardType(char c, bool useJokers) => c switch
    {
        'J' when useJokers => CardType.Joker,
        '2' => CardType.Two,
        '3' => CardType.Three,
        '4' => CardType.Four,
        '5' => CardType.Five,
        '6' => CardType.Six,
        '7' => CardType.Seven,
        '8' => CardType.Eight,
        '9' => CardType.Nine,
        'T' => CardType.Ten,
        'J' when !useJokers => CardType.Jack,
        'Q' => CardType.Queen,
        'K' => CardType.King,
        'A' => CardType.Ace,
        _ => CardType.None,
    };

    private bool IsFiveOfAKind() =>
        Cards.GroupByType().Count() == 1 ||
        Cards.ExcludeJokers().GroupByType().Count() == 1;

    private bool IsFourOfAKind() =>
        Cards.ExcludeJokers().GroupByType().Any(g => g.Count() + Cards.CountJokers() == 4);

    private bool IsFullHouse() =>
        Cards.ExcludeJokers().GroupByType().Count() == 2;

    private bool IsThreeOfAKind() =>
        Cards.ExcludeJokers().GroupByType().Any(g => g.Count() + Cards.CountJokers() == 3);

    private bool IsTwoPairs()
    {
        var nonJokers = Cards.ExcludeJokers();
        var jokerCount = Cards.CountJokers();

        return jokerCount switch
        {
            0 => Cards.GroupByType().Count(g => g.Count() == 2) == 2,
            1 => nonJokers.GroupByType().Any(g => g.Count() == 2),
            2 => nonJokers.GroupByType().All(g => g.Count() == 1),
            _ => false
        };
    }

    private bool IsOnePair()
    {
        var jokerCount = Cards.CountJokers();

        return jokerCount switch
        {
            0 => Cards.GroupByType().Any(g => g.Count() == 2),
            1 => true,
            _ => false
        };
    }

    private HandType CalculateHandType()
    {
        if (IsFiveOfAKind()) return HandType.FiveOfAKind;
        if (IsFourOfAKind()) return HandType.FourOfAKind;
        if (IsFullHouse()) return HandType.FullHouse;
        if (IsThreeOfAKind()) return HandType.ThreeOfAKind;
        if (IsTwoPairs()) return HandType.TwoPairs;
        if (IsOnePair()) return HandType.OnePair;
        return HandType.HighCard;
    }

    public static Hand Parse(string line, bool useJokers)
    {
        var (cards, bid) = line.Split(" ");

        var cardArray = cards.Select(c => GetCardType(c, useJokers))
            .Select(type => new Card(type))
            .ToArray();

        return new Hand(cardArray, long.Parse(bid));
    }
}
