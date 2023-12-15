namespace aoc_2023_csharp.Day15;

public static class Day15
{
    private static readonly string Input = File.ReadAllText("Day15/day15.txt").Trim();

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    public static int Solve1(string input) => input.Split(",").Select(ComputeHash).Sum();

    public static int Solve2(string input)
    {
        var steps = input.Split(",");
        var boxes = InitializeBoxes();

        foreach (var step in steps)
        {
            var index = step.IndexOfAny(new[] { '=', '-' });
            var label = step[..index];
            var box = ComputeHash(label);
            var operation = step[index];

            if (operation == '-')
            {
                var existingLens = boxes[box].SingleOrDefault(l => l.Split(' ')[0] == label);

                if (existingLens is not null)
                {
                    boxes[box].Remove(existingLens);
                }
            }
            else if (operation == '=')
            {
                var focalLength = int.Parse(step[(index + 1)..]);

                if (boxes[box].Any(l => l.Split(' ')[0] == label))
                {
                    var existingLens = boxes[box]
                        .Select((l, i) => (label: l, index: i))
                        .Single(lens => lens.label.Split(' ')[0] == label);

                    boxes[box][existingLens.index] = $"{label} {focalLength}";
                }
                else
                {
                    boxes[box].Add($"{label} {focalLength}");
                }
            }
        }

        return ComputeResult(boxes);
    }

    private static int ComputeHash(string step)
    {
        var result = 0;

        foreach (var c in step)
        {
            result += c;
            result *= 17;
            result %= 256;
        }

        return result;
    }

    private static List<string>[] InitializeBoxes()
    {
        var boxes = new List<string>[256];

        for (var i = 0; i < 256; i++)
        {
            boxes[i] = new List<string>();
        }

        return boxes;
    }

    private static int ComputeResult(IReadOnlyList<List<string>> boxes)
    {
        var result = 0;

        for (var i = 0; i < boxes.Count; i++)
        {
            var box = boxes[i];

            for (var j = 0; j < box.Count; j++)
            {
                var lens = box[j];
                var focalLength = int.Parse(lens.Split(' ')[1]);
                result += (i + 1) * (j + 1) * focalLength;
            }
        }

        return result;
    }
}
