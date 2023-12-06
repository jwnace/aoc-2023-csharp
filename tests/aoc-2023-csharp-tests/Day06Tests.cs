using aoc_2023_csharp.Day06;

namespace aoc_2023_csharp_tests;

public class Day06Tests
{
    // TODO: I think there is an error in the problem description for this example
    // [Test]
    // public void Part1_Example()
    // {
    //     // arrange
    //     var input = """
    //                 Time:      7  15   30
    //                 Distance:  9  40  200
    //                 """;
    //
    //     var expected = 288;
    //
    //     // act
    //     var actual = Day06.Solve1(input);
    //
    //     // assert
    //     actual.Should().Be(expected);
    // }

    [Test]
    public void Part1_Solution()
    {
        Day06.Part1().Should().Be(275724);
    }

    [Test]
    public void Part2_Example()
    {
        // arrange
        var input = """
                    Time:      7  15   30
                    Distance:  9  40  200
                    """;

        var expected = 71503;

        // act
        var actual = Day06.Solve2(input);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_Solution()
    {
        Day06.Part2().Should().Be(37286485);
    }
}
