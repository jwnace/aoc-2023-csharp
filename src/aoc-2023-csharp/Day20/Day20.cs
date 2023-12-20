using aoc_2023_csharp.Extensions;
using aoc_2023_csharp.Shared;

namespace aoc_2023_csharp.Day20;

public static class Day20
{
    private static readonly string[] Input = File.ReadAllLines("Day20/day20.txt").ToArray();

    public static long Part1() => Solve1(Input);

    public static long Part2() => Solve2(Input);

    public static long Solve1(string[] lines)
    {
        var modules = ParseModules(lines);

        var lowPulses = 0L;
        var highPulses = 0L;

        var broadcaster = modules["broadcaster"];

        for (var i = 0; i < 1_000; i++)
        {
            // send a low pulse to the broadcaster
            // var (low, high) = broadcaster.Pulse(Pulse.Low);
            var queue = new PriorityQueue<(Module Source, Module destination, Pulse pulse, int priority), int>();
            queue.Enqueue((broadcaster, broadcaster, Pulse.Low, 0), 0);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                var (source, module, pulse, priority) = current;

                if (pulse == Pulse.High)
                {
                    highPulses++;
                }
                else
                {
                    lowPulses++;
                }

                if (module.Type == ModuleType.Broadcaster)
                {
                    foreach (var destination in module.Destinations)
                    {
                        queue.Enqueue((module, destination, pulse, priority + 1), priority + 1);
                    }
                }

                if (module.Type == ModuleType.FlipFlop)
                {
                    if (pulse == Pulse.High)
                    {
                        continue;
                    }

                    module.IsOn = !module.IsOn;

                    var newPulse = module.IsOn ? Pulse.High : Pulse.Low;

                    foreach (var destination in module.Destinations)
                    {
                        queue.Enqueue((module, destination, newPulse, priority + 1), priority + 1);
                    }
                }

                if (module.Type == ModuleType.Conjunction)
                {
                    // HACK: initialize the memory if it's empty
                    if (module.Memory.Count == 0)
                    {
                        foreach (var input in module.Inputs)
                        {
                            module.Memory[input.Name] = Pulse.Low;
                        }
                    }

                    module.Memory[source.Name] = pulse;

                    var newPulse = module.Memory.Values.All(x => x == Pulse.High) ? Pulse.Low : Pulse.High;

                    foreach (var destination in module.Destinations)
                    {
                        queue.Enqueue((module, destination, newPulse, priority + 1), priority + 1);
                    }
                }
            }
        }

        return lowPulses * highPulses;
    }

    public static long Solve2(string[] lines)
    {
        // TODO: modify the implementation so it's not hardcoded based on my input file
        // - we are finished when the `rx` module receives a low pulse from the `cl` module
        // - the `cl` module will send a low pulse when it receives a high pulse from all inputs
        // - the `cl` module has four inputs: `js`, `qs`, `dt`, and `ts`
        // - we need to determine the first time all four inputs send high pulses
        // - we can do this by finding the least common multiple of the cycle lengths of the four inputs

        var modules = ParseModules(lines);

        var lowPulses = 0L;
        var highPulses = 0L;

        var broadcaster = modules["broadcaster"];

        var cycleLengths = new Dictionary<string, long>();

        for (var i = 0; i < int.MaxValue; i++)
        {
            var queue = new PriorityQueue<(Module Source, Module destination, Pulse pulse, int priority), int>();
            queue.Enqueue((broadcaster, broadcaster, Pulse.Low, 0), 0);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                var (source, module, pulse, priority) = current;

                if (pulse == Pulse.High)
                {
                    highPulses++;
                }
                else
                {
                    lowPulses++;
                }

                if (source.Name == "js" && pulse == Pulse.High && !cycleLengths.ContainsKey("js"))
                {
                    cycleLengths["js"] = i + 1;
                }

                if (source.Name == "qs" && pulse == Pulse.High && !cycleLengths.ContainsKey("qs"))
                {
                    cycleLengths["qs"] = i + 1;
                }

                if (source.Name == "dt" && pulse == Pulse.High && !cycleLengths.ContainsKey("dt"))
                {
                    cycleLengths["dt"] = i + 1;
                }

                if (source.Name == "ts" && pulse == Pulse.High && !cycleLengths.ContainsKey("ts"))
                {
                    cycleLengths["ts"] = i + 1;
                }

                if (cycleLengths.Count == 4)
                {
                    return MathHelper.LeastCommonMultiple(cycleLengths.Values.ToArray());
                }

                if (module.Type == ModuleType.Broadcaster)
                {
                    foreach (var destination in module.Destinations)
                    {
                        queue.Enqueue((module, destination, pulse, priority + 1), priority + 1);
                    }
                }

                if (module.Type == ModuleType.FlipFlop)
                {
                    if (pulse == Pulse.High)
                    {
                        continue;
                    }

                    module.IsOn = !module.IsOn;

                    var newPulse = module.IsOn ? Pulse.High : Pulse.Low;

                    foreach (var destination in module.Destinations)
                    {
                        queue.Enqueue((module, destination, newPulse, priority + 1), priority + 1);
                    }
                }

                if (module.Type == ModuleType.Conjunction)
                {
                    // HACK: initialize the memory if it's empty
                    if (module.Memory.Count == 0)
                    {
                        foreach (var input in module.Inputs)
                        {
                            module.Memory[input.Name] = Pulse.Low;
                        }
                    }

                    module.Memory[source.Name] = pulse;

                    var newPulse = module.Memory.Values.All(x => x == Pulse.High) ? Pulse.Low : Pulse.High;

                    foreach (var destination in module.Destinations)
                    {
                        queue.Enqueue((module, destination, newPulse, priority + 1), priority + 1);
                    }
                }
            }
        }

        throw new Exception("No solution found");
    }

    private static Dictionary<string, Module> ParseModules(string[] input)
    {
        var modules = new Dictionary<string, Module>();

        foreach (var line in input)
        {
            var (left, right) = line.Split(" -> ");
            var prefix = left[0];

            var name = prefix switch
            {
                'b' => left,
                _ => left[1..]
            };

            var type = prefix switch
            {
                'b' => ModuleType.Broadcaster,
                '%' => ModuleType.FlipFlop,
                '&' => ModuleType.Conjunction,
                _ => throw new Exception($"Unknown module type: {prefix}")
            };

            var destinations = right.Split(", ");

            var module = modules.TryGetValue(name, out var m)
                ? m
                : new Module(name, type, new List<Module>(), new List<Module>());

            if (module.Type is ModuleType.Unknown)
            {
                module.Type = type;
            }
            else if (module.Type != type)
            {
                throw new Exception($"Module {name} has conflicting types: {module.Type} and {type}");
            }

            modules[name] = module;

            foreach (var destinationName in destinations)
            {
                var destination = modules.TryGetValue(destinationName, out var dm)
                    ? dm
                    : new Module(destinationName, ModuleType.Unknown, new List<Module>(), new List<Module>());

                modules[destinationName] = destination;

                module.Destinations.Add(destination);
                destination.Inputs.Add(module);
            }
        }

        return modules;
    }

    private class Module
    {
        public string Name { get; init; }
        public ModuleType Type { get; set; }
        public List<Module> Destinations { get; init; }
        public List<Module> Inputs { get; init; }
        public bool IsOn { get; set; } = false;
        public Dictionary<string, Pulse> Memory = new();

        public Module(string name, ModuleType type, List<Module> destinations, List<Module> inputs)
        {
            Name = name;
            Type = type;
            Destinations = destinations;
            Inputs = inputs;
        }

        public void Deconstruct(out string name, out List<Module> destinations)
        {
            name = Name;
            destinations = Destinations;
        }

        public override string ToString()
        {
            return $"{Type} {Name} -> {string.Join(", ", Destinations.Select(d => d.Name))}";
        }
    }

    private enum ModuleType
    {
        Unknown,
        Broadcaster,
        FlipFlop,
        Conjunction,
    }

    private enum Pulse
    {
        Low,
        High,
    }
}
