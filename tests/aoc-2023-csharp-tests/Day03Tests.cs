using aoc_2023_csharp.Day03;

namespace aoc_2023_csharp_tests;

public class Day03Tests
{
    [Test]
    public void Part1_Example()
    {
        // arrange
        var input = new[]
        {
            "467..114..",
            "...*......",
            "..35..633.",
            "......#...",
            "617*......",
            ".....+.58.",
            "..592.....",
            "......755.",
            "...$.*....",
            ".664.598..",
        };

        var expected = 4361;

        // act
        var actual = Day03.Solve1(input);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part1_Solution()
    {
        Day03.Part1().Should().Be(514969);
    }

    [Test]
    public void Part2_Example()
    {
        // arrange
        var input = new[]
        {
            "467..114..",
            "...*......",
            "..35..633.",
            "......#...",
            "617*......",
            ".....+.58.",
            "..592.....",
            "......755.",
            "...$.*....",
            ".664.598..",
        };

        var expected = 467835;

        // act
        var actual = Day03.Solve2(input);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_Solution()
    {
        Day03.Part2().Should().Be(78915902);
    }
}
