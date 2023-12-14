using aoc_2023_csharp.Day14;

namespace aoc_2023_csharp_tests;

public class Day14Tests
{
    [Test]
    public void Part1_Example()
    {
        // arrange
        var input = new[]
        {
            "O....#....",
            "O.OO#....#",
            ".....##...",
            "OO.#O....O",
            ".O.....O#.",
            "O.#..O.#.#",
            "..O..#O..O",
            ".......O..",
            "#....###..",
            "#OO..#....",
        };

        var expected = 136;

        // act
        var actual = Day14.Solve1(input);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part1_Solution()
    {
        Day14.Part1().Should().Be(106517);
    }

    [Test]
    public void Part2_Example()
    {
        // arrange
        var input = new[]
        {
            "O....#....",
            "O.OO#....#",
            ".....##...",
            "OO.#O....O",
            ".O.....O#.",
            "O.#..O.#.#",
            "..O..#O..O",
            ".......O..",
            "#....###..",
            "#OO..#....",
        };

        var expected = 64;

        // act
        var actual = Day14.Solve2(input);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_Solution()
    {
        Day14.Part2().Should().Be(79723);
    }
}
