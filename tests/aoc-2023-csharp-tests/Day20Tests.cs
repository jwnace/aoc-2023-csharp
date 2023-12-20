using aoc_2023_csharp.Day20;

namespace aoc_2023_csharp_tests;

public class Day20Tests
{
    [TestCase(new[]
    {
        "broadcaster -> a, b, c",
        "%a -> b",
        "%b -> c",
        "%c -> inv",
        "&inv -> a",
    }, 32000000)]
    [TestCase(new[]
    {
        "broadcaster -> a",
        "%a -> inv, con",
        "&inv -> b",
        "%b -> con",
        "&con -> output",
    }, 11687500)]
    public void Part1_Example(string[] input, long expected)
    {
        // act
        var actual = Day20.Solve1(input);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part1_Solution()
    {
        Day20.Part1().Should().Be(938065580);
    }

    [Test]
    public void Part2_Solution()
    {
        Day20.Part2().Should().Be(250628960065793);
    }
}
