using aoc_2023_csharp.Extensions;

namespace aoc_2023_csharp.Day09;

public static class Day09
{
    private static readonly string[] Input = File.ReadAllLines("Day09/day09.txt").ToArray();

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    public static int Solve1(string[] input) => input.Sum(Extrapolate);

    public static int Solve2(string[] input) => input.Sum(ExtrapolateBackwards);

    private static int Extrapolate(string line)
    {
        var numbers = line.Split(" ").Select(int.Parse).ToList();
        var nextSequence = numbers.ToList();
        var sequences = new List<List<int>> { numbers };

        while (nextSequence.Any(n => n != 0))
        {
            nextSequence = nextSequence.Windowed(2).Select(w => w.ToList()).Select(w => w[1] - w[0]).ToList();
            sequences.Add(nextSequence);
        }

        sequences.Last().Add(0);

        for (var i = sequences.Count - 1; i > 0; i--)
        {
            var sequence = sequences[i];
            var lastNumber = sequence.Last();
            var previousSequence = sequences[i - 1];
            var previousLastNumber = previousSequence.Last();

            previousSequence.Add(lastNumber + previousLastNumber);
        }

        return sequences.First().Last();
    }

    private static int ExtrapolateBackwards(string line)
    {
        var numbers = line.Split(" ").Select(int.Parse).ToList();
        var nextSequence = numbers.ToList();
        var sequences = new List<List<int>> { numbers };

        while (nextSequence.Any(n => n != 0))
        {
            nextSequence = nextSequence.Windowed(2).Select(w => w.ToList()).Select(w => w[1] - w[0]).ToList();
            sequences.Add(nextSequence);
        }

        sequences.Last().Add(0);

        for (var i = sequences.Count - 1; i > 0; i--)
        {
            var sequence = sequences[i];
            var firstNumber = sequence.First();
            var previousSequence = sequences[i - 1];
            var previousFirstNumber = previousSequence.First();

            previousSequence.Insert(0, previousFirstNumber - firstNumber);
        }

        return sequences.First().First();
    }
}
