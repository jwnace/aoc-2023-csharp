namespace aoc_2023_csharp.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<IEnumerable<T>> Windowed<T>(this IEnumerable<T> source, int size)
    {
        var array = source as T[] ?? source.ToArray();

        for (var i = 0; i < array.Length - size + 1; i++)
        {
            yield return array.Skip(i).Take(size);
        }
    }

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

    // TODO: this uses the index instead of the value to build permutations
    // public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> source)
    // {
    //     var array = source as T[] ?? source.ToArray();
    //
    //     if (array.Length == 1)
    //     {
    //         yield return array;
    //     }
    //
    //     for (var i = 0; i < array.Length; i++)
    //     {
    //         var item = array[i];
    //
    //         foreach (var permutation in array.Where((_, index) => index != i).GetPermutations())
    //         {
    //             yield return new[] { item }.Concat(permutation);
    //         }
    //     }
    // }

    public static void Deconstruct<T>(this T[] array, out T first, out T second)
    {
        first = array[0];
        second = array[1];
    }

    public static void Deconstruct<T>(this T[] array, out T first, out T second, out T third)
    {
        first = array[0];
        second = array[1];
        third = array[2];
    }

    public static void Deconstruct<T>(this T[] array, out T first, out T second, out T third, out T fourth)
    {
        first = array[0];
        second = array[1];
        third = array[2];
        fourth = array[3];
    }

    public static void Deconstruct<T>(this T[] array, out T first, out T second, out T third, out T fourth, out T fifth)
    {
        first = array[0];
        second = array[1];
        third = array[2];
        fourth = array[3];
        fifth = array[4];
    }
}
