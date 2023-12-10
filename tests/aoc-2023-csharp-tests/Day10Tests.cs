using aoc_2023_csharp.Day10;

namespace aoc_2023_csharp_tests;

public class Day10Tests
{
    [TestCase(new[]
    {
        ".....",
        ".S-7.",
        ".|.|.",
        ".L-J.",
        ".....",
    }, 4)]
    [TestCase(new[]
    {
        "..F7.",
        ".FJ|.",
        "SJ.L7",
        "|F--J",
        "LJ...",
    }, 8)]
    public void Part1_Example(string[] input, int expected)
    {
        // act
        var actual = Day10.Solve1(input);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part1_Solution()
    {
        Day10.Part1().Should().Be(7066);
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
        var actual = Day10.Solve2(input);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_Solution()
    {
        Day10.Part2().Should().Be(0);
    }
}
