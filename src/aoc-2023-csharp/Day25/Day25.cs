using System.Text;
using aoc_2023_csharp.Extensions;

namespace aoc_2023_csharp.Day25;

public static class Day25
{
    private static readonly string[] Input = File.ReadAllLines("Day25/day25.txt").ToArray();

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    public static int Solve1(string[] input)
    {
        var components = ParseInput(input);
        var connections = GetAllConnections(components).ToList();

        // Console.WriteLine(OutputDotForGraphviz(connections));

        Console.WriteLine($"connections: {connections.Count}");

        connections.RemoveAll(c =>
            c is { first: "nvh", second: "grh" }
                or { first: "grh", second: "nvh" }
                or { first: "hhx", second: "vrx" }
                or { first: "vrx", second: "hhx" }
                or { first: "vkb", second: "jzj" }
                or { first: "jzj", second: "vkb" });

        Console.WriteLine($"connections: {connections.Count}");

        var count1 = CountComponentsGroupedWith("nvh", connections);
        var count2 = CountComponentsGroupedWith("grh", connections);

        Console.WriteLine($"count1: {count1}");
        Console.WriteLine($"count2: {count2}");

        return count1 * count2;
    }

    private static int CountComponentsGroupedWith(string name, List<Connection> connections)
    {
        var count = 0;
        var visited = new HashSet<string>();

        var queue = new Queue<string>();
        queue.Enqueue(name);

        while (queue.Any())
        {
            var component = queue.Dequeue();

            if (visited.Contains(component))
            {
                continue;
            }

            visited.Add(component);
            count++;

            foreach (var connection in connections.Where(c => c.first == component || c.second == component))
            {
                queue.Enqueue(connection.first == component ? connection.second : connection.first);
            }
        }

        return count;
    }

    private static string OutputDotForGraphviz(List<Connection> connections)
    {
        var sb = new StringBuilder();

        sb.AppendLine("graph {");

        foreach (var connection in connections)
        {
            // assign a random color to each edge
            var color = new Random().Next(0, 0xFFFFFF);
            sb.AppendLine($"  {connection.first} -- {connection.second} [color=\"#{color:X}\"];");
        }

        sb.AppendLine("}");

        return sb.ToString();
    }

    private static string OutputDotForGraphviz(Dictionary<string, Component> components)
    {
        var sb = new StringBuilder();

        sb.AppendLine("graph {");

        foreach (var component in components.Values)
        {
            sb.AppendLine($"  {component.Name};");
        }

        foreach (var component in components.Values)
        {
            foreach (var connection in component.Connections)
            {
                sb.AppendLine($"  {component.Name} -- {connection.Name};");
            }
        }

        sb.AppendLine("}");

        return sb.ToString();
    }

    private static IEnumerable<Connection> GetAllConnections(Dictionary<string, Component> components)
    {
        var connections = new List<Connection>();

        foreach (var component in components.Values)
        {
            foreach (var connection in component.Connections)
            {
                if (connections.Any(c => c.first == component.Name && c.second == connection.Name))
                {
                    continue;
                }

                if (connections.Any(c => c.first == connection.Name && c.second == component.Name))
                {
                    continue;
                }

                connections.Add(new Connection(component.Name, connection.Name));
            }
        }

        return connections;
    }

    private static Dictionary<string, Component> ParseInput(string[] input)
    {
        var components = new Dictionary<string, Component>();

        foreach (var line in input)
        {
            var (left, right) = line.Split(": ");

            var leftComponent = components.TryGetValue(left, out var value1) ? value1 : new Component(left);
            components[left] = leftComponent;

            foreach (var name in right.Split(" "))
            {
                var rightComponent = components.TryGetValue(name, out var value2) ? value2 : new Component(name);
                components[name] = rightComponent;

                leftComponent.Connections.Add(rightComponent);
                rightComponent.Connections.Add(leftComponent);
            }
        }

        return components;
    }

    public static int Solve2(string[] input)
    {
        return 0;
    }

    private class Component
    {
        public string Name { get; set; }
        public List<Component> Connections { get; set; } = new();

        public Component(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"{Name}: {string.Join(", ", Connections.Select(c => c.Name))}";
        }
    }

    private record Connection(string first, string second);
}
