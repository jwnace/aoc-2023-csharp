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

    private bool IsFiveOfAKind()
    {
        // if all of the cards are the same type, we already have five of a kind
        if (Cards.GroupByType().Count() == 1)
        {
            return true;
        }

        // if all non-joker cards are the same type, we can make five of a kind
        return Cards.ExcludeJokers().GroupByType().Count() == 1;
    }

    private bool IsFourOfAKind()
    {
        // if we can all of the jokers to any group and end up with a group of four, we can make four of a kind
        return Cards.ExcludeJokers().GroupByType().Any(g => g.Count() + Cards.CountJokers() == 4);
    }

    private bool IsFullHouse()
    {
        // if we only have two unique types (not including jokers), we can make a full house
        return Cards.ExcludeJokers().GroupByType().Count() == 2;
    }

    private bool IsThreeOfAKind()
    {
        // if we can all of the jokers to any group and end up with a group of three, we can make four of a kind
        return Cards.ExcludeJokers().GroupByType().Any(g => g.Count() + Cards.CountJokers() == 3);
    }

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
            0 => Cards.GroupByType().Count() == 4,
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
}
