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

    [Ignore("ignoring this for now...")]
    [TestCase(6, 16)]
    [TestCase(10, 50)]
    [TestCase(50, 1594)]
    [TestCase(100, 6536)]
    [TestCase(500, 167004)]
    [TestCase(1000, 668697)]
    [TestCase(5000, 16733044)]
    public void Part2_Example(int steps, int expected)
    {
        // arrange
        var input = new[]
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
        };

        // act
        var actual = Day21.Solve2(input, steps);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_Solution()
    {
        Day21.Part2().Should().Be(610158187362102);
    }
}
