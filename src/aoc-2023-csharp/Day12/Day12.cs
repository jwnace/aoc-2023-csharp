using System.Collections;
using aoc_2023_csharp.Extensions;

namespace aoc_2023_csharp.Day12;

public static class Day12
{
    private static readonly string[] Input = File.ReadAllLines("Day12/day12.txt").ToArray();

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    public static int Solve1(string[] input)
    {
        var total = 0;

        for (var lineNumber = 0; lineNumber < input.Length; lineNumber++)
        {
            Console.WriteLine(
                $"Processing line {lineNumber + 1} out of {input.Length} = {lineNumber / (double)input.Length:P}");

            var line = input[lineNumber];
            total += CountWays(line);
        }

        return total;
    }

    public static int Solve2(string[] input)
    {
        return 0;
    }

    private static int CountWays(string line)
    {
        var (left, right) = line.Split(" ");
        var numbers = right.Split(",").Select(int.Parse).ToArray();

        var leftIndexed = left.Select((c, i) => (c, i)).ToArray();

        var operational = leftIndexed.Where(spring => spring.c == '.');
        var damaged = leftIndexed.Where(spring => spring.c == '#');
        var unknown = leftIndexed.Where(spring => spring.c == '?');

        var totalDamaged = numbers.Sum();
        var missingDamaged = totalDamaged - damaged.Count();

        var possibilities = GeneratePossibilities(left, missingDamaged).ToList();

        var count = 0;

        foreach (var possibility in possibilities)
        {
            var temp = possibility.Split('.', StringSplitOptions.RemoveEmptyEntries);

            if (temp.Length == numbers.Length && temp.Select(x => x.Length).SequenceEqual(numbers))
            {
                count++;
            }
        }

        return count;
    }

    private static IEnumerable<string> GeneratePossibilities(string original, int missingDamaged)
    {
        var queue = new Queue<(string, int, int)>();
        queue.Enqueue((original, missingDamaged, 0));

        var seen = new HashSet<string>();

        while (queue.Any())
        {
            var (current, missing, index) = queue.Dequeue();

            if (missing == 0)
            {
                var result = current.Replace('?', '.');

                if (!seen.Add(result))
                {
                    continue;
                }

                yield return result;
            }
            else
            {
                for (var i = index; i < current.Length; i++)
                {
                    if (current[i] == '?')
                    {
                        queue.Enqueue((current.ReplaceAt(i, '#'), missing - 1, i + 1));
                        queue.Enqueue((current.ReplaceAt(i, '.'), missing, i + 1));
                    }
                }
            }
        }
    }
}

public static class StringExtensions
{
    public static string ReplaceAt(this string str, int index, char newChar)
    {
        var chars = str.ToCharArray();
        chars[index] = newChar;
        return new string(chars);
    }
}
