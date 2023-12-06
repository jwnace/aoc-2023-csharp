namespace aoc_2023_csharp.Day05;

public record MappingFunction(List<Mapping> Mappings)
{
    public static MappingFunction Parse(string section)
    {
        var mappings = section.Split("\n").Skip(1)
            .Select(x => x.Split(" ").Select(long.Parse).ToArray())
            .Select(y => new Mapping(y[0], y[1], y[2]))
            .ToList();

        return new MappingFunction(mappings);
    }

    public IEnumerable<long> MapValues(IEnumerable<long> values) =>
        values.Select(MapValue);

    private long MapValue(long value)
    {
        foreach (var mapping in Mappings)
        {
            if (mapping.SourceStart <= value && value < mapping.SourceStart + mapping.Length)
            {
                return mapping.DestinationStart + value - mapping.SourceStart;
            }
        }

        return value;
    }

    public IEnumerable<SeedRange> MapRanges(IEnumerable<SeedRange> ranges)
    {
        var mappedRanges = new List<SeedRange>();

        foreach (var mapping in Mappings)
        {
            var newRanges = new List<SeedRange>();

            foreach (var seedRange in ranges)
            {
                var a = seedRange with { End = Math.Min(seedRange.End, mapping.SourceStart) };

                var b = new SeedRange(
                    Math.Max(seedRange.Start, mapping.SourceStart),
                    Math.Min(mapping.SourceEnd, seedRange.End));

                var c = seedRange with { Start = Math.Max(mapping.SourceEnd, seedRange.Start) };

                if (a.End > a.Start)
                {
                    newRanges.Add(a);
                }

                if (b.End > b.Start)
                {
                    mappedRanges.Add(new SeedRange(
                        b.Start - mapping.SourceStart + mapping.DestinationStart,
                        b.End - mapping.SourceStart + mapping.DestinationStart));
                }

                if (c.End > c.Start)
                {
                    newRanges.Add(c);
                }
            }

            ranges = newRanges.ToList();
        }

        return mappedRanges.Concat(ranges);
    }
}
