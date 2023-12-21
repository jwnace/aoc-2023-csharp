using aoc_2023_csharp.Day21;

namespace aoc_2023_csharp_tests;

public class Day21Tests
{
    [TestCase(new[]
    {
        "...........",
        ".....###.#.",
        ".###.##..#.",
        "..#.#...#..",
        "....#.#....",
        ".##..S####.",
        ".##..#...#.",
        ".......##..",
        ".##.#.####.",
        ".##..##.##.",
        "...........",
    }, 6, 16)]
    public void Part1_Example(string[] input, int steps, int expected)
    {
        // act
        var actual = Day21.Solve1(input, steps);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part1_Solution()
    {
        Day21.Part1().Should().Be(3689);
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
        var actual = Day21.Solve2(input);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_Solution()
    {
        Day21.Part2().Should().Be(0);
    }
}
