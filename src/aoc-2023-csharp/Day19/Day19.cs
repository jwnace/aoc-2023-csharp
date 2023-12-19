using aoc_2023_csharp.Extensions;

namespace aoc_2023_csharp.Day19;

public static class Day19
{
    private static readonly string Input = File.ReadAllText("Day19/day19.txt").Trim();

    public static int Part1() => Solve1(Input);

    public static long Part2() => Solve2(Input);

    public static int Solve1(string input)
    {
        var sectionStrings = input.Split("\n\n");
        var (workflowStrings, partStrings) = sectionStrings.Select(x => x.Split("\n")).ToArray();

        var workflows = workflowStrings.Select(Workflow.Parse).ToDictionary(w => w.Name, w => w);
        var parts = partStrings.Select(Part.Parse);

        var start = workflows["in"];

        var acceptedParts = parts.Where(part => IsAccepted(part, start, workflows)).ToList();

        return acceptedParts.Sum(x => x.X + x.M + x.A + x.S);
    }

    public static long Solve2(string input)
    {
        var sectionStrings = input.Split("\n\n");
        var (workflowStrings, _) = sectionStrings.Select(x => x.Split("\n")).ToArray();
        var workflows = workflowStrings.Select(Workflow.Parse).ToDictionary(w => w.Name, w => w);

        return 0;
    }

    private static bool IsAccepted(Part part, Workflow workflow, Dictionary<string, Workflow> workflows)
    {
        foreach (var rule in workflow.Rules)
        {
            if (rule.Type == RuleType.None)
            {
                if (rule.NextWorkflowName == "A")
                {
                    return true;
                }

                if (rule.NextWorkflowName == "R")
                {
                    return false;
                }

                return IsAccepted(part, workflows[rule.NextWorkflowName], workflows);
            }

            var value = rule.Category switch
            {
                'x' => part.X,
                'm' => part.M,
                'a' => part.A,
                's' => part.S,
                _ => throw new Exception("Invalid category")
            };

            var satisfiesCondition = rule.Type switch
            {
                RuleType.GreaterThan => value > rule.Threshold,
                RuleType.LessThan => value < rule.Threshold,
                _ => throw new Exception("Invalid rule type")
            };

            if (satisfiesCondition)
            {
                if (rule.NextWorkflowName == "A")
                {
                    return true;
                }

                if (rule.NextWorkflowName == "R")
                {
                    return false;
                }

                return IsAccepted(part, workflows[rule.NextWorkflowName], workflows);
            }
        }

        return false;
    }

    private record Workflow(string Name, Rule[] Rules)
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

    private record Rule(char Category, RuleType Type, int Threshold, string NextWorkflowName)
    {
        public static Rule Parse(string text)
        {
            var split = text.Split(':');

            if (split.Length == 1)
            {
                return new Rule(' ', RuleType.None, 0, text);
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

    private enum RuleType
    {
        None,
        GreaterThan,
        LessThan,
    }

    private record Part(int X, int M, int A, int S)
    {
        public static Part Parse(string arg)
        {
            var (x, m, a, s) = arg[1..^1].Split(',').Select(x => int.Parse(x[2..])).ToArray();
            return new Part(x, m, a, s);
        }
    }
}
