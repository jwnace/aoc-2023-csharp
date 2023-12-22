using aoc_2023_csharp.Extensions;

namespace aoc_2023_csharp.Day22;

public record Brick(Point Start, Point End)
{
    public int Bottom => Math.Min(Start.Z, End.Z);
    public int Top => Math.Max(Start.Z, End.Z);
    public int Left => Math.Min(Start.X, End.X);
    public int Right => Math.Max(Start.X, End.X);
    public int Front => Math.Min(Start.Y, End.Y);
    public int Back => Math.Max(Start.Y, End.Y);
    public int Height => Top - Bottom;

    public static Brick Parse(string input)
    {
        var (start, end) = input.Split('~').Select(Point.Parse).ToArray();
        return new Brick(start, end);
    }

    public bool OverlapsWith(Brick other)
    {
        var xOverlap = (Left <= other.Left && Right >= other.Left) ||
                       (Left <= other.Right && Right >= other.Right) ||
                       (Left >= other.Left && Right <= other.Right);

        var yOverlap = (Front <= other.Front && Back >= other.Front) ||
                       (Front <= other.Back && Back >= other.Back) ||
                       (Front >= other.Front && Back <= other.Back);

        return xOverlap && yOverlap;
    }

    public bool IsSupportedBy(Brick other) => Bottom == other.Top + 1 && OverlapsWith(other);

    public bool IsSupporting(Brick other) => other.IsSupportedBy(this);
}
