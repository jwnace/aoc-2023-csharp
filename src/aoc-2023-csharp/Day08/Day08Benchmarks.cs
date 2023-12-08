using BenchmarkDotNet.Attributes;

namespace aoc_2023_csharp.Day08;

[MemoryDiagnoser]
[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class Day08Benchmarks
{
    [Benchmark(Baseline = true)]
    public int Baseline() => Day08.Part1();

    [Benchmark]
    public int Parallel() => Day08Parallel.Part1();
}
