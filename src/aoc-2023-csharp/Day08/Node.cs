namespace aoc_2023_csharp.Day08;

public class Node
{
    public Node(string name)
    {
        Name = name;
    }

    public string Name { get; }
    public Node? Left { get; set; }
    public Node? Right { get; set; }
}
