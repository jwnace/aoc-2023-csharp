using aoc_2023_csharp.Day24;

namespace aoc_2023_csharp_tests;

public class Day24Tests
{
    [Test]
    public void Part1_Example()
    {
        // arrange
        var input = new[]
        {
            "19, 13, 30 @ -2,  1, -2",
            "18, 19, 22 @ -1, -1, -2",
            "20, 25, 34 @ -2, -2, -4",
            "12, 31, 28 @ -1, -2, -1",
            "20, 19, 15 @  1, -5, -3",
        };

        var expected = 2;

        // act
        var actual = Day24.Solve1(input, (7, 27));

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part1_Solution()
    {
        Day24.Part1().Should().Be(14799);
    }

    [Test]
    public void Part2_Example()
    {
        // arrange
        var input = new[]
        {
            "",
        };

        var expected = 0;

        // act
        var actual = Day24.Solve2(input);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_Solution()
    {
        Day24.Part2().Should().Be(0);
    }
}
