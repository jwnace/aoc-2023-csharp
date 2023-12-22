using System.Text;
using aoc_2023_csharp.Extensions;

namespace aoc_2023_csharp.Day22;

public static class Day22
{
    private static char _currentName = 'A';
    private static readonly string[] Input = File.ReadAllLines("Day22/day22.txt").ToArray();

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    public static int Solve1(string[] input) => BuildTower(input).CountSafeBricksToDisintegrate();

    public static int Solve2(string[] input)
    {
        var tower = BuildTower(input);

        Console.WriteLine(tower.DrawFacingX());
        Console.WriteLine(tower.DrawFacingY());

        return tower.SumChainReaction();
    }

    private static Tower BuildTower(string[] input)
    {
        var bricks = input.Select(Brick.Parse)
            .OrderBy(b => b.Bottom)
            .ThenBy(b => b.Left)
            .ThenBy(b => b.Front)
            .ToArray();

        var tower = new Tower();

        foreach (var brick in bricks)
        {
            tower.Add(brick);
        }

        return tower;
    }

    private class Tower
    {
        private readonly List<Brick> _bricks = new();

        public void Add(Brick brick)
        {
            // TODO: consider encapsulating this `+1` in the `FindHighestPointBelow()` method
            var top = FindHighestPointBelow(brick);

            // update the brick's lowest Z coordinate to the top of the tower
            var updatedBrick = brick.Start.Z == brick.Bottom
                ? brick with
                {
                    Start = brick.Start with { Z = top },
                    End = brick.End with { Z = top + brick.Height }
                }
                : brick with
                {
                    End = brick.End with { Z = top },
                    Start = brick.Start with { Z = top - brick.Height }
                };

            _bricks.Add(updatedBrick);
        }

        public string DrawFacingX()
        {
            var sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine("------------------------------");

            var minX = _bricks.Min(b => b.Left);
            var maxX = _bricks.Max(b => b.Right);
            var minZ = _bricks.Min(b => b.Bottom);
            var maxZ = _bricks.Max(b => b.Top);

            for (var z = maxZ; z >= minZ; z--)
            {
                for (var x = minX; x <= maxX; x++)
                {
                    var count = _bricks.Count(b => b.Left <= x && b.Right >= x && b.Bottom <= z && b.Top >= z);

                    if (count > 1)
                    {
                        sb.Append('?');
                    }
                    else if (count == 1)
                    {
                        var brick = _bricks.First(b => b.Left <= x && b.Right >= x && b.Bottom <= z && b.Top >= z);
                        sb.Append(brick.Name);
                    }
                    else
                    {
                        sb.Append('.');
                    }
                }

                sb.AppendLine();
            }

            sb.AppendLine("------------------------------");
            sb.AppendLine();
            return sb.ToString();
        }

        public string DrawFacingY()
        {
            var sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine("------------------------------");

            var minY = _bricks.Min(b => b.Front);
            var maxY = _bricks.Max(b => b.Back);
            var minZ = _bricks.Min(b => b.Bottom);
            var maxZ = _bricks.Max(b => b.Top);

            for (var z = maxZ; z >= minZ; z--)
            {
                for (var y = minY; y <= maxY; y++)
                {
                    var count = _bricks.Count(b => b.Front <= y && b.Back >= y && b.Bottom <= z && b.Top >= z);

                    if (count > 1)
                    {
                        sb.Append('?');
                    }
                    else if (count == 1)
                    {
                        var brick = _bricks.First(b => b.Front <= y && b.Back >= y && b.Bottom <= z && b.Top >= z);
                        sb.Append(brick.Name);
                    }
                    else
                    {
                        sb.Append('.');
                    }
                }

                sb.AppendLine();
            }

            sb.AppendLine("------------------------------");
            sb.AppendLine();
            return sb.ToString();
        }

        private int FindHighestPointBelow(Brick brick)
        {
            // find the highest point of the tower that the brick will collide with given its (x, y) coordinates
            var query = _bricks.Where(b => b.Top < brick.Bottom)
                .Where(b => b.WouldCollideWith(brick))
                .ToArray();

            return query.Any() ? query.Max(b => b.Top) + 1 : 1;
        }

        public int CountSafeBricksToDisintegrate()
        {
            return _bricks.Count(CanBeSafelyDisintegrated);
        }

        private bool CanBeSafelyDisintegrated(Brick brick)
        {
            // a brick is safe to disintegrate if it is not the ONLY brick supporting another brick
            // a brick is ALSO safe to disintegrate if it is not supporting any other brick
            var supportedBricks = GetBricksSupportedBy(brick);

            foreach (var supportedBrick in supportedBricks)
            {
                var supportingBricks = GetBricksSupporting(supportedBrick);

                if (supportingBricks.Length == 1)
                {
                    return false;
                }
            }

            return true;
        }

        private Brick[] GetBricksSupporting(Brick supportedBrick)
        {
            return _bricks.Where(b => b.IsSupporting(supportedBrick)).ToArray();
        }

        private Brick[] GetBricksSupportedBy(Brick brick)
        {
            return _bricks.Where(b => b.IsSupportedBy(brick)).ToArray();
        }

        public int SumChainReaction()
        {
            var total = 0;

            for (var index = 0; index < _bricks.Count; index++)
            {
                Console.WriteLine($"index: {index} out of {_bricks.Count} ({((double)index / _bricks.Count):P})");

                var brick = _bricks[index];
                var copy = _bricks.ToList();
                copy.Remove(brick);

                var unsupportedBricks = copy.Where(b => b.Bottom > 1 && IsNotSupportedByAnyBrick(b, copy)).ToArray();

                while (unsupportedBricks.Any())
                {
                    total += unsupportedBricks.Length;
                    copy.RemoveAll(b => unsupportedBricks.Contains(b));
                    unsupportedBricks = copy.Where(b => b.Bottom > 1 && IsNotSupportedByAnyBrick(b, copy)).ToArray();
                }
            }

            return total;
        }

        private bool IsNotSupportedByAnyBrick(Brick brick, List<Brick> otherBricks)
        {
            return otherBricks.All(b => !b.IsSupporting(brick));
        }

        public int CountBricksSupportedBy(Brick brick)
        {
            var total = 0;
            var supportedBricks = _bricks.Where(b => b.IsSupportedBy(brick)).ToArray();

            foreach (var supportedBrick in supportedBricks)
            {
                var supportingBricks = _bricks.Where(b => b.IsSupporting(supportedBrick)).ToArray();

                if (supportingBricks.Length == 1)
                {
                    total += 1;
                    total += CountBricksSupportedBy(supportedBrick);
                }
            }

            return total;
        }
    }

    private record Brick(char Name, Point Start, Point End)
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
            var name = _currentName;
            _currentName++;
            _currentName = _currentName > 'Z' ? 'A' : _currentName;
            return new Brick(name, start, end);
        }

        public bool WouldCollideWith(Brick other)
        {
            var xOverlap = (Left <= other.Left && Right >= other.Left) ||
                           (Left <= other.Right && Right >= other.Right) ||
                           (Left >= other.Left && Right <= other.Right);

            var yOverlap = (Front <= other.Front && Back >= other.Front) ||
                           (Front <= other.Back && Back >= other.Back) ||
                           (Front >= other.Front && Back <= other.Back);

            return xOverlap && yOverlap;
        }

        public bool IsSupportedBy(Brick other)
        {
            return Bottom == other.Top + 1 && WouldCollideWith(other);
        }

        public bool IsSupporting(Brick other)
        {
            return Top == other.Bottom - 1 && WouldCollideWith(other);
        }
    }

    private record Point(int X, int Y, int Z)
    {
        public static Point Parse(string input)
        {
            var (x, y, z) = input.Split(',').Select(int.Parse).ToArray();
            return new Point(x, y, z);
        }
    }
}
