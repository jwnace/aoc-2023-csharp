﻿using aoc_2023_csharp.Day06;

namespace aoc_2023_csharp_tests;

public class Day06Tests
{
    [Test]
    public void Part1_Example()
    {
        // arrange
        var input = "";
        var expected = 0;

        // act
        var actual = Day06.Solve1(input);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part1_Solution()
    {
        Day06.Part1().Should().Be(0);
    }

    [Test]
    public void Part2_Example()
    {
        // arrange
        var input = "";
        var expected = 0;

        // act
        var actual = Day06.Solve2(input);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_Solution()
    {
        Day06.Part2().Should().Be(0);
    }
}