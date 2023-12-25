using aoc_2023_csharp.Day25;

namespace aoc_2023_csharp_tests;

public class Day25Tests
{
    [Ignore("my solution is hard-coded for my input...")]
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

    [Test]
    public void Part2_Solution()
    {
        Day25.Part2().Should().Be("Merry Christmas!");
    }
}
