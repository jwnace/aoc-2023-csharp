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

    [TestCase(1, 374)]
    [TestCase(9, 1030)]
    [TestCase(99, 8410)]
    public void Part2_Example(int expansion, int expected)
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

        // act
        var actual = Day11.Solve2(input, expansion);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_Solution()
    {
        Day11.Part2().Should().Be(298932923702);
    }
}
