namespace aoc_2023_csharp.Day19;

public record Rule(char Category, RuleType Type, int Threshold, string NextWorkflowName)
{
    public static Rule Parse(string text)
    {
        var split = text.Split(':');

        if (split.Length == 1)
        {
            return new Rule(' ', RuleType.GoTo, 0, text);
        }

        var rule = split[0];
        var nextWorkflowName = split[1];

        var category = rule[0];
        var comparison = rule[1];
        var threshold = int.Parse(rule[2..]);

        var type = comparison switch
        {
            '<' => RuleType.LessThan,
            '>' => RuleType.GreaterThan,
            _ => throw new Exception("Invalid comparison")
        };

        return new Rule(category, type, threshold, nextWorkflowName);
    }
}
