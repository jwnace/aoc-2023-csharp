using aoc_2023_csharp.Extensions;

namespace aoc_2023_csharp.Day24;

public static class Day24
{
    private static readonly string[] Input = File.ReadAllLines("Day24/day24.txt").ToArray();

    public static int Part1() => Solve1(Input, (200000000000000, 400000000000000));

    public static int Part2() => Solve2(Input);

    public static int Solve1(string[] input, (long min, long max) testArea)
    {
        var hailStones = input.Select(HailStone.Parse).ToArray();
        var count = 0;

        for (var i = 0; i < hailStones.Length - 1; i++)
        {
            for (var j = i + 1; j < hailStones.Length; j++)
            {
                var (first, second) = (hailStones[i], hailStones[j]);

                var m1 = (double)first.Velocity.Y / first.Velocity.X;
                var m2 = (double)second.Velocity.Y / second.Velocity.X;

                // TODO: do I need to worry about floating point precision here?
                if (m1 == m2)
                {
                    continue;
                }

                var b1 = first.Position.Y - m1 * first.Position.X;
                var b2 = second.Position.Y - m2 * second.Position.X;

                var x = (b2 - b1) / (m1 - m2);
                var y = m1 * x + b1;

                if (x >= testArea.min && x <= testArea.max && y >= testArea.min && y <= testArea.max)
                {
                    var px1 = first.Position.X;
                    var vx1 = first.Velocity.X;

                    if ((x < px1 && vx1 > 0) || (x > px1 && vx1 < 0))
                    {
                        continue;
                    }

                    var px2 = second.Position.X;
                    var vx2 = second.Velocity.X;

                    if ((x < px2 && vx2 > 0) || (x > px2 && vx2 < 0))
                    {
                        continue;
                    }

                    count++;
                }
            }
        }

        return count;
    }

    public static int Solve2(string[] input)
    {
        return 0;
    }

    private record HailStone(Point Position, Point Velocity)
    {
        public static HailStone Parse(string text)
        {
            var (position, velocity) = text.Split(" @ ").Select(Point.Parse).ToArray();
            return new HailStone(position, velocity);
        }
    }

    private record Point(long X, long Y, long Z)
    {
        public static Point Parse(string text)
        {
            var (x, y, z) = text.Split(", ").Select(long.Parse).ToArray();
            return new Point(x, y, z);
        }
    }
}
