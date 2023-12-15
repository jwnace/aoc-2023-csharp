using aoc_2023_csharp.Day15;

namespace aoc_2023_csharp_tests;

public class Day15Tests
{
    [Test]
    public void Part1_Example()
    {
        // arrange
        var input = "rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7";
        var expected = 1320;

        // act
        var actual = Day15.Solve1(input);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part1_Solution()
    {
        Day15.Part1().Should().Be(516070);
    }

    [Test]
    public void Part2_Example()
    {
        // arrange
        var input = "rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7";
        var expected = 145;

        // act
        var actual = Day15.Solve2(input);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_Solution()
    {
        Day15.Part2().Should().Be(244981);
    }
}
