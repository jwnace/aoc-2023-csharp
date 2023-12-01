using aoc_2023_csharp.Day01;

namespace aoc_2023_csharp_tests;

public class Day01Tests
{
    [Test]
    public void Part1_Example()
    {
        // arrange
        var input = new[]
        {
            "1abc2",
            "pqr3stu8vwx",
            "a1b2c3d4e5f",
            "treb7uchet",
        };

        // act
        var actual = Day01.Solve1(input);

        // assert
        actual.Should().Be(142);
    }

    [Test]
    public void Part1_Solution()
    {
        Day01.Part1().Should().Be(55208);
    }

    [Test]
    public void Part2_Example()
    {
        // arrange
        var input = new[]
        {
            "two1nine",
            "eightwothree",
            "abcone2threexyz",
            "xtwone3four",
            "4nineeightseven2",
            "zoneight234",
            "7pqrstsixteen",
        };

        // act
        var actual = Day01.Solve2(input);

        // assert
        actual.Should().Be(281);
    }

    [Test]
    public void Part2_Solution()
    {
        Day01.Part2().Should().Be(54578);
    }
}
