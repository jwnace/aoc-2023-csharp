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

        var partRange = new PartRange(1, 4000, 1, 4000, 1, 4000, 1, 4000);

        var start = workflows["in"];

        var processedRanges = GetAcceptedRanges(partRange, start, workflows);

        var result = 0L;

        foreach (var range in processedRanges)
        {
            var xRange = range.MaxX - range.MinX + 1;
            var mRange = range.MaxM - range.MinM + 1;
            var aRange = range.MaxA - range.MinA + 1;
            var sRange = range.MaxS - range.MinS + 1;

            result += xRange * mRange * aRange * sRange;
        }

        return result;
    }

    private static bool IsAccepted(Part part, Workflow workflow, Dictionary<string, Workflow> workflows)
    {
        foreach (var rule in workflow.Rules)
        {
            if (rule.Type == RuleType.GoTo)
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

    private static IEnumerable<PartRange> GetAcceptedRanges(
        PartRange startingRange,
        Workflow startingWorkflow,
        Dictionary<string, Workflow> workflows)
    {
        var queue = new Queue<(PartRange, Workflow)>();
        queue.Enqueue((startingRange, startingWorkflow));

        while (queue.Any())
        {
            var (range, workflow) = queue.Dequeue();

            foreach (var rule in workflow.Rules)
            {
                if (rule.Type == RuleType.GoTo)
                {
                    if (rule.NextWorkflowName == "A")
                    {
                        yield return range;
                        // if this range is accepted, we don't need to continue processing it
                        break;
                    }
                    else if (rule.NextWorkflowName == "R")
                    {
                        // if this range is rejected, we don't need to continue processing it
                        break;
                    }
                    else
                    {
                        queue.Enqueue((range, workflows[rule.NextWorkflowName]));
                        // if this range matches a GoTo, we don't need to continue processing it
                        break;
                    }
                }

                if (rule.Category == 'x')
                {
                    if (rule.Type == RuleType.GreaterThan)
                    {
                        var minX = Math.Max(range.MinX, rule.Threshold + 1);
                        var matchingRange = range with { MinX = minX };
                        // var nonMatchingRange = range with { MaxX = minX - 1 };
                        range = range with { MaxX = minX - 1 };

                        if (rule.NextWorkflowName == "A")
                        {
                            yield return matchingRange;
                            // if this range is accepted, we don't need to continue processing it
                            // break;
                        }
                        else if (rule.NextWorkflowName == "R")
                        {
                            // if this range is rejected, we don't need to continue processing it
                            // break;
                        }
                        else
                        {
                            queue.Enqueue((matchingRange, workflows[rule.NextWorkflowName]));
                            // if this range matches a GoTo, we don't need to continue processing it
                            // break;
                        }
                    }

                    if (rule.Type == RuleType.LessThan)
                    {
                        var maxX = Math.Min(range.MaxX, rule.Threshold - 1);
                        var matchingRange = range with { MaxX = maxX };
                        // var nonMatchingRange = range with { MinX = maxX + 1 };
                        range = range with { MinX = maxX + 1 };

                        if (rule.NextWorkflowName == "A")
                        {
                            yield return matchingRange;
                            // if this range is accepted, we don't need to continue processing it
                            // break;
                        }
                        else if (rule.NextWorkflowName == "R")
                        {
                            // if this range is rejected, we don't need to continue processing it
                            // break;
                        }
                        else
                        {
                            queue.Enqueue((matchingRange, workflows[rule.NextWorkflowName]));
                            // if this range matches a GoTo, we don't need to continue processing it
                            // break;
                        }
                    }
                }

                if (rule.Category == 'm')
                {
                    if (rule.Type == RuleType.GreaterThan)
                    {
                        var minM = Math.Max(range.MinM, rule.Threshold + 1);
                        var matchingRange = range with { MinM = minM };
                        // var nonMatchingRange = range with { MaxM = minM - 1 };
                        range = range with { MaxM = minM - 1 };

                        if (rule.NextWorkflowName == "A")
                        {
                            yield return matchingRange;
                            // if this range is accepted, we don't need to continue processing it
                            // break;
                        }
                        else if (rule.NextWorkflowName == "R")
                        {
                            // if this range is rejected, we don't need to continue processing it
                            // break;
                        }
                        else
                        {
                            queue.Enqueue((matchingRange, workflows[rule.NextWorkflowName]));
                            // if this range matches a GoTo, we don't need to continue processing it
                            // break;
                        }
                    }

                    if (rule.Type == RuleType.LessThan)
                    {
                        var maxM = Math.Min(range.MaxM, rule.Threshold - 1);
                        var matchingRange = range with { MaxM = maxM };
                        // var nonMatchingRange = range with { MinM = maxM + 1 };
                        range = range with { MinM = maxM + 1 };

                        if (rule.NextWorkflowName == "A")
                        {
                            yield return matchingRange;
                            // if this range is accepted, we don't need to continue processing it
                            // break;
                        }
                        else if (rule.NextWorkflowName == "R")
                        {
                            // if this range is rejected, we don't need to continue processing it
                            // break;
                        }
                        else
                        {
                            queue.Enqueue((matchingRange, workflows[rule.NextWorkflowName]));
                            // if this range matches a GoTo, we don't need to continue processing it
                            // break;
                        }
                    }
                }

                if (rule.Category == 'a')
                {
                    if (rule.Type == RuleType.GreaterThan)
                    {
                        var minA = Math.Max(range.MinA, rule.Threshold + 1);
                        var matchingRange = range with { MinA = minA };
                        // var nonMatchingRange = range with { MaxA = minA - 1 };
                        range = range with { MaxA = minA - 1 };

                        if (rule.NextWorkflowName == "A")
                        {
                            yield return matchingRange;
                            // if this range is accepted, we don't need to continue processing it
                            // break;
                        }
                        else if (rule.NextWorkflowName == "R")
                        {
                            // if this range is rejected, we don't need to continue processing it
                            // break;
                        }
                        else
                        {
                            queue.Enqueue((matchingRange, workflows[rule.NextWorkflowName]));
                            // if this range matches a GoTo, we don't need to continue processing it
                            // break;
                        }
                    }

                    if (rule.Type == RuleType.LessThan)
                    {
                        var maxA = Math.Min(range.MaxA, rule.Threshold - 1);
                        var matchingRange = range with { MaxA = maxA };
                        // var nonMatchingRange = range with { MinA = maxA + 1 };
                        range = range with { MinA = maxA + 1 };

                        if (rule.NextWorkflowName == "A")
                        {
                            yield return matchingRange;
                            // if this range is accepted, we don't need to continue processing it
                            // break;
                        }
                        else if (rule.NextWorkflowName == "R")
                        {
                            // if this range is rejected, we don't need to continue processing it
                            // break;
                        }
                        else
                        {
                            queue.Enqueue((matchingRange, workflows[rule.NextWorkflowName]));
                            // if this range matches a GoTo, we don't need to continue processing it
                            // break;
                        }
                    }
                }

                if (rule.Category == 's')
                {
                    if (rule.Type == RuleType.GreaterThan)
                    {
                        var minS = Math.Max(range.MinS, rule.Threshold + 1);
                        var matchingRange = range with { MinS = minS };
                        // var nonMatchingRange = range with { MaxS = minS - 1 };
                        range = range with { MaxS = minS - 1 };

                        if (rule.NextWorkflowName == "A")
                        {
                            yield return matchingRange;
                            // if this range is accepted, we don't need to continue processing it
                            // break;
                        }
                        else if (rule.NextWorkflowName == "R")
                        {
                            // if this range is rejected, we don't need to continue processing it
                            // break;
                        }
                        else
                        {
                            queue.Enqueue((matchingRange, workflows[rule.NextWorkflowName]));
                            // if this range matches a GoTo, we don't need to continue processing it
                            // break;
                        }
                    }

                    if (rule.Type == RuleType.LessThan)
                    {
                        var maxS = Math.Min(range.MaxS, rule.Threshold - 1);
                        var matchingRange = range with { MaxS = maxS };
                        // var nonMatchingRange = range with { MinS = maxS + 1 };
                        range = range with { MinS = maxS + 1 };

                        if (rule.NextWorkflowName == "A")
                        {
                            yield return matchingRange;
                            // if this range is accepted, we don't need to continue processing it
                            // break;
                        }
                        else if (rule.NextWorkflowName == "R")
                        {
                            // if this range is rejected, we don't need to continue processing it
                            // break;
                        }
                        else
                        {
                            queue.Enqueue((matchingRange, workflows[rule.NextWorkflowName]));
                            // if this range matches a GoTo, we don't need to continue processing it
                            // break;
                        }
                    }
                }
            }
        }
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

    private enum RuleType
    {
        GoTo,
        GreaterThan,
        LessThan,
    }

    private record Part(int X, int M, int A, int S)
    {
        public static Part Parse(string text)
        {
            var (x, m, a, s) = text[1..^1].Split(',').Select(x => int.Parse(x[2..])).ToArray();
            return new Part(x, m, a, s);
        }
    }

    private record PartRange(long MinX, long MaxX, long MinM, long MaxM, long MinA, long MaxA, long MinS, long MaxS);
}
