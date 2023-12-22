namespace aoc_2023_csharp.Day22;

public class Tower
{
    private readonly List<Brick> _bricks = new();

    public static Tower Build(IEnumerable<string> input)
    {
        var bricks = input.Select(Brick.Parse)
            .OrderBy(b => b.Bottom)
            .ThenBy(b => b.Left)
            .ThenBy(b => b.Front)
            .ToArray();

        var tower = new Tower();

        foreach (var brick in bricks)
        {
            tower.Add(brick);
        }

        return tower;
    }

    private void Add(Brick brick)
    {
        var top = FindHighestPointBelow(brick);

        // update the brick's lowest Z coordinate to the top of the tower
        var updatedBrick = brick.Start.Z == brick.Bottom
            ? new Brick(Start: brick.Start with { Z = top }, End: brick.End with { Z = top + brick.Height })
            : new Brick(End: brick.End with { Z = top }, Start: brick.Start with { Z = top - brick.Height });

        _bricks.Add(updatedBrick);
    }

    private int FindHighestPointBelow(Brick brick)
    {
        // find the highest point of the tower that the brick will collide with given its (x, y) coordinates
        var query = _bricks.Where(b => b.Top < brick.Bottom)
            .Where(b => b.OverlapsWith(brick))
            .ToArray();

        return query.Any() ? query.Max(b => b.Top) + 1 : 1;
    }

    public int CountSafeBricksToDisintegrate() => _bricks.Count(CanBeSafelyDisintegrated);

    private bool CanBeSafelyDisintegrated(Brick brick)
    {
        // a brick is safe to disintegrate if it is not the ONLY brick supporting another brick
        var supportedBricks = GetBricksSupportedBy(brick);

        foreach (var supportedBrick in supportedBricks)
        {
            var supportingBricks = GetBricksSupporting(supportedBrick);

            if (supportingBricks.Length == 1)
            {
                return false;
            }
        }

        return true;
    }

    private Brick[] GetBricksSupporting(Brick supportedBrick) =>
        _bricks.Where(b => b.IsSupporting(supportedBrick)).ToArray();

    private Brick[] GetBricksSupportedBy(Brick brick) =>
        _bricks.Where(b => b.IsSupportedBy(brick)).ToArray();

    public int SumChainReaction()
    {
        var total = 0;

        foreach (var brick in _bricks)
        {
            var copy = _bricks.ToList();
            copy.Remove(brick);

            var unsupportedBricks = copy.Where(b => b.Bottom > 1 && IsNotSupportedByAnyBrick(b, copy)).ToArray();

            while (unsupportedBricks.Any())
            {
                total += unsupportedBricks.Length;
                copy.RemoveAll(b => unsupportedBricks.Contains(b));
                unsupportedBricks = copy.Where(b => b.Bottom > 1 && IsNotSupportedByAnyBrick(b, copy)).ToArray();
            }
        }

        return total;
    }

    private bool IsNotSupportedByAnyBrick(Brick brick, List<Brick> otherBricks) =>
        otherBricks.All(b => !b.IsSupporting(brick));
}
