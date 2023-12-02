using aoc_2023_csharp.Day02;

namespace aoc_2023_csharp_tests;

public class Day02Tests
{
    [Test]
    public void Part1_Example()
    {
        // arrange
        var input = new[]
        {
            "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
            "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
            "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
            "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
            "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green",
        };

        // act
        var actual = Day02.Solve1(input);

        // assert
        actual.Should().Be(8);
    }

    [Test]
    public void Part1_Solution()
    {
        Day02.Part1().Should().Be(2348);
    }

    [Test]
    public void Part2_Example()
    {
        // arrange
        var input = new[]
        {
            "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
            "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
            "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
            "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
            "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green",
        };

        // act
        var actual = Day02.Solve2(input);

        // assert
        actual.Should().Be(2286);
    }

    [Test]
    public void Part2_Solution()
    {
        Day02.Part2().Should().Be(76008);
    }
}
