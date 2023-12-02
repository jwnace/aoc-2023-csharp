using aoc_2023_csharp.Extensions;

namespace aoc_2023_csharp.Day02;

public record Game(int Id, Round[] Rounds)
{
    private const int RedCubes = 12;
    private const int GreenCubes = 13;
    private const int BlueCubes = 14;

    public static Game Parse(string input)
    {
        var (left, right) = input.Split(": ");
        var id = int.Parse(left.Split(" ")[1].TrimEnd(':'));
        var rounds = right.Split("; ").Select(Round.Parse).ToArray();

        return new Game(id, rounds);
    }

    public bool IsPossible => MinRed <= RedCubes && MinGreen <= GreenCubes && MinBlue <= BlueCubes;

    private int MinRed => Rounds.Max(r => r.Red);

    private int MinGreen => Rounds.Max(r => r.Green);

    private int MinBlue => Rounds.Max(r => r.Blue);

    public int Power => MinRed * MinGreen * MinBlue;
}
