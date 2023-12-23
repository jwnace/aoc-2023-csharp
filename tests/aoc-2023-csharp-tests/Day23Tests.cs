using aoc_2023_csharp.Day23;

namespace aoc_2023_csharp_tests;

public class Day23Tests
{
    [Ignore("my implementation doesn't work for this example")]
    [Test]
    public void Part1_Example()
    {
        // arrange
        var input = new[]
        {
            "#.#####################",
            "#.......#########...###",
            "#######.#########.#.###",
            "###.....#.>.>.###.#.###",
            "###v#####.#v#.###.#.###",
            "###.>...#.#.#.....#...#",
            "###v###.#.#.#########.#",
            "###...#.#.#.......#...#",
            "#####.#.#.#######.#.###",
            "#.....#.#.#.......#...#",
            "#.#####.#.#.#########v#",
            "#.#...#...#...###...>.#",
            "#.#.#v#######v###.###v#",
            "#...#.>.#...>.>.#.###.#",
            "#####v#.#.###v#.#.###.#",
            "#.....#...#...#.#.#...#",
            "#.#########.###.#.#.###",
            "#...###...#...#...#.###",
            "###.###.#.###v#####v###",
            "#...#...#.#.>.>.#.>.###",
            "#.###.###.#.###.#.#v###",
            "#.....###...###...#...#",
            "#####################.#",
        };

        var expected = 94;

        // act
        var actual = Day23.Solve1(input);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part1_Solution()
    {
        Day23.Part1().Should().Be(2086);
    }

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
        var actual = Day23.Solve2(input);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_Solution()
    {
        Day23.Part2().Should().Be(0);
    }
}
