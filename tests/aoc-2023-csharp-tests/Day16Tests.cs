using aoc_2023_csharp.Day16;

namespace aoc_2023_csharp_tests;

public class Day16Tests
{
    [Test]
    public void Part1_Example()
    {
        // arrange
        var input = new[]
        {
            @".|...\....",
            @"|.-.\.....",
            @".....|-...",
            @"........|.",
            @"..........",
            @".........\",
            @"..../.\\..",
            @".-.-/..|..",
            @".|....-|.\",
            @"..//.|....",
        };

        var expected = 46;

        // act
        var actual = Day16.Solve1(input);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part1_Solution()
    {
        Day16.Part1().Should().Be(8098);
    }

    [Test]
    public void Part2_Example()
    {
        // arrange
        var input = new[]
        {
            @".|...\....",
            @"|.-.\.....",
            @".....|-...",
            @"........|.",
            @"..........",
            @".........\",
            @"..../.\\..",
            @".-.-/..|..",
            @".|....-|.\",
            @"..//.|....",
        };

        var expected = 51;

        // act
        var actual = Day16.Solve2(input);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_Solution()
    {
        Day16.Part2().Should().Be(8335);
    }
}
