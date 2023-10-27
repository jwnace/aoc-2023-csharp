using aoc_2023_csharp.Day25;

namespace aoc_2023_csharp_tests;

public class Day25Tests
{
    [Test]
    public void Part1_Example()
    {
        // arrange
        var input = "";
        var expected = 0;

        // act
        var actual = Day25.Solve1(input);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part1_Solution()
    {
        Day25.Part1().Should().Be(0);
    }

    [Test]
    public void Part2_Example()
    {
        // arrange
        var input = "";
        var expected = 0;

        // act
        var actual = Day25.Solve2(input);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_Solution()
    {
        Day25.Part2().Should().Be(0);
    }
}
