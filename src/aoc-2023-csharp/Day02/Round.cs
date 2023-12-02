using aoc_2023_csharp.Extensions;

namespace aoc_2023_csharp.Day02;

public record Round(int Red, int Green, int Blue)
{
    public static Round Parse(string input)
    {
        var cubes = input.Split(", ");

        var red = 0;
        var green = 0;
        var blue = 0;

        foreach (var cube in cubes)
        {
            var (countString, color) = cube.Split(' ');
            var count = int.Parse(countString);

            if (color == "red")
            {
                red = count;
            }
            else if (color == "green")
            {
                green = count;
            }
            else if (color == "blue")
            {
                blue = count;
            }
        }

        return new Round(red, green, blue);
    }
}
