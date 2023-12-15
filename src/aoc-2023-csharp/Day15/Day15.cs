namespace aoc_2023_csharp.Day15;

public static class Day15
{
    private static readonly string Input = File.ReadAllText("Day15/day15.txt").Trim();

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    public static int Solve1(string input) => input.Split(",").Select(ComputeHash).Sum();

    public static int Solve2(string input)
    {
        var boxes = new List<string>[256];
        var steps = input.Split(",");

        for (var i = 0; i < 256; i++)
        {
            boxes[i] = new List<string>();
        }

        foreach (var step in steps)
        {
            var index = step.IndexOfAny(new[] { '=', '-' });
            var label = step[..index];
            var box = ComputeHash(label);
            var operation = step[index];

            if (operation == '-')
            {
                var temp = boxes[box].SingleOrDefault(l => l.Split(' ')[0] == label);

                if (temp is not null)
                {
                    boxes[box].Remove(temp);
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

        var result = 0;

        for (var i = 0; i < boxes.Length; i++)
        {
            var box = boxes[i];

            for (var j = 0; j < box.Count; j++)
            {
                var lens = box[j];
                var focalLength = int.Parse(lens.Split(' ')[1]);
                result += (i + 1) * (j + 1) * (focalLength);
            }
        }

        return result;
    }

    private static int ComputeHash(string input)
    {
        var result = 0;

        foreach (var c in input)
        {
            var ascii = (int)c;
            result += ascii;
            result *= 17;
            result %= 256;
        }

        return result;
    }
}
