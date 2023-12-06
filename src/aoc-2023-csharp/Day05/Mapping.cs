namespace aoc_2023_csharp.Day05;

public record Mapping(long DestinationStart, long SourceStart, long Length)
{
    public long SourceEnd => SourceStart + Length;
}
