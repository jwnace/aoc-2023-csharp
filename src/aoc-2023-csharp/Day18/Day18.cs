using aoc_2023_csharp.Extensions;

namespace aoc_2023_csharp.Day18;

public static class Day18
{
    private static readonly string[] Input = File.ReadAllLines("Day18/day18.txt").ToArray();

    public static long Part1() => Solve1(Input);

    public static long Part2() => Solve2(Input);

    public static long Solve1(string[] input) => Solve(input, 1);

    public static long Solve2(string[] input) => Solve(input, 2);

    private static long Solve(IEnumerable<string> input, int part)
    {
        var instructions = ParseInstructions(input, part).ToList();
        var polygon = BuildPolygon(instructions);
        var perimeter = CalculatePerimeter(polygon);

        return CalculateArea(polygon) + perimeter / 2 + 1;
    }

    private static IEnumerable<Instruction> ParseInstructions(IEnumerable<string> input, long part)
    {
        foreach (var line in input)
        {
            if (part == 1)
            {
                var (direction, distance, _) = ParseLine(line);
                yield return new Instruction(direction, distance);
            }
            else if (part == 2)
            {
                var (_, _, hexString) = ParseLine(line);
                var distance = Convert.ToInt32(hexString[1..^1], 16);
                var direction = hexString[^1] switch
                {
                    '0' => Direction.Right,
                    '1' => Direction.Down,
                    '2' => Direction.Left,
                    '3' => Direction.Up,
                    _ => throw new Exception($"Unknown direction: {hexString[^1]}")
                };

                yield return new Instruction(direction, distance);
            }
        }
    }

    private static (Direction direction, long distance, string color) ParseLine(string line)
    {
        var (directionString, distanceString, colorString) = line.Split(' ');

        var direction = directionString switch
        {
            "R" => Direction.Right,
            "L" => Direction.Left,
            "U" => Direction.Up,
            "D" => Direction.Down,
            _ => throw new Exception($"Unknown direction: {directionString}")
        };

        var distance = long.Parse(distanceString);
        var color = colorString[1..^1];

        return (direction, distance, color);
    }

    private static List<Point> BuildPolygon(List<Instruction> instructions)
    {
        var current = new Point(0, 0);
        var polygon = new List<Point> { current };

        foreach (var instruction in instructions)
        {
            var (direction, distance) = instruction;

            current = direction switch
            {
                Direction.Up => current with { Row = current.Row - distance },
                Direction.Down => current with { Row = current.Row + distance },
                Direction.Left => current with { Col = current.Col - distance },
                Direction.Right => current with { Col = current.Col + distance },
                _ => throw new Exception($"Unknown direction: {direction}")
            };

            polygon.Add(current);
        }

        return polygon;
    }

    private static long CalculatePerimeter(IEnumerable<Point> polygon)
    {
        var perimeter = 0L;

        foreach (var pair in polygon.Windowed(2))
        {
            var (start, end) = pair;

            if (start.Row == end.Row)
            {
                perimeter += Math.Abs(start.Col - end.Col);
            }
            else
            {
                perimeter += Math.Abs(start.Row - end.Row);
            }
        }

        return perimeter;
    }

    private static long CalculateArea(IEnumerable<Point> polygon)
    {
        var area = 0L;

        foreach (var pair in polygon.Windowed(2))
        {
            var (start, end) = pair;
            area += start.Row * end.Col - end.Row * start.Col;
        }

        return Math.Abs(area) / 2;
    }

    private record Instruction(Direction Direction, long Distance);

    private record Point(long Row, long Col);

    private enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}
