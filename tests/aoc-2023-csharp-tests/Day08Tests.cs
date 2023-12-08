using aoc_2023_csharp.Day08;

namespace aoc_2023_csharp_tests;

public class Day08Tests
{
    [TestCase(
        """
        RL

        AAA = (BBB, CCC)
        BBB = (DDD, EEE)
        CCC = (ZZZ, GGG)
        DDD = (DDD, DDD)
        EEE = (EEE, EEE)
        GGG = (GGG, GGG)
        ZZZ = (ZZZ, ZZZ)
        """, 2)]
    [TestCase(
        """
        LLR

        AAA = (BBB, BBB)
        BBB = (AAA, ZZZ)
        ZZZ = (ZZZ, ZZZ)
        """, 6)]
    public void Part1_Example(string input, int expected)
    {
        Day08.Solve1(input).Should().Be(expected);
    }

    [Test]
    public void Part1_Solution()
    {
        Day08.Part1().Should().Be(18673);
    }

    [Test]
    public void Part2_Example()
    {
        // arrange
        var input =
            """
            LR

            11A = (11B, XXX)
            11B = (XXX, 11Z)
            11Z = (11B, XXX)
            22A = (22B, XXX)
            22B = (22C, 22C)
            22C = (22Z, 22Z)
            22Z = (22B, 22B)
            XXX = (XXX, XXX)
            """;

        var expected = 6;

        // act
        var actual = Day08.Solve2(input);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_Solution()
    {
        Day08.Part2().Should().Be(17972669116327);
    }
}

public class Day08ParallelTests
{
    [TestCase(
        """
        RL

        AAA = (BBB, CCC)
        BBB = (DDD, EEE)
        CCC = (ZZZ, GGG)
        DDD = (DDD, DDD)
        EEE = (EEE, EEE)
        GGG = (GGG, GGG)
        ZZZ = (ZZZ, ZZZ)
        """, 2)]
    [TestCase(
        """
        LLR

        AAA = (BBB, BBB)
        BBB = (AAA, ZZZ)
        ZZZ = (ZZZ, ZZZ)
        """, 6)]
    public void Part1_Example(string input, int expected)
    {
        Day08Parallel.Solve1(input).Should().Be(expected);
    }

    [Test]
    public void Part1_Solution()
    {
        Day08Parallel.Part1().Should().Be(18673);
    }

    [Test]
    public void Part2_Example()
    {
        // arrange
        var input =
            """
            LR

            11A = (11B, XXX)
            11B = (XXX, 11Z)
            11Z = (11B, XXX)
            22A = (22B, XXX)
            22B = (22C, 22C)
            22C = (22Z, 22Z)
            22Z = (22B, 22B)
            XXX = (XXX, XXX)
            """;

        var expected = 6;

        // act
        var actual = Day08Parallel.Solve2(input);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_Solution()
    {
        Day08Parallel.Part2().Should().Be(17972669116327);
    }
}
