using aoc_2023_csharp.Day07;

namespace aoc_2023_csharp_tests;

public class Day07Tests
{
    [Test]
    public void Part1_Example()
    {
        // arrange
        var input = new[]
        {
            "32T3K 765",
            "T55J5 684",
            "KK677 28",
            "KTJJT 220",
            "QQQJA 483",
        };

        var expected = 6440;

        // act
        var actual = Day07.Solve1(input);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part1_Solution()
    {
        Day07.Part1().Should().Be(249390788);
    }

    [Test]
    public void Part2_Example()
    {
        // arrange
        var input = new[]
        {
            "32T3K 765",
            "T55J5 684",
            "KK677 28",
            "KTJJT 220",
            "QQQJA 483",
        };

        var expected = 5905;

        // act
        var actual = Day07.Solve2(input);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_Solution()
    {
        Day07.Part2().Should().Be(248750248);
    }
}
