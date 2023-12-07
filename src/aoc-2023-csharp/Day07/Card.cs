namespace aoc_2023_csharp.Day07;

public record Card(CardType Type);

public static class CardExtensions
{
    public static IEnumerable<IGrouping<CardType, Card>> GroupByType(this IEnumerable<Card> cards) =>
        cards.GroupBy(card => card.Type);

    public static IEnumerable<Card> ExcludeJokers(this IEnumerable<Card> cards) =>
        cards.Where(card => card.Type != CardType.Joker);

    public static int CountJokers(this IEnumerable<Card> cards) =>
        cards.Count(card => card.Type == CardType.Joker);
}
