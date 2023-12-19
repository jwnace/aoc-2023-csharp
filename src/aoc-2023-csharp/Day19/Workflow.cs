namespace aoc_2023_csharp.Day19;

public record Workflow(string Name, Rule[] Rules)
{
    public static Workflow Parse(string text)
    {
        var split = text.Split('{');
        var name = split[0];
        var ruleStrings = split[1][..^1].Split(',');
        var rules = ruleStrings.Select(Rule.Parse).ToArray();

        return new Workflow(name, rules);
    }
}
