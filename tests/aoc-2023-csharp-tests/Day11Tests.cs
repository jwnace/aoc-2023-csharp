using aoc_2023_csharp.Day11;

namespace aoc_2023_csharp_tests;

public class Day11Tests
{
    [Test]
    public void Part1_Example()
    {
        // arrange
        var input = new[]
        {
            "...#......",
            ".......#..",
            "#.........",
            "..........",
            "......#...",
            ".#........",
            ".........#",
            "..........",
            ".......#..",
            "#...#.....",
        };

        var expected = 374;

        // act
        var actual = Day11.Solve1(input);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part1_Solution()
    {
        Day11.Part1().Should().Be(9521550);
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
        var actual = Day11.Solve2(input);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_Solution()
    {
        Day11.Part2().Should().Be(0);
    }
}
