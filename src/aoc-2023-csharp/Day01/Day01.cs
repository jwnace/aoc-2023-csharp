namespace aoc_2023_csharp.Day01;

public static class Day01
{
    private static readonly string[] Input = File.ReadAllLines("Day01/day01.txt");

    private static readonly Dictionary<string, int> Digits = new()
    {
        { "one", 1 },
        { "two", 2 },
        { "three", 3 },
        { "four", 4 },
        { "five", 5 },
        { "six", 6 },
        { "seven", 7 },
        { "eight", 8 },
        { "nine", 9 },
    };

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    public static int Solve1(IEnumerable<string> input) =>
        input.Select(line => line.Where(char.IsDigit).ToArray())
            .Select(digits => $"{digits.First()}{digits.Last()}")
            .Sum(int.Parse);

    public static int Solve2(IEnumerable<string> input) =>
        input.Sum(line => GetFirstDigit(line) * 10 + GetLastDigit(line));

    private static int GetFirstDigit(string input) => GetDigits(input).First();

    private static int GetLastDigit(string input) => GetDigits(input).Last();

    private static IEnumerable<int> GetDigits(string input)
    {
        for (var i = 0; i < input.Length; i++)
        {
            if (char.IsDigit(input[i]))
            {
                yield return input[i] - '0';
            }

            foreach (var digit in Digits.Keys)
            {
                if (input[i..].StartsWith(digit))
                {
                    yield return Digits[digit];
                }
            }
        }
    }
}
