namespace aoc_2023_csharp.Day19;

public record PartRange(long MinX, long MaxX, long MinM, long MaxM, long MinA, long MaxA, long MinS, long MaxS)
{
    public long Product => (MaxX - MinX + 1) * (MaxM - MinM + 1) * (MaxA - MinA + 1) * (MaxS - MinS + 1);
}
