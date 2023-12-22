using aoc_2023_csharp.Day22;

namespace aoc_2023_csharp_tests;

public class Day22Tests
{
    [Test]
    public void Part1_Example()
    {
        // arrange
        var input = new[]
        {
            "1,0,1~1,2,1",
            "0,0,2~2,0,2",
            "0,2,3~2,2,3",
            "0,0,4~0,2,4",
            "2,0,5~2,2,5",
            "0,1,6~2,1,6",
            "1,1,8~1,1,9",
        };

        var expected = 5;

        // act
        var actual = Day22.Solve1(input);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part1_Solution()
    {
        Day22.Part1().Should().Be(524);
    }

    [Test]
    public void Part2_Example()
    {
        // arrange
        var input = new[]
        {
            "1,0,1~1,2,1",
            "0,0,2~2,0,2",
            "0,2,3~2,2,3",
            "0,0,4~0,2,4",
            "2,0,5~2,2,5",
            "0,1,6~2,1,6",
            "1,1,8~1,1,9",
        };

        var expected = 7;

        // act
        var actual = Day22.Solve2(input);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_Solution()
    {
        Day22.Part2().Should().Be(77070);
    }
}
