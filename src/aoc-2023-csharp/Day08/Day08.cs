using aoc_2023_csharp.Extensions;
using aoc_2023_csharp.Shared;

namespace aoc_2023_csharp.Day08;

public static class Day08
{
    private static readonly string Input = File.ReadAllText("Day08/day08.txt").Trim();

    public static int Part1() => Solve1(Input);

    public static long Part2() => Solve2(Input);

    public static int Solve1(string input)
    {
        var (instructions, nodes) = ParseInput(input);
        var currentNode = nodes["AAA"];
        var steps = 0;
        var index = 0;

        while (true)
        {
            if (currentNode!.Name == "ZZZ")
            {
                return steps;
            }

            var instruction = instructions[index];

            currentNode = instruction switch
            {
                'L' => currentNode.Left,
                'R' => currentNode.Right,
                _ => throw new Exception("Invalid instruction")
            };

            steps++;
            index++;
            index %= instructions.Length;
        }
    }

    public static long Solve2(string input)
    {
        var (instructions, nodes) = ParseInput(input);
        var currentNodes = nodes.Where(n => n.Key.EndsWith("A")).Select(n => n.Value).ToArray();
        var cycleLengths = new long[currentNodes.Length];
        var steps = 0L;
        var index = 0;

        while (true)
        {
            if (currentNodes.All(n => n.Name.EndsWith("Z")))
            {
                return steps;
            }

            for (var i = 0; i < currentNodes.Length; i++)
            {
                var currentNode = currentNodes[i];

                if (currentNode.Name.EndsWith("Z") && cycleLengths[i] == 0)
                {
                    cycleLengths[i] = steps;
                }

                if (cycleLengths.All(x => x > 0))
                {
                    return MathHelper.LeastCommonMultiple(cycleLengths);
                }

                currentNodes[i] = instructions[index] switch
                {
                    'L' => currentNode.Left!,
                    'R' => currentNode.Right!,
                    _ => throw new Exception("Invalid instruction")
                };
            }

            steps++;
            index++;
            index %= instructions.Length;
        }
    }

    private static (char[] instructions, Dictionary<string, Node> nodes) ParseInput(string input)
    {
        var lines = input.Split("\n");
        var instructions = lines[0].ToArray();
        var nodes = new Dictionary<string, Node>();

        foreach (var line in lines.Skip(2))
        {
            var (name, pair) = line.Split(" = ");
            var (left, right) = pair[1..^1].Split(", ");

            var node = nodes.GetValueOrDefault(name) ?? new Node(name);
            nodes[name] = node;

            var leftNode = nodes.GetValueOrDefault(left) ?? new Node(left);
            nodes[left] = leftNode;

            var rightNode = nodes.GetValueOrDefault(right) ?? new Node(right);
            nodes[right] = rightNode;

            node.Left = leftNode;
            node.Right = rightNode;
        }

        return (instructions, nodes);
    }
}
