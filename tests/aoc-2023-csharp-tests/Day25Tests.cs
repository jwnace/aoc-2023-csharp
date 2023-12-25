using aoc_2023_csharp.Day25;

namespace aoc_2023_csharp_tests;

public class Day25Tests
{
    [Ignore("ignore example test")]
    [Test]
    public void Part1_Example()
    {
        // arrange
        var input = new[]
        {
            "jqt: rhn xhk nvd",
            "rsh: frs pzl lsr",
            "xhk: hfx",
            "cmg: qnr nvd lhk bvb",
            "rhn: xhk bvb hfx",
            "bvb: xhk hfx",
            "pzl: lsr hfx nvd",
            "qnr: nvd",
            "ntq: jqt hfx bvb xhk",
            "nvd: lhk",
            "lsr: lhk",
            "rzs: qnr cmg lsr rsh",
            "frs: qnr lhk lsr",
        };

        var expected = 54;

        // act
        var actual = Day25.Solve1(input);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part1_Solution()
    {
        Day25.Part1().Should().Be(546804);
    }

    [Ignore("ignore example test")]
    [Test]
    public void Part2_Example()
    {
        // arrange
        var input = new[]
        {
            "",
        };

        var expected = 0;

        // act
        var actual = Day25.Solve2(input);

        // assert
        actual.Should().Be(expected);
    }

    [Ignore("ignore part2 test")]
    [Test]
    public void Part2_Solution()
    {
        Day25.Part2().Should().Be(0);
    }
}
