using aoc_2023_csharp.Day12;

namespace aoc_2023_csharp_tests;

public class Day12Tests
{
    [Test]
    public void Part1_Example()
    {
        // arrange
        var input = new[]
        {
            "???.### 1,1,3",
            ".??..??...?##. 1,1,3",
            "?#?#?#?#?#?#?#? 1,3,1,6",
            "????.#...#... 4,1,1",
            "????.######..#####. 1,6,5",
            "?###???????? 3,2,1",
        };

        var expected = 21;

        // act
        var actual = Day12.Solve1(input);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part1_Solution()
    {
        Day12.Part1().Should().Be(7732);
    }

    [Test]
    public void Part2_Example()
    {
        // arrange
        var input = new[]
        {
            "???.### 1,1,3",
            ".??..??...?##. 1,1,3",
            "?#?#?#?#?#?#?#? 1,3,1,6",
            "????.#...#... 4,1,1",
            "????.######..#####. 1,6,5",
            "?###???????? 3,2,1",
        };

        var expected = 525152;

        // act
        var actual = Day12.Solve2(input);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_Solution()
    {
        Day12.Part2().Should().Be(4500070301581);
    }
}
