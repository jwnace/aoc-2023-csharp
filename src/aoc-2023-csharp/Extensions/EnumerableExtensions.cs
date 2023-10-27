﻿namespace aoc_2023_csharp.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<IEnumerable<T>> GetCombinations<T>(this IEnumerable<T> enumerable, int length) where T : IComparable
    {
        return length switch
        {
            0 => new List<List<T>> { new() },
            1 => enumerable.Select(x => new List<T> { x }),
            _ => GetCombinations(enumerable, length - 1)
                .SelectMany(x => enumerable.Where(y => y.CompareTo(x.Last()) > 0), (a, b) => a.Concat(new[] { b }).ToList())
        };
    }

    public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> enumerable, int length)
    {
        if (length == 1)
        {
            return enumerable.Select(x => new List<T> { x });
        }

        return GetPermutations(enumerable, length - 1)
            .SelectMany(x => enumerable.Where(y => !x.Contains(y)), (a, b) => a.Concat(new[] { b }));
    }
}
