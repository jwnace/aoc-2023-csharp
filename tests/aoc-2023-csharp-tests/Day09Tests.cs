using aoc_2023_csharp.Day09;

namespace aoc_2023_csharp_tests;

public class Day09Tests
{
    [Test]
    public void Part1_Example()
    {
        // arrange
        var input = new[]
        {
            "0 3 6 9 12 15",
            "1 3 6 10 15 21",
            "10 13 16 21 30 45",
        };

        var expected = 114;

        // act
        var actual = Day09.Solve1(input);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part1_Solution()
    {
        Day09.Part1().Should().Be(1904165718);
    }

    [Test]
    public void Part2_Example()
    {
        // arrange
        var input = new[]
        {
            "0 3 6 9 12 15",
            "1 3 6 10 15 21",
            "10 13 16 21 30 45",
        };

        var expected = 2;

        // act
        var actual = Day09.Solve2(input);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_Solution()
    {
        Day09.Part2().Should().Be(964);
    }
}
