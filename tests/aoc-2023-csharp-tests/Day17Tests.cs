using aoc_2023_csharp.Day17;

namespace aoc_2023_csharp_tests;

public class Day17Tests
{
    [Test]
    public void Part1_Example()
    {
        // arrange
        var input = new[]
        {
            "2413432311323",
            "3215453535623",
            "3255245654254",
            "3446585845452",
            "4546657867536",
            "1438598798454",
            "4457876987766",
            "3637877979653",
            "4654967986887",
            "4564679986453",
            "1224686865563",
            "2546548887735",
            "4322674655533",
        };

        var expected = 102;

        // act
        var actual = Day17.Solve1(input);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part1_Solution()
    {
        Day17.Part1().Should().Be(668);
    }

    [TestCase(new[]
    {
        "2413432311323",
        "3215453535623",
        "3255245654254",
        "3446585845452",
        "4546657867536",
        "1438598798454",
        "4457876987766",
        "3637877979653",
        "4654967986887",
        "4564679986453",
        "1224686865563",
        "2546548887735",
        "4322674655533",
    }, 94)]
    [TestCase(new[]
    {
        "111111111111",
        "999999999991",
        "999999999991",
        "999999999991",
        "999999999991",
    }, 71)]
    public void Part2_Example(string[] input, int expected)
    {
        // act
        var actual = Day17.Solve2(input);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_Solution()
    {
        Day17.Part2().Should().Be(788);
    }
}
