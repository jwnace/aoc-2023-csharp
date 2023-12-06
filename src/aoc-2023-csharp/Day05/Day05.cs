namespace aoc_2023_csharp.Day05;

public static class Day05
{
    private static readonly string Input = File.ReadAllText("Day05/day05.txt").Trim();

    public static long Part1() => Solve1(Input);

    public static long Part2() => Solve2(Input);

    public static long Solve1(string input)
    {
        var (seeds, mappingFunctions) = ParseInput(input);

        foreach (var mappingFunction in mappingFunctions)
        {
            seeds = mappingFunction.MapValues(seeds).ToList();
        }

        return seeds.Min();
    }

    public static long Solve2(string input)
    {
        var (seeds, mappingFunctions) = ParseInput(input);
        var seedRanges = GetSeedRanges(seeds);
        var resultRanges = new List<SeedRange>();

        foreach (var seedRange in seedRanges)
        {
            var newRanges = new List<SeedRange> { new(seedRange.Start, seedRange.End) };

            foreach (var mappingFunction in mappingFunctions)
            {
                newRanges = mappingFunction.MapRanges(newRanges).ToList();
            }

            resultRanges.Add(newRanges.MinBy(r => r.Start)!);
        }

        return resultRanges.Min(x => x.Start);
    }

    private static (List<long> seeds, List<MappingFunction> mappingFunctions) ParseInput(string input)
    {
        var sections = input.Split("\n\n");
        var seeds = GetSeeds(sections[0]);
        var mappingFunctions = sections.Skip(1).Select(MappingFunction.Parse).ToList();

        return (seeds, mappingFunctions);
    }

    private static List<long> GetSeeds(string section) =>
        section.Split(" ").Skip(1).Select(long.Parse).ToList();

    private static List<SeedRange> GetSeedRanges(List<long> seeds) =>
        seeds.Chunk(2).Select(x => new SeedRange(x[0], x[0] + x[1])).ToList();
}
