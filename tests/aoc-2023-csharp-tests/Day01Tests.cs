using aoc_2023_csharp.Day01;

namespace aoc_2023_csharp_tests;

public class Day01Tests
{
    [Test]
    public void Part1_Example_ReturnsCorrectAnswer()
    {
        var input = "";
        Day01.Solve1(input).Should().Be(0);
    }

    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        Day01.Part1().Should().Be(0);
    }

    [Test]
    public void Part2_Example_ReturnsCorrectAnswer()
    {
        var input = "";
        Day01.Solve1(input).Should().Be(0);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        Day01.Part2().Should().Be(0);
    }
}
